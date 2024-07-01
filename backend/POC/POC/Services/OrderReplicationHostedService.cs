namespace POC.Services;

public class OrderReplicationHostedService(
    IServiceScopeFactory _serviceScopeFactory) : IHostedService, IDisposable
{
    private Timer timer;
    private int interval = 15; //TODO PUT IT IN A PLACE MORE FITTING
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(interval));
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
