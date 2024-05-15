using System.Collections.Concurrent;
using POC.Infrastructure.Generators;
using POC.Infrastructure.IRepositories;

namespace POC.Infrastructure.Repositories;

public class GuestConnectionRepository(ILogger<IGuestConnectionRepository> logger) : IGuestConnectionRepository
{
    private readonly ConcurrentDictionary<string, string> _pairingCodeToConnectionIdMap = new();
    
    private readonly PairCodeGenerator _pairCodeGenerator = new();
    
    public async Task<string> AddConnectionAsync(string connectionId)
    {
        var pairingCode = await _pairCodeGenerator.GenerateAsync();
        
         if (_pairingCodeToConnectionIdMap.TryAdd(pairingCode,connectionId))
         {
             logger.LogInformation($"Pairing code added to repo. Pairing code: {pairingCode}, ConnectionId: {connectionId}");
         }
         else
         {
             logger.LogInformation($"Pairing code {pairingCode} failed to be added to repo , ConnectionId: {connectionId}");
             throw new Exception("Failed to add pairing code to repo");
         }
         return pairingCode;

    }
    
    public async Task RemoveConnectionAsync(string paringCode)
    {
        if (_pairingCodeToConnectionIdMap.TryRemove(paringCode, out _))
        {
            logger.LogInformation($"Guest hub connection with pairing code: {paringCode} has been removed");
        }
        else
        {
            // Connection not found
            logger.LogWarning($"No connection found with pairing code: {paringCode}");
        }

        await Task.CompletedTask;
    }

    
    public Task<bool> IsConnectionExistsAsync(string paringCode)
    {
        return Task.Run(() => _pairingCodeToConnectionIdMap.ContainsKey(paringCode));
    }
    
    
    public Task<string?> GetConnectionIdByCodeAsync(string paringCode, CancellationToken cancellationToken)
    {
        _pairingCodeToConnectionIdMap.TryGetValue(paringCode, out string connectionId);
        return Task.FromResult(connectionId);
    }
    
}


