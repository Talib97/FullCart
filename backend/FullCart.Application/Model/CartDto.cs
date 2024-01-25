using FullCart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Model
{
    public record CartDto (
       Guid CartId,
       Guid InventoryId,
       string Name,
       string Description,
       string Image,
       decimal UnitPrice,
       int AddedQty
    );



    public record CartItemCreateDto (
        Guid UserId,
        int AddedQuantity,
        Guid InventoryId
    );


    public record UpdateItemQuantityResponse(bool CanUpdate, int AvailableStockQuantity);
}
