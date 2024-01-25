using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Model
{
    public record InventoryDto(
        Guid Id,
        string Name,
        string Description,
        string Image,
       decimal Price,
       CategoryDto Category,
       InventoryStatusDto Status,
       int Quantity);
}
