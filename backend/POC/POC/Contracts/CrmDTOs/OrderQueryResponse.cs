namespace POC.Contracts.CrmDTOs
{
    // TODO: Implement CRM adapter and related classes

    public class OrderQueryResponse
    {
        public required List<OrderDto> Items { get; set; }
        public int Count { get; set; }
    }
}