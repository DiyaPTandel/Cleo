using System.ComponentModel.DataAnnotations;

namespace cleo.Models;

public class NotificationSetting
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    // Cycle Predictions
    public bool PeriodApproachingEnabled { get; set; } = true;
    public int PeriodApproachingDays { get; set; } = 2;
    
    public bool OvulationWindowEnabled { get; set; } = true;
    
    public bool NewCycleSummaryEnabled { get; set; } = true;
    
    // Daily Habits
    public bool DailyCheckInEnabled { get; set; } = true;
    
    // Notification Preferences
    public string DefaultReminderTime { get; set; } = "13:25";
    
    // Tracking — prevents duplicate sends
    public DateTime? LastDailyCheckInSentDate { get; set; } = null;
}
