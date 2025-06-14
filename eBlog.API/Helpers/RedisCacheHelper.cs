using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace eBlog.Application.Helpers
{
    public class RedisCacheHelper
    {
        private readonly IDistributedCache _cache;

        public RedisCacheHelper(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetAsync<T>(string key, T value, int durationMinutes = 30)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, serializedValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(durationMinutes)
            });
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var serializedValue = await _cache.GetStringAsync(key);
            if (serializedValue == null)
                return default;

            return JsonSerializer.Deserialize<T>(serializedValue);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
