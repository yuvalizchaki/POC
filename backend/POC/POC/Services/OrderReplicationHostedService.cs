namespace POC.Services;

public class OrderReplicationHostedService(
    IServiceScopeFactory _serviceScopeFactory) : IHostedService, IDisposable
{
    private Timer timer;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
            orderService.FetchAndReplicateOrdersAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }
}
