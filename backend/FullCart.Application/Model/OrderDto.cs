using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Model
{
    public record OrderDto(Guid OrderId, string OrderStatus,DateTimeOffset OrderDate,List<OrderDetailDto> OrderDetail);

    public record OrderDetailDto(string Name,string Image,int OrderedQty);

    public record CreateOrderDto(Guid UserId, string ShippingAddress, decimal OrderPrice);
}
