namespace SocialMediaApp.Infrastructure.Persistence.Repository
{
    using SocialMediaApp.Application.Interfaces.Repositories;
    using StackExchange.Redis;
    using System.Threading.Tasks;

    public class RedisRepository : IRedisRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<string> GetValueAsync(string key)
        {
            IDatabase db = _redis.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task SetValueAsync(string key, string value)
        {
            IDatabase db = _redis.GetDatabase();
            await db.StringSetAsync(key, value);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            IDatabase db = _redis.GetDatabase();
            return await db.KeyExistsAsync(key);
        }

        public async Task<bool> DeleteKeyAsync(string key)
        {
            IDatabase db = _redis.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }
    }

}
