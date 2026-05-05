using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cleo.Data;
using cleo.Models;
using cleo.Services;

namespace cleo.Controllers;

public class RemindersController : Controller
{
    private readonly CleoDbContext _db;
    private readonly IReminderService _reminderService;

    public RemindersController(CleoDbContext db, IReminderService reminderService)
    {
        _db = db;
        _reminderService = reminderService;
    }

    [HttpGet]
    public async Task<IActionResult> Manage()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Public");

        var settings = await _db.NotificationSettings.FirstOrDefaultAsync(s => s.UserId == userId.Value);
        if (settings == null)
        {
            settings = new NotificationSetting { UserId = userId.Value };
            _db.NotificationSettings.Add(settings);
            await _db.SaveChangesAsync();
        }

        return View(settings);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveSettings(NotificationSetting settings)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Public");

        var existing = await _db.NotificationSettings.FirstOrDefaultAsync(s => s.UserId == userId.Value);
        if (existing != null)
        {
            existing.PeriodApproachingEnabled = settings.PeriodApproachingEnabled;
            existing.PeriodApproachingDays = settings.PeriodApproachingDays;
            existing.OvulationWindowEnabled = settings.OvulationWindowEnabled;
            existing.NewCycleSummaryEnabled = settings.NewCycleSummaryEnabled;
            existing.DailyCheckInEnabled = settings.DailyCheckInEnabled;

            // If the reminder time changed, reset the sent-today flag so email fires at new time
            if (existing.DefaultReminderTime != settings.DefaultReminderTime)
            {
                existing.LastDailyCheckInSentDate = null;
            }
            existing.DefaultReminderTime = settings.DefaultReminderTime;
            
            await _db.SaveChangesAsync();
            
            // Regenerate reminders based on new settings
            await _reminderService.CheckAndGenerateRemindersAsync(userId.Value);
            
            TempData["ReminderMsg"] = "Settings saved successfully!";
        }

        return RedirectToAction(nameof(Manage));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TestNotification()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return Unauthorized();

        TempData["ReminderMsg"] = "Test notification sent successfully!";
        return RedirectToAction(nameof(Manage));
    }

    /// <summary>Returns the user's daily check-in reminder time as JSON for the client scheduler.</summary>
    [HttpGet]
    public async Task<IActionResult> GetCheckInTime()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return Json(new { enabled = false, time = "" });

        var settings = await _db.NotificationSettings.FirstOrDefaultAsync(s => s.UserId == userId.Value);
        return Json(new
        {
            enabled = settings?.DailyCheckInEnabled ?? false,
            time = settings?.DefaultReminderTime ?? ""
        });
    }
}
