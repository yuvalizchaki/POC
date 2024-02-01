namespace POC.Infrastructure.Repositories;

public class ConnectionRepository
{
    private readonly Dictionary<string, string> _ipToConnectionIdMap = new();

    public Task AddConnectionAsync(string ipAddress, string connectionId)
    {
        _ipToConnectionIdMap[ipAddress] = connectionId;
        return Task.CompletedTask;
    }
    
    public Task RemoveConnectionAsync(string ipAddress)
    {
        if ( _ipToConnectionIdMap.ContainsKey(ipAddress))
        {
            _ipToConnectionIdMap.Remove(ipAddress);
        }
        
        return Task.CompletedTask;
    }
    
    public Task<bool> IsConnectionExistsAsync(string ipAddress)
    {
        return Task.Run(() => _ipToConnectionIdMap.ContainsKey(ipAddress));
    }
    
    
    public Task<string> GetConnectionIdAsync(string ipAddress)
    {
        if (_ipToConnectionIdMap.TryGetValue(ipAddress, out string connectionId))
        {
            return Task.FromResult(connectionId);
        }
        else
        {
            return Task.FromResult<string>(null);
        }
    }
    
    
}