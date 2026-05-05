using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using cleo.Data;
using cleo.Models;
using Microsoft.EntityFrameworkCore;
using cleo.Services;

namespace cleo.Controllers;

public class DashboardController : Controller
{
    private readonly CleoDbContext _db;
    private readonly IAIService _aiService;
    private readonly ICyclePredictionService _predictionService;
    private readonly IReminderService _reminderService;

    public DashboardController(CleoDbContext db, IAIService aiService, ICyclePredictionService predictionService, IReminderService reminderService)
    {
        _db = db;
        _aiService = aiService;
        _predictionService = predictionService;
        _reminderService = reminderService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        var user = await _db.Users.FindAsync(userId);
        
        if (user == null) 
        {
            return RedirectToAction("Login", "Public");
        }

        ViewBag.UserName = user.Name;
        
        var lastCycle = await _db.CycleTracks
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.StartDate)
            .FirstOrDefaultAsync();

        // Ensure user goes through onboarding if they haven't set their profiling info
        if (string.IsNullOrEmpty(user.AgeGroup) || lastCycle == null)
        {
            return RedirectToAction("Onboarding", "Public");
        }

        if (lastCycle != null && user != null)
        {
            // === EXTERNAL CYCLE PREDICTION LOGIC EXPORTED TO DASHBOARD UI === //
            
            // 1. Current Cycle Day
            int currentDay = _predictionService.GetCurrentCycleDay(lastCycle.StartDate);
            ViewBag.CycleDay = currentDay <= user.CycleLength ? currentDay : 1;
            
            // 2. Next Period Calculation
            var nextDate = _predictionService.GetNextPeriodDate(lastCycle.StartDate, user.CycleLength);
            ViewBag.NextPeriodDate = nextDate.ToString("MMM dd, yyyy");
            ViewBag.DaysUntilNextPeriod = _predictionService.GetDaysLeft(nextDate);
            
            // 3. Ovulation Tracking
            var ovulationDate = _predictionService.GetOvulationDate(lastCycle.StartDate, user.CycleLength);
            ViewBag.OvulationDay = ovulationDate.ToString("MMM dd, yyyy");
            ViewBag.OvulationDaysUntil = _predictionService.GetDaysLeft(ovulationDate);

            // 4. Fertile Window
            var fertileWindow = _predictionService.GetFertileWindow(lastCycle.StartDate, user.CycleLength);
            ViewBag.FertileWindowStart = fertileWindow.Start.ToString("MMM dd");
            ViewBag.FertileWindowEnd = fertileWindow.End.ToString("MMM dd");
            ViewBag.FertileWindowRange = $"{ViewBag.FertileWindowStart} - {ViewBag.FertileWindowEnd}";

            // === AUTO-GENERATE REMINDERS === //
            await _reminderService.CheckAndGenerateRemindersAsync(userId);

            // === CYCLE INSIGHTS CALCULATION === //
            var allCycles = await _db.CycleTracks
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.StartDate)
                .ToListAsync();

            // 1. Average Cycle Length
            ViewBag.AvgCycleLength = user.CycleLength; // Fallback
            if (allCycles.Count >= 2)
            {
                var lengths = new List<double>();
                for (int i = 0; i < allCycles.Count - 1; i++)
                {
                    lengths.Add((allCycles[i].StartDate - allCycles[i + 1].StartDate).TotalDays);
                }
                ViewBag.AvgCycleLength = (int)Math.Round(lengths.Average());
            }

            // 2. Cycle Regularity
            bool isRegular = true;
            if (allCycles.Count >= 3)
            {
                var lengths = new List<double>();
                for (int i = 0; i < Math.Min(allCycles.Count - 1, 3); i++)
                {
                    lengths.Add((allCycles[i].StartDate - allCycles[i + 1].StartDate).TotalDays);
                }
                // If standard deviation is low, it's regular. Simplified check:
                var avg = lengths.Average();
                isRegular = lengths.All(l => Math.Abs(l - avg) <= 2);
            }
            ViewBag.IsRegular = isRegular;

