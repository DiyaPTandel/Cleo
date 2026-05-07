using cleo.Data;
using cleo.Models;
using cleo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace cleo.Controllers;

public class AdminController : Controller
{
    private readonly CleoDbContext _db;
    private readonly IWebHostEnvironment _env;
    private readonly ISettingsService _settings;

    public AdminController(CleoDbContext db, IWebHostEnvironment env, ISettingsService settings)
    {
        _db = db;
        _env = env;
        _settings = settings;
    }

    [HttpGet]
    public IActionResult Index() => RedirectToAction(nameof(Dashboard));

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string email, string password)
    {
        // Simple authentication check against the database
        var admin = await _db.Admins.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
        if (admin != null)
        {
            HttpContext.Session.SetString("Role", "Admin");
            HttpContext.Session.SetString("Email", admin.Email);
            HttpContext.Session.SetString("AdminName", admin.Name);
            return RedirectToAction(nameof(Dashboard));
        }

        ViewBag.Error = "Invalid credentials";
        return View();
    }



    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        ViewBag.AdminEmail = HttpContext.Session.GetString("Email") ?? "admin@cleo.app";
        
        var totalUsers = await _db.Users.CountAsync();
        var activeUsers = await _db.Users.CountAsync(u => u.Status == AccountStatus.Active);
        var cyclesLogged = await _db.CycleTracks.CountAsync();
        var moodsLogged = await _db.MoodNotes.CountAsync();
        var symptomsLogged = await _db.SymptomLogs.CountAsync();
        
        ViewBag.Stats = new
        {
            TotalUsers = totalUsers,
            ActiveUsers = activeUsers,
            CyclesLogged = cyclesLogged,
            MoodsLogged = moodsLogged,
            SymptomsLogged = symptomsLogged,
            SystemHealth = "98%",
            TotalLogs = cyclesLogged + moodsLogged + symptomsLogged
        };

        // Activity Insights Data
        ViewBag.ActivityInsights = new
        {
            Periods = cyclesLogged,
            Moods = moodsLogged,
            Symptoms = symptomsLogged,
            Notes = await _db.MoodNotes.CountAsync(m => !string.IsNullOrEmpty(m.Note))
        };

        // Aggregated Recent Activity
        var latestPeriods = await _db.CycleTracks
            .Join(_db.Users, p => p.UserId, u => u.Id, (p, u) => new { p, u })
            .OrderByDescending(x => x.p.Id)
            .Take(5)
            .ToListAsync();

        var latestMoods = await _db.MoodNotes
            .Join(_db.Users, m => m.UserId, u => u.Id, (m, u) => new { m, u })
            .OrderByDescending(x => x.m.Id)
            .Take(5)
            .ToListAsync();
        var latestSymptoms = await _db.SymptomLogs
            .Join(_db.Users, s => s.UserId, u => u.Id, (s, u) => new { s, u })
            .OrderByDescending(x => x.s.Id)
            .Take(5)
            .ToListAsync();
        
        var aggregatedActivity = latestPeriods.Select(p => new {
            User = p.u.Name,
            Email = p.u.Email,
            Action = "Logged a new period",
            Time = "Recent",
            Status = p.u.Status.ToString()
        }).Concat(latestMoods.Select(m => new {
            User = m.u.Name,
            Email = m.u.Email,
            Action = "Added a mood entry",
            Time = "Recent",
            Status = m.u.Status.ToString()
        })).Concat(latestSymptoms.Select(s => new {
            User = s.u.Name,
            Email = s.u.Email,
            Action = "Logged symptoms",
            Time = "Recent",
            Status = s.u.Status.ToString()
        })).OrderByDescending(a => a.Time).Take(10).ToList();

        ViewBag.RecentActivity = aggregatedActivity;
        
        // Growth Chart Data (last 7 days)
        var chartLabels = new List<string>();
        var chartData = new List<int>();
        for (int i = 6; i >= 0; i--)
        {
            var date = DateTime.Today.AddDays(-i);
            chartLabels.Add(date.ToString("MMM dd"));
            var count = await _db.Users.CountAsync(u => u.JoinDate.Date <= date.Date);
            chartData.Add(count);
        }
        ViewBag.ChartLabels = chartLabels;
        ViewBag.ChartData = chartData;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Users()
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        ViewBag.Stats = new
        {
            TotalUsers = await _db.Users.CountAsync(),
            ActiveUsers = await _db.Users.CountAsync(u => u.Status == AccountStatus.Active),
            BlockedUsers = await _db.Users.CountAsync(u => u.Status == AccountStatus.Blocked)
        };

        ViewBag.UsersList = await _db.Users.OrderByDescending(u => u.Id).ToListAsync();
        return View();
    }
    

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(UserAccount model)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;
        
        if (!ModelState.IsValid) return View(model);
        
        var existingUser = await _db.Users.FindAsync(model.Id);
        if (existingUser == null) return NotFound();
        
        existingUser.Name = model.Name;
        existingUser.Status = model.Status;
        existingUser.CycleLength = model.CycleLength;
        existingUser.PeriodLength = model.PeriodLength;
        
        await _db.SaveChangesAsync();
        TempData["AdminMessage"] = "User updated successfully!";
        return RedirectToAction(nameof(Users));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddUser(string name, string email, string password)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return Json(new { success = false, message = "Unauthorized" });

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return Json(new { success = false, message = "All fields are required" });

        if (await _db.Users.AnyAsync(u => u.Email == email))
            return Json(new { success = false, message = "Email already registered" });

        var user = new UserAccount
        {
            Name = name,
            Email = email,
            Password = password,
            JoinDate = DateTime.UtcNow,
            Status = AccountStatus.Active
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return Json(new { success = true, message = "User added successfully!" });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BlockUser(int id)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return Json(new { success = false, message = "Unauthorized" });
        
        var user = await _db.Users.FindAsync(id);
        if (user != null)
        {
            user.Status = (user.Status == AccountStatus.Blocked) ? AccountStatus.Active : AccountStatus.Blocked;
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = $"User status changed to {user.Status}!" });
        }
        return Json(new { success = false, message = "User not found" });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return Json(new { success = false, message = "Unauthorized" });
        
        var user = await _db.Users.FindAsync(id);
        if (user != null)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "User deleted successfully!" });
        }
        return Json(new { success = false, message = "User not found" });
    }



    [HttpGet]
    public async Task<IActionResult> Content()
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        ViewBag.Stats = new
        {
            TotalArticles = await _db.Articles.CountAsync(),
            Published = await _db.Articles.CountAsync(a => a.Status == "Published"),
            Drafts = await _db.Articles.CountAsync(a => a.Status == "Draft"),
            Categories = await _db.Articles.Select(a => a.Category).Distinct().CountAsync()
        };

        ViewBag.ArticlesList = await _db.Articles.ToListAsync();
        return View();
    }
    

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditArticle(ContentArticle model)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return Json(new { success = false, message = "Unauthorized" });
        
        if (!ModelState.IsValid) return Json(new { success = false, message = "Invalid data provided" });
        
        var existing = await _db.Articles.FindAsync(model.Id);
        if (existing == null) return Json(new { success = false, message = "Article not found" });
        
        existing.Title = model.Title;
        existing.Category = model.Category;
        existing.Content = model.Content;
        existing.Status = model.Status;
        
        await _db.SaveChangesAsync();
        return Json(new { success = true, message = "Article updated successfully!" });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return Json(new { success = false, message = "Unauthorized" });
        
        var article = await _db.Articles.FindAsync(id);
        if (article != null)
        {
            _db.Articles.Remove(article);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Article deleted successfully!" });
        }
        return Json(new { success = false, message = "Article not found" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddArticle(string title, string category, string content)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return Json(new { success = false, message = "Unauthorized" });

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            return Json(new { success = false, message = "Title and Content are required" });

        var article = new ContentArticle
        {
            Title = title,
            Category = string.IsNullOrEmpty(category) ? "General" : category,
            Content = content,
            Status = "Published",
            PublishDate = DateTime.UtcNow
        };

        _db.Articles.Add(article);
        await _db.SaveChangesAsync();
        return Json(new { success = true, message = "Article added successfully!" });
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        var email = HttpContext.Session.GetString("Email");
        var admin = await _db.Admins.FirstOrDefaultAsync(a => a.Email == email);
        if (admin == null) return RedirectToAction(nameof(Login));

        return View(admin);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(AdminMember model)
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        var email = HttpContext.Session.GetString("Email");
        var admin = await _db.Admins.FirstOrDefaultAsync(a => a.Email == email);
        if (admin == null) return RedirectToAction(nameof(Login));

        if (ModelState.IsValid)
        {
            admin.Name = model.Name;
            if (!string.IsNullOrEmpty(model.Password))
            {
                admin.Password = model.Password;
            }
            await _db.SaveChangesAsync();
            HttpContext.Session.SetString("AdminName", admin.Name);
            TempData["ProfileMsg"] = "Profile updated successfully.";
        }
        
        return View(admin);
    }

    [HttpGet]
    public async Task<IActionResult> Settings(string tab = "general")
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        ViewBag.Tab = tab.ToLower();
        var settings = await _db.SystemSettings.FirstOrDefaultAsync(s => s.Id == 1) ?? new SystemSetting { Id = 1 };

        return View(settings);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveSettings(SystemSetting model, IFormFile? logo, string tab = "general")
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        if (ModelState.IsValid)
        {
            var existing = await _db.SystemSettings.FirstOrDefaultAsync(s => s.Id == 1);
            
            if (logo != null && logo.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "branding");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                var fileName = "site-logo" + Path.GetExtension(logo.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await logo.CopyToAsync(fileStream);
                }
                model.LogoPath = "/uploads/branding/" + fileName;
            }
            else if (existing != null)
            {
                model.LogoPath = existing.LogoPath;
            }

            if (existing == null)
            {
                model.Id = 1;
                _db.SystemSettings.Add(model);
            }
            else
            {
                existing.SiteName = model.SiteName;
                existing.SupportEmail = model.SupportEmail;
                existing.ContactNo = model.ContactNo;
                existing.LogoPath = model.LogoPath;
                existing.AllowNewRegistrations = model.AllowNewRegistrations;
                existing.SessionTimeout = model.SessionTimeout;
                existing.DefaultCycleLength = model.DefaultCycleLength;
                existing.EnableDiscussionDisplay = model.EnableDiscussionDisplay;
                existing.ShowTimerDashboard = model.ShowTimerDashboard;
                existing.ShowCycleSummaryCard = model.ShowCycleSummaryCard;
                existing.RestrictAdminAccess = model.RestrictAdminAccess;
            }
            await _db.SaveChangesAsync();
            await _settings.RefreshCacheAsync();
            TempData["AdminMessage"] = "Settings updated successfully.";
        }
        return RedirectToAction(nameof(Settings), new { tab });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public async Task<IActionResult> ExportUsersCsv()
    {
        var guard = EnsureAdminSession();
        if (guard != null) return guard;

        var users = await _db.Users.ToListAsync();
        var builder = new System.Text.StringBuilder();
        builder.AppendLine("Id,Name,Email,JoinDate,AgeGroup,CycleLength");

        foreach (var user in users)
        {
            builder.AppendLine($"{user.Id},{user.Name},{user.Email},{user.JoinDate:yyyy-MM-dd},{user.AgeGroup},{user.CycleLength}");
        }

        var csvBytes = System.Text.Encoding.UTF8.GetBytes(builder.ToString());
        return File(csvBytes, "text/csv", $"Users_Export_{DateTime.Now:yyyyMMdd}.csv");
    }

    private IActionResult? EnsureAdminSession()
    {
        var role = HttpContext.Session.GetString("Role");
        return string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase)
            ? null
            : RedirectToAction(nameof(Login));
    }
}
