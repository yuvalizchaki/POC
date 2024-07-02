namespace POC.Infrastructure.IRepositories;

public interface IScreenConnectionRepository
{
    Task AddConnectionAsync(int screenId, string connectionId);
    Task<string?> RemoveConnectionAsync(int screenId);
    Task<string?> GetConnectionIdByScreenIdAsync(int screenId);
    Task<IEnumerable<string>> GetConnectionIdsByScreenIdsAsync(IEnumerable<int> screenIds);
    
    Task<IEnumerable<int>> GetConnectedScreensAsync();
}




    
