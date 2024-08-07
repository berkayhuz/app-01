using StackExchange.Redis;

namespace Authentication.API.Helpers.TokenHelpers
{
    public class RedisTokenStorageService : ITokenStorageService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisTokenStorageService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task StoreTokenAsync(Guid userId, string token)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(userId.ToString(), token);
        }

        public async Task<string> GetTokenAsync(Guid userId)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(userId.ToString());
        }
    }

}
