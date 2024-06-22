using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using POC.Infrastructure.Generators;
using POC.Infrastructure.IRepositories;

namespace POC.Infrastructure.Repositories;

public class CachedGuestConnectionRepository : IGuestConnectionRepository
{
    //private readonly GuestConnectionRepository _decorated;
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<CachedGuestConnectionRepository> _logger;
    private readonly PairCodeGenerator _pairCodeGenerator = new();

    public CachedGuestConnectionRepository( IDistributedCache distributedCache,
        ILogger<CachedGuestConnectionRepository> logger)
    {
        //_decorated = decorated;
        _distributedCache = distributedCache;
        _logger = logger;
    }

    public async Task<string> AddConnectionAsync(string connectionId)
    {
        try
        {
            string pairingCode;
            bool  exists;
            
            do
            {
                pairingCode = await _pairCodeGenerator.GenerateAsync();
                var cachedValue = await _distributedCache.GetStringAsync(pairingCode);
                exists = cachedValue != null;
            } while (exists);
    
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
            if (cachedConnectionId != null)
            {
                // Reset the expiration time if the code is still valid
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                };
                await _distributedCache.SetStringAsync(pairingCode, cachedConnectionId, cacheOptions, cancellationToken);
            }
            return cachedConnectionId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve connection from cache");
            throw;
        }
    }
    
    public async Task RemoveConnectionAsync(string pairingCode)
    {
        try
        {
            await _distributedCache.RemoveAsync(pairingCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to remove connection from cache");
            throw;
        }
    }
    
    public async Task<bool> IsConnectionExistsAsync(string pairingCode)
    {
        var cachedValue = await _distributedCache.GetStringAsync(pairingCode);
        return cachedValue != null;
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


