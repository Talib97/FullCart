using FullCart.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid? UserId { get; set; }
        public string ShippingAddress { get; set; } //this should be a structured address but for simplicity stroring as string

        [Column(TypeName = "decimal(20, 2)")]
        public decimal OrderPrice { get; set; }
        public Guid? OrderStatusId {  get; set; }
        public virtual User UserOrder {  get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }
}
