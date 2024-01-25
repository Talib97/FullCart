using FullCart.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public int AddedQuantity { get; set; }
        public Guid? UserId { get; set; }
        public Guid? InventoryId { get; set; }
        public Guid? OrderId { get; set; }
        public virtual User CartUser { get; set; }
        public virtual Inventory InventoryItem { get; set; }
    }
}
