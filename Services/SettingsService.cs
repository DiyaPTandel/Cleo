using cleo.Data;
using cleo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace cleo.Services;

public class SettingsService : ISettingsService
{
    private readonly CleoDbContext _db;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "SystemSettings";

    public SettingsService(CleoDbContext db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<SystemSetting> GetSettingsAsync()
    {
        if (!_cache.TryGetValue(CacheKey, out SystemSetting settings))
        {
            settings = await _db.SystemSettings.FirstOrDefaultAsync(s => s.Id == 1) 
                       ?? new SystemSetting { Id = 1 };
            
            _cache.Set(CacheKey, settings, TimeSpan.FromHours(1));
        }
        return settings;
    }

    public async Task RefreshCacheAsync()
    {
        _cache.Remove(CacheKey);
        await GetSettingsAsync();
    }
}
