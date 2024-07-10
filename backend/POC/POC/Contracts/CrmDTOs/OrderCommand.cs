namespace POC.Contracts.CrmDTOs;

public class OrderCommand
{
    public string cmd { get; set; }
    public BaseOrderDto order { get; set; }
}

