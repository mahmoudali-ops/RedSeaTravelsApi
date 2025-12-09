using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase database;
        public CacheService(IConnectionMultiplexer _redis)
        {
            database = _redis.GetDatabase();
        }

        public async Task<string> GetCacheKeyAsync(string key)
        {
            var chachedResponse = await database.StringGetAsync(key);
            if(chachedResponse.IsNullOrEmpty) return null;
            return chachedResponse.ToString();
        }

        public async Task SetCacheKeyAsync(string key, object response, TimeSpan expireTime)
        {
            if (response is null) return ;

            var options= new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            await database.StringSetAsync(key,JsonSerializer.Serialize(response,options), expireTime);  
        }
    }
}
