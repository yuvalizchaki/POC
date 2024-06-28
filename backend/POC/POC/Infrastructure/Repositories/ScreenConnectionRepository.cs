using POC.Infrastructure.IRepositories;

namespace POC.Infrastructure.Repositories;

public class ScreenConnectionRepository: IScreenConnectionRepository
    
{
    private readonly Dictionary<int, string> _screenIdToConnectionIdMap = new();

    public Task AddConnectionAsync(int screenId, string connectionId)
    {
        _screenIdToConnectionIdMap[screenId] = connectionId;
        return Task.CompletedTask;
    }

    public Task<string?> RemoveConnectionAsync(int screenId)
    {
        string? connectionId = null;
        if (_screenIdToConnectionIdMap.TryGetValue(screenId, out connectionId))
        {
            _screenIdToConnectionIdMap.Remove(screenId);
        }
        return Task.FromResult(connectionId);
    }





    public Task<string?> GetConnectionIdByScreenIdAsync(int screenId)
    {
        if (_screenIdToConnectionIdMap.TryGetValue(screenId, out string connectionId))
        {
            return Task.FromResult(connectionId);
        }
        else
        {
            return Task.FromResult<string>(null);
        }
    }

    public Task<IEnumerable<string>> GetConnectionIdsByScreenIdsAsync(IEnumerable<int> screenIds)
    {
        var connectionIds = screenIds
            .Select(id => _screenIdToConnectionIdMap.TryGetValue(id, out var connectionId) ? connectionId : null)
            .Where(id => id != null)
            .ToList();

        return Task.FromResult(connectionIds as IEnumerable<string>);
    }

    public Task<IEnumerable<int>> GetConnectedScreensAsync()
    {
        var connectedScreens = _screenIdToConnectionIdMap.Keys.ToList();
        return Task.FromResult(connectedScreens as IEnumerable<int>);
    }

}

