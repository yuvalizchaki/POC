namespace POC.Infrastructure.Common.Notifiers;

public interface NotifyOnOrdersChanged
{
    Task NotifyAsync();
}