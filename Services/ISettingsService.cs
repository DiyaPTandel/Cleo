using cleo.Models;

namespace cleo.Services;

public interface ISettingsService
{
    Task<SystemSetting> GetSettingsAsync();
    Task RefreshCacheAsync();
}
