using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using POC.Infrastructure.IRepositories;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;

namespace POC.Infrastructure.Repositories;

public class CachedScreenConnectionRepository: IScreenConnectionRepository
{
    private readonly ScreenConnectionRepository _decorated;
    private readonly IDistributedCache _distributedCache;
    //private readonly ApplicationDbContext _dbContext;

    public CachedScreenConnectionRepository(ScreenConnectionRepository decorated, IDistributedCache distributedCache)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;
       // _dbContext = dbContext;
    }

    public Task AddConnectionAsync(int screenId, string connectionId)
    {
        return _decorated.AddConnectionAsync(screenId, connectionId);
    }

    public Task RemoveConnectionAsync(int screenId)
    {
        return _decorated.RemoveConnectionAsync(screenId);
    }

    public async Task<string?> GetConnectionIdByScreenIdAsync(int screenId)
    {
        string key = screenId.ToString();

        string? cachedConnectionId = await _distributedCache.GetStringAsync(key, default);

        string? connectionId;
        if (string.IsNullOrEmpty(cachedConnectionId))
        {
            connectionId = await _decorated.GetConnectionIdByScreenIdAsync(screenId);

            if (connectionId is null)
                return connectionId;
            
            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(connectionId),
                default);
            return connectionId;
        }
        
        // in case there is a deserialization to be made
        // connectionId = JsonConvert.DeserializeObject<object>(cachedConnectionId,
        // new JsonSerializerSettings
        // {
        //     ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        // });

        return cachedConnectionId;
    }

    public Task<IEnumerable<string>> GetConnectionIdsByScreenIdsAsync(IEnumerable<int> screenIds)
    {
        return _decorated.GetConnectionIdsByScreenIdsAsync(screenIds);
    }
}