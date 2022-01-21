using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json.Linq;

using System.Collections.Concurrent;

namespace MediatMyR;

public class EventStatus
{
    public bool IsDuplicated { get; set; }
    
	public string Key { get; set; }
}
public interface IDeduplicationSource
{
	Task<EventStatus> GetStatus(JObject ev);
	
    Task<EventStatus> Tick(string key);
}

public class MockDeduplicationSource : IDeduplicationSource
{
    private class CacheRecord
    {
        public string Key { get; set; }
        
        public DateTime Seen { get; set; }
    }

	private ILogger<MockDeduplicationSource> _logger;
    private IMemoryCache _cache;

	public MockDeduplicationSource(IMemoryCache cache, ILogger<MockDeduplicationSource> logger)
	{
        _cache = cache;
		_logger = logger;
	}

    public Task<EventStatus> GetStatus(JObject ev)
    {
        var key = ev["key"]?.Value<string>() ?? string.Empty;

        if (_cache.TryGetValue<CacheRecord>(key, out var record)
            && record.Seen + TimeSpan.FromSeconds(5) > DateTime.UtcNow)
        {
            return Task.FromResult(new EventStatus
            {
                IsDuplicated = true,
                Key = record.Key
            });
        }

        return Task.FromResult(new EventStatus
        {
            IsDuplicated = false,
            Key = key
        }); ;
    }

    public Task<EventStatus> Tick(string key)
    {
        if (_cache.TryGetValue<CacheRecord>(key, out var record))
        {
            return Task.FromResult(new EventStatus
            {
                IsDuplicated = true,
                Key = record.Key
            });
        }
        
        var cached = _cache.Set(key, new CacheRecord
        {
            Key = key,
            Seen = DateTime.UtcNow
        }, DateTime.UtcNow + TimeSpan.FromMinutes(1));
        
        return Task.FromResult(new EventStatus()
        { 
            Key = cached.Key,
            IsDuplicated = false
        });

    }
}