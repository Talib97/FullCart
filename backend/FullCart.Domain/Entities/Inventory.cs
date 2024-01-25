using FullCart.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }

        [Column(TypeName = "decimal(20, 2)")]
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public Guid? InventoryStatusId { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? IsDeleted { get; set; }    
        public virtual InventoryStatus InventoryStatus { get; set; }
        public virtual Category Category { get; set; }
        public virtual Cart Cart { get; set; }

    }
}