            // 3. Days Until Fertile
            var nextFertile = _predictionService.GetFertileWindow(lastCycle.StartDate, user.CycleLength).Start;
            // If already passed in current cycle, check next cycle
            if (nextFertile < DateTime.UtcNow.Date)
            {
                nextFertile = _predictionService.GetFertileWindow(nextDate, user.CycleLength).Start;
            }
            ViewBag.DaysUntilFertile = (nextFertile - DateTime.UtcNow.Date).Days;

            // === CALENDAR DATA PREPARATION === //
            var today = DateTime.UtcNow.Date;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            
            var calendarDays = new List<object>();
            for (var date = startOfMonth; date <= endOfMonth; date = date.AddDays(1))
            {
                string type = "none";
                if (date == today) type = "today";
                
                // Determine if date falls into period, fertile, or ovulation
                // Check all logged cycles for historical periods
                bool isHistoricalPeriod = allCycles.Any(c => date >= c.StartDate.Date && date <= (c.EndDate?.Date ?? c.StartDate.AddDays(user.PeriodLength - 1).Date));
                
                if (isHistoricalPeriod)
                {
                    type = "period";
                }
                else
                {
                    // Predict future
                    var nextPeriodStart = _predictionService.GetNextPeriodDate(lastCycle.StartDate, user.CycleLength).Date;
                    var nextPeriodEnd = nextPeriodStart.AddDays(user.PeriodLength - 1).Date;
                    
                    if (date >= nextPeriodStart && date <= nextPeriodEnd) type = "period";
                    else if (date == ovulationDate.Date) type = "ovulation";
                    else if (date >= fertileWindow.Start.Date && date <= fertileWindow.End.Date) type = "fertile";
                }

                calendarDays.Add(new { Day = date.Day, Type = type, FullDate = date });
            }
            ViewBag.CalendarDays = calendarDays;
            ViewBag.MonthTitle = today.ToString("MMMM yyyy");

