namespace POC.Infrastructure.IRepositories;

public interface IGuestConnectionRepository
{
    Task<string> AddConnectionAsync(string connectionId);
        
    Task<bool> IsConnectionExistsAsync(string paringCode);
        
    Task<string> GetConnectionIdByCodeAsync(string paringCode, CancellationToken cancellationToken);

    Task RemoveConnectionAsync(string paringCode);

}