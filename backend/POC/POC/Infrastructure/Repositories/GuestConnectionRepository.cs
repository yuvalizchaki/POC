using POC.Infrastructure.Generators;

namespace POC.Infrastructure.Repositories;

public class GuestConnectionRepository
{
    private readonly Dictionary<string, string> _PairingCodeToConnectionIdMap = new();
    
    private readonly PairCodeGenerator _pairCodeGenerator = new();
    
    public Task AddConnectionAsync(string paringCode, string connectionId)
    {
        _PairingCodeToConnectionIdMap[paringCode] = connectionId;
        return Task.CompletedTask;
    }
    
    public Task RemoveConnectionAsync(string paringCode)
    {
        _PairingCodeToConnectionIdMap.Remove(paringCode);
        
        return Task.CompletedTask;
    }
    
    // public Task<bool> IsConnectionExistsAsync(string ipAddress)
    // {
    //     return Task.Run(() => _PairingCodeToConnectionIdMap.ContainsKey(ipAddress));
    // }
    
    
    public Task<string> GetConnectionIdByCodeAsync(string paringCode)
    {
        if (_PairingCodeToConnectionIdMap.TryGetValue(paringCode, out string connectionId))
        {
            return Task.FromResult(connectionId);
        }
        else
        {
            return Task.FromResult<string>(null);
        }
    }
    
    
}