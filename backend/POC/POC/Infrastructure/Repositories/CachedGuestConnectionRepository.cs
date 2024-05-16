using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using POC.Infrastructure.IRepositories;

namespace POC.Infrastructure.Repositories;

public class CachedGuestConnectionRepository : IGuestConnectionRepository
{
    private readonly GuestConnectionRepository _decorated;
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<CachedGuestConnectionRepository> _logger;

    public CachedGuestConnectionRepository(GuestConnectionRepository decorated, 
        IDistributedCache distributedCache,
        ILogger<CachedGuestConnectionRepository> logger)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;
        _logger = logger;
    }

    public async Task<string> AddConnectionAsync(string connectionId)
    {
        try
        {
            var pairingCode = await _decorated.AddConnectionAsync(connectionId);
    
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) 
            };
    
            await _distributedCache.SetStringAsync(pairingCode, connectionId, cacheOptions);
    
            return pairingCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add connection to cache");
            throw;
        }
    }

    public async Task<string?> GetConnectionIdByCodeAsync(string pairingCode, CancellationToken cancellationToken)
    {
        try
        { 
            string? cachedConnectionId = await _distributedCache.GetStringAsync(pairingCode, cancellationToken);
           // return await _distributedCache.GetStringAsync(pairingCode, cancellationToken);
           return cachedConnectionId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve connection from cache");
            throw;
        }
    }
    
    public Task RemoveConnectionAsync(string paringCode)
    {
        return _decorated.RemoveConnectionAsync(paringCode);
    }
    
    public Task<bool> IsConnectionExistsAsync(string paringCode)
    {
        return _decorated.IsConnectionExistsAsync(paringCode);
    }
    
    // public async Task<string> GetConnectionIdByCodeAsync(string paringCode, CancellationToken cancellationToken)
    //  {
    //      string key = paringCode;
    //
    //      string? cachedConnectionId = await _distributedCache.GetStringAsync(key, cancellationToken);
    //
    //      string? connectionId;
    //      if (string.IsNullOrEmpty(cachedConnectionId))
    //      {
    //          connectionId = await _decorated.GetConnectionIdByCodeAsync(paringCode, cancellationToken);
    //
    //          if (connectionId is null)
    //              return connectionId;
    //          
    //          await _distributedCache.SetStringAsync(
    //              key,
    //              JsonConvert.SerializeObject(connectionId), cancellationToken);
    //          return connectionId;
    //      }
    //
    //      return cachedConnectionId;
    //  }
    
}


