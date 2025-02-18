using StackExchange.Redis;
using System.Text.Json;

namespace ProductService.Infrastructure.Services;

public class RedisService
{
    private readonly IDatabase _database;

    public RedisService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Redis") ?? throw new ArgumentNullException(nameof(configuration));
        var connection = ConnectionMultiplexer.Connect(connectionString);
        _database = connection.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<T>(value.ToString()) : default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var serializedValue = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}