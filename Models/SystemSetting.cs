using System.ComponentModel.DataAnnotations;

namespace cleo.Models;

public class SystemSetting
{
    [Key]
    public int Id { get; set; }
    
    // General Settings
    public string SiteName { get; set; } = "Cleo";
    public string SupportEmail { get; set; } = "support@cleo.com";
    public string ContactNo { get; set; } = "+91 1234567890";
    public string? LogoPath { get; set; }
    
    // Privacy Settings
    public bool AllowNewRegistrations { get; set; } = true;
    public bool SessionTimeout { get; set; } = true;
    
    // Content Settings
    public int DefaultCycleLength { get; set; } = 28;
    public bool EnableDiscussionDisplay { get; set; } = true;
    public bool ShowTimerDashboard { get; set; } = false;
    public bool ShowCycleSummaryCard { get; set; } = false;
    
    // Data Settings
    public bool RestrictAdminAccess { get; set; } = false;
}
