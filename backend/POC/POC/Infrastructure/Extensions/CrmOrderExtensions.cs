using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common.Constants;

namespace POC.Infrastructure.Extensions;

public static class CrmOrderExtensions
{
    public static OrderDto ToIncomingOrderDto(this CrmOrder crmOrder)
    {
        return new OrderDto
        {
            CrmOrder = crmOrder,
            TransportType = OrderTransportType.Incoming,
        };
    }

    public static OrderDto ToOutgoingOrderDto(this CrmOrder crmOrder)
    {
        return new OrderDto
        {
            CrmOrder = crmOrder,
            TransportType = OrderTransportType.Outgoing,
        };
    }

    public static List<OrderDto> ToOrderDtoList(this IEnumerable<CrmOrder> crmOrders)
    {
        return crmOrders.SelectMany(crmOrder => new List<OrderDto>
        {
            crmOrder.ToIncomingOrderDto(),
            crmOrder.ToOutgoingOrderDto()
        }).ToList();
    }
}
