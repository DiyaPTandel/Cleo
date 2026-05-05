using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using cleo.Data;
using cleo.Services;
using Microsoft.EntityFrameworkCore;

namespace cleo.Services;

public class ReminderBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ReminderBackgroundService> _logger;

    public ReminderBackgroundService(IServiceProvider serviceProvider, ILogger<ReminderBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Reminder Background Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Wait until top of next minute
                var now = DateTime.Now;
                var nextMinute = now.AddSeconds(60 - now.Second).AddMilliseconds(-now.Millisecond);
                var delay = nextMinute - DateTime.Now;
                if (delay > TimeSpan.Zero)
                    await Task.Delay(delay, stoppingToken);

                // Check every minute whether any user's reminder time matches NOW (HH:mm)
                var currentTime = DateTime.Now.ToString("HH:mm");
                var todayUtc = DateTime.UtcNow.Date;

                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<CleoDbContext>();
                var reminderService = scope.ServiceProvider.GetRequiredService<IReminderService>();
                // Get all notification settings whose DefaultReminderTime matches current minute
                var matchingSettings = await db.NotificationSettings
                    .Where(s => s.DefaultReminderTime == currentTime)
                    .ToListAsync(stoppingToken);

                foreach (var setting in matchingSettings)
                {
                    // Skip if daily check-in email was already sent today
                    if (setting.LastDailyCheckInSentDate.HasValue &&
                        setting.LastDailyCheckInSentDate.Value.Date == todayUtc)
                    {
                        _logger.LogInformation(
                            "Skipping daily check-in for user {UserId} — already sent today.", setting.UserId);
                        continue;
                    }

                    if (setting.DailyCheckInEnabled)
                    {
                        await reminderService.ProcessDailyCheckInAsync(setting.UserId);

                        setting.LastDailyCheckInSentDate = DateTime.UtcNow;
                        await db.SaveChangesAsync(stoppingToken);

                        _logger.LogInformation(
                            "Daily check-in email sent for user {UserId} at {Time}.", setting.UserId, currentTime);
                    }
                }

                // Once a day at midnight: process cycle reminders & other scheduled reminders
                if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
                {
                    await reminderService.ProcessDailyRemindersAsync();
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in reminder background service.");
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        _logger.LogInformation("Reminder Background Service stopped.");
    }
}