            // === TODAY'S MOOD === //
            var todayMood = await _db.MoodNotes
                .Where(m => m.UserId == userId && m.Date.Date == today)
                .FirstOrDefaultAsync();
            ViewBag.Mood = todayMood?.Mood;
        }
        else
        {
            ViewBag.CycleDay = 0;
            ViewBag.NextPeriodDate = "No data";
            ViewBag.DaysUntilNextPeriod = 0;
            ViewBag.OvulationDay = "No data";
            ViewBag.OvulationDaysUntil = 0;
        }

        ViewBag.CycleLength = user.CycleLength;
        ViewBag.PeriodLength = user.PeriodLength;

        // Real symptoms/mood
        ViewBag.Symptoms = await _db.SymptomLogs.Where(s => s.UserId == userId).Take(3).Select(s => s.Symptoms).ToListAsync();
        var lastMood = await _db.MoodNotes.Where(m => m.UserId == userId).OrderByDescending(m => m.Date).FirstOrDefaultAsync();
        ViewBag.Mood = lastMood?.Mood ?? "Not set";
        ViewBag.Notes = await _db.MoodNotes.Where(m => m.UserId == userId).OrderByDescending(m => m.Date).Take(3).Select(n => n.Note).ToListAsync();

        // Upcoming Reminders
        ViewBag.UpcomingReminders = await _db.Reminders
            .Where(r => r.UserId == userId && r.ReminderDate >= DateTime.UtcNow.Date)
            .OrderBy(r => r.ReminderDate)
            .Take(3)
            .ToListAsync();

        return View("~/Views/Dashboard/Index.cshtml");
    }

    [HttpGet]
    public IActionResult LogPeriod()
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;
        return View("~/Views/Period/Index.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogPeriod(string startDate, string endDate, string flow = "Medium", string source = "", int? year = null, int? month = null)
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Public");

        if (DateTime.TryParse(startDate, out var start))
        {
            DateTime? end = DateTime.TryParse(endDate, out var e) ? e : null;

            var allUserCycles = await _db.CycleTracks
                .Where(c => c.UserId == userId.Value)
                .ToListAsync();

            var existingCycle = allUserCycles.FirstOrDefault(c => Math.Abs((c.StartDate - start).TotalDays) < 10);

            if (existingCycle != null)
            {
                existingCycle.StartDate = start;
                existingCycle.EndDate = end;
                existingCycle.Flow = flow;
            }
            else
            {
                _db.CycleTracks.Add(new CycleTrack { UserId = userId.Value, StartDate = start, EndDate = end, Flow = flow });
            }

            await _db.SaveChangesAsync();
            await _reminderService.UpdateLastActivityAsync(userId.Value);
            TempData["PeriodMessage"] = "Period cycle logged successfully!";
        }

        if (source == "Calendar")
        {
            // Preserve the month/year the user was on
            return RedirectToAction(nameof(Calendar), new { year = year, month = month });
        }

        return RedirectToAction(nameof(LogPeriod));
    }

    [HttpGet]
    public IActionResult LogSymptoms()
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;
        return View("~/Views/Symptoms/Index.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogSymptoms(string date, List<string>? symptoms, string notes)
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Public");

        var selected = symptoms != null ? string.Join(", ", symptoms) : "";
        var tip = await _aiService.GetSymptomTipAsync(symptoms ?? new List<string>(), notes);
        
        _db.SymptomLogs.Add(new SymptomLog 
        { 
            UserId = userId.Value, 
            Date = date, 
            Symptoms = selected, 
            Notes = notes,
            AITip = tip
        });
        await _db.SaveChangesAsync();
        await _reminderService.UpdateLastActivityAsync(userId.Value);

        TempData["SymptomsMessage"] = "Symptoms logged to your profile!";
        TempData["Tips"] = tip;
        return RedirectToAction(nameof(LogSymptoms));
    }

    [HttpGet]
    public async Task<IActionResult> Calendar(int? year, int? month)
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return RedirectToAction("Login", "Public");

        var allCycles = await _db.CycleTracks
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.StartDate)
            .ToListAsync();
            
        var lastCycle = allCycles.FirstOrDefault();

        var today = DateTime.UtcNow.Date;
        
        int targetYear = year ?? today.Year;
        int targetMonth = month ?? today.Month;
        var currentDisplayDate = new DateTime(targetYear, targetMonth, 1);

        ViewBag.MonthTitle = currentDisplayDate.ToString("MMMM yyyy");
        ViewBag.CurrentYear = targetYear;
        ViewBag.CurrentMonth = targetMonth;
        ViewBag.PrevYear = targetMonth == 1 ? targetYear - 1 : targetYear;
        ViewBag.PrevMonth = targetMonth == 1 ? 12 : targetMonth - 1;
        ViewBag.NextYear = targetMonth == 12 ? targetYear + 1 : targetYear;
        ViewBag.NextMonth = targetMonth == 12 ? 1 : targetMonth + 1;

        var startOfMonth = new DateTime(targetYear, targetMonth, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            
        ViewBag.FirstDayOfWeek = (int)startOfMonth.DayOfWeek;
        ViewBag.DaysInMonth = DateTime.DaysInMonth(targetYear, targetMonth);
        ViewBag.TodayDay = (targetYear == today.Year && targetMonth == today.Month) ? today.Day : 0;

        var calendarDays = new List<object>();
        for (var date = startOfMonth; date <= endOfMonth; date = date.AddDays(1))
        {
            string type = "none";
            if (date == today) type = "today";
            
            bool isHistoricalPeriod = allCycles.Any(c => date >= c.StartDate.Date && date <= (c.EndDate?.Date ?? c.StartDate.AddDays(user.PeriodLength - 1).Date));
            
            if (isHistoricalPeriod) 
            {
                type = "period";
            }
            else 
            {
                bool isOvulation = false;
                bool isFertile = false;

                foreach (var cycle in allCycles)
                {
                    var cycleOvulation = _predictionService.GetOvulationDate(cycle.StartDate, user.CycleLength).Date;
                    var cycleFertile = _predictionService.GetFertileWindow(cycle.StartDate, user.CycleLength);
                    
                    if (date == cycleOvulation) isOvulation = true;
                    if (date >= cycleFertile.Start.Date && date <= cycleFertile.End.Date) isFertile = true;
                }

                if (isOvulation) type = "ovulation";
                else if (isFertile) type = "fertile";
                else if (lastCycle != null)
                {
                    var nextPeriodStart = _predictionService.GetNextPeriodDate(lastCycle.StartDate, user.CycleLength).Date;
                    var nextPeriodEnd = nextPeriodStart.AddDays(user.PeriodLength - 1).Date;
                    
                    if (date >= nextPeriodStart && date <= nextPeriodEnd && date > today) 
                    {
                        // Check if we already have a logged cycle that is close to this next predicted start date
                        bool alreadyLoggedCloseToNext = allCycles.Any(c => Math.Abs((c.StartDate - nextPeriodStart).TotalDays) < 10);
                        if (!alreadyLoggedCloseToNext)
                        {
                            type = "period";
                        }
                    }
                }
            }

            calendarDays.Add(new { Day = date.Day, Type = type, FullDate = date });
        }

        ViewBag.CalendarDays = calendarDays;

        return View("~/Views/Calendar/Index.cshtml");
    }

    [HttpGet]
    public async Task<IActionResult> History()
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        var user = await _db.Users.FindAsync(userId);
        var history = await _db.CycleTracks
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.StartDate)
            .ToListAsync();

        ViewBag.HistoryList = history;
        ViewBag.TotalCycles = history.Count;
        
        // 1. Average Cycle Length
        int avgCycle = user.CycleLength;
        if (history.Count >= 2)
        {
            var lengths = new List<double>();
            for (int j = 0; j < history.Count - 1; j++)
            {
                lengths.Add((history[j].StartDate - history[j + 1].StartDate).TotalDays);
            }
            avgCycle = (int)Math.Round(lengths.Average());
        }
        ViewBag.AvgCycleLength = $"{avgCycle} days";

        var lastCycle = history.FirstOrDefault();
        ViewBag.LastPeriod = lastCycle?.StartDate.ToString("MMM dd, yyyy") ?? "None";
        if (lastCycle != null && user != null)
        {
            ViewBag.NextExpected = _predictionService.GetNextPeriodDate(lastCycle.StartDate, user.CycleLength).ToString("MMM dd, yyyy");
        }
        else
        {
            ViewBag.NextExpected = "None";
        }

        // 2. Regularity
        string regularity = "Very Regular";
        string regularityDetail = "±1 day variance";
        if (history.Count >= 3)
        {
            var lengths = new List<double>();
            for (int j = 0; j < Math.Min(history.Count - 1, 3); j++)
            {
                lengths.Add((history[j].StartDate - history[j + 1].StartDate).TotalDays);
            }
            var avg = lengths.Average();
            var variance = lengths.Select(l => Math.Abs(l - avg)).Max();
            if (variance > 5) { regularity = "Irregular"; regularityDetail = $"±{(int)variance} days variance"; }
            else if (variance > 2) { regularity = "Fairly Regular"; regularityDetail = $"±{(int)variance} days variance"; }
        }
        ViewBag.Regularity = regularity;
        ViewBag.RegularityDetail = regularityDetail;

        // 3. Most Common Flow
        var flowGroups = history.Where(c => !string.IsNullOrEmpty(c.Flow))
            .GroupBy(c => c.Flow)
            .OrderByDescending(g => g.Count())
            .FirstOrDefault();
        ViewBag.CommonFlow = flowGroups?.Key ?? "Medium";
        ViewBag.FlowDetail = history.Count > 0 ? $"{(flowGroups?.Count() ?? 0) * 100 / history.Count}% of cycles" : "No data";

        return View("~/Views/Dashboard/History.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCycle(int id)
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var cycle = await _db.CycleTracks.FindAsync(id);
        if (cycle != null && cycle.UserId == HttpContext.Session.GetInt32("UserId"))
        {
            _db.CycleTracks.Remove(cycle);
            await _db.SaveChangesAsync();
        }
        
        return RedirectToAction(nameof(History));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCycle(int id, string startDate, string endDate, string flow)
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var cycle = await _db.CycleTracks.FindAsync(id);
        if (cycle != null && cycle.UserId == HttpContext.Session.GetInt32("UserId"))
        {
            if (DateTime.TryParse(startDate, out var start))
            {
                cycle.StartDate = start;
                cycle.EndDate = DateTime.TryParse(endDate, out var e) ? e : null;
                cycle.Flow = flow;
                await _db.SaveChangesAsync();
            }
        }
        
        return RedirectToAction(nameof(History));
    }

    [HttpGet]
    public async Task<IActionResult> Mood()
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        ViewBag.MoodEntries = await _db.MoodNotes
            .Where(m => m.UserId == userId)
            .OrderByDescending(m => m.Date)
            .ToListAsync();
            
        return View("~/Views/Dashboard/Mood.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogMood(string mood, string description, string source = "")
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId != null)
        {
            var tip = await _aiService.GetMoodTipAsync(mood, description);
            _db.MoodNotes.Add(new MoodNote 
            { 
                UserId = userId.Value, 
                Mood = mood, 
                Note = description,
                AITip = tip,
                Date = DateTime.UtcNow.Date
            });
            await _db.SaveChangesAsync();
            await _reminderService.UpdateLastActivityAsync(userId.Value);
            TempData["AITip"] = tip;
        }

        if (source == "Dashboard")
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Mood));
    }

    [HttpGet]
    public async Task<IActionResult> Notes()
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        
        var moodNotes = await _db.MoodNotes
            .Where(m => m.UserId == userId)
            .OrderByDescending(m => m.Date)
            .Select(m => new { Id = m.Id, Type = "Mood", Title = $"Mood: {m.Mood}", Content = m.Note ?? "No notes", Date = m.Date.ToString("yyyy-MM-dd"), RawDate = m.Date })
            .ToListAsync();
            
        var symptomNotes = await _db.SymptomLogs
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.Date)
            .Select(s => new { Id = s.Id, Type = "Symptom", Title = $"Symptoms: {s.Symptoms}", Content = s.Notes ?? "No notes", Date = s.Date, RawDate = DateTime.Parse(s.Date) })
            .ToListAsync();
            
        var allNotes = moodNotes.Cast<dynamic>().Concat(symptomNotes.Cast<dynamic>()).OrderByDescending(n => n.RawDate).ToList();

        ViewBag.Notes = allNotes;
        return View("~/Views/Dashboard/Notes.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddNote(string title, string content)
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId != null && !string.IsNullOrEmpty(content))
        {
            var noteContent = string.IsNullOrEmpty(title) ? content : $"{title}\n\n{content}";
            _db.MoodNotes.Add(new MoodNote 
            { 
                UserId = userId.Value, 
                Mood = "Note", 
                Note = noteContent,
                Date = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Notes));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditNote(int id, string type, string title, string content)
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        if (type == "Mood")
        {
            var note = await _db.MoodNotes.FindAsync(id);
            if (note != null)
            {
                note.Note = string.IsNullOrEmpty(title) ? content : $"{title}\n\n{content}";
                await _db.SaveChangesAsync();
            }
        }
        else if (type == "Symptom")
        {
            var log = await _db.SymptomLogs.FindAsync(id);
            if (log != null)
            {
                log.Notes = content;
                await _db.SaveChangesAsync();
            }
        }
        return RedirectToAction(nameof(Notes));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteNote(int id, string type)
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        if (type == "Mood")
        {
            var note = await _db.MoodNotes.FindAsync(id);
            if (note != null)
            {
                _db.MoodNotes.Remove(note);
                await _db.SaveChangesAsync();
            }
        }
        else if (type == "Symptom")
        {
            var log = await _db.SymptomLogs.FindAsync(id);
            if (log != null)
            {
                _db.SymptomLogs.Remove(log);
                await _db.SaveChangesAsync();
            }
        }
        return RedirectToAction(nameof(Notes));
    }

    [HttpGet]
    public async Task<IActionResult> Tips()
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var articles = await _db.Articles.ToListAsync();
        var colors = new[] { "green", "pink", "blue", "purple" };
        var icons = new[] { "🥬", "🧘", "💊", "🫧", "✨", "😴", "🍵", "💧" };
        
        var tips = articles.Select((a, i) => new 
        { 
            Icon = icons[i % icons.Length], 
            Title = a.Title, 
            Category = a.Category, 
            Content = a.Content, 
            Color = colors[i % colors.Length] 
        }).ToList();

        ViewBag.Tips = tips;
        return View("~/Views/Dashboard/Tips.cshtml");
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return RedirectToAction("Login", "Public");

        ViewBag.Profile = new
        {
            user.Name,
            user.Email,
            AgeGroup = user.AgeGroup ?? "Not set",
            CycleLength = user.CycleLength,
            PeriodLength = user.PeriodLength,
            JoinDate = user.JoinDate.ToString("MMM yyyy"),
            TotalCycles = await _db.CycleTracks.CountAsync(c => c.UserId == userId),
            TotalMoods = await _db.MoodNotes.CountAsync(m => m.UserId == userId),
            Age = user.AgeGroup ?? "Not set",
            DateOfBirth = user.DateOfBirth?.ToString("yyyy-MM-dd")
        };

        return View("~/Views/Dashboard/Profile.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfile(string name, string ageGroup, DateTime? dob)
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        var user = await _db.Users.FindAsync(userId);
        if (user != null)
        {
            user.Name = name;
            user.AgeGroup = ageGroup;
            user.DateOfBirth = dob;
            await _db.SaveChangesAsync();
            
            // Update session if name changed
            HttpContext.Session.SetString("UserName", name);
        }
        return RedirectToAction(nameof(Profile));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAccount()
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        var userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        var user = await _db.Users.FindAsync(userId);
        if (user != null)
        {
            // Delete associated data first
            var moods = _db.MoodNotes.Where(m => m.UserId == userId);
            var symptoms = _db.SymptomLogs.Where(s => s.UserId == userId);
            var cycles = _db.CycleTracks.Where(c => c.UserId == userId);
            var reminders = _db.Reminders.Where(r => r.UserId == userId);

            _db.MoodNotes.RemoveRange(moods);
            _db.SymptomLogs.RemoveRange(symptoms);
            _db.CycleTracks.RemoveRange(cycles);
            _db.Reminders.RemoveRange(reminders);
            
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            
            HttpContext.Session.Clear();
        }
        return RedirectToAction("Login", "Public");
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;
        return View("~/Views/Dashboard/ChangePassword.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
    {
        var guard = EnsureUserSession();
        if (guard != null) return guard;

        if (newPassword != confirmPassword)
        {
            TempData["PassMessage"] = "Passwords do not match.";
            return RedirectToAction(nameof(ChangePassword));
        }

        TempData["PassMessage"] = "Password updated successfully.";
        return RedirectToAction(nameof(ChangePassword));
    }

    private T? GetSessionObject<T>(string key)
    {
        var json = HttpContext.Session.GetString(key);
        return string.IsNullOrWhiteSpace(json) ? default : JsonSerializer.Deserialize<T>(json);
    }

    private void SetSessionObject<T>(string key, T value)
    {
        HttpContext.Session.SetString(key, JsonSerializer.Serialize(value));
    }

    private IActionResult? EnsureUserSession()
    {
        var role = HttpContext.Session.GetString("Role");
        return string.Equals(role, "User", StringComparison.OrdinalIgnoreCase)
            ? null
            : RedirectToAction("Login", "Public");
    }
}
