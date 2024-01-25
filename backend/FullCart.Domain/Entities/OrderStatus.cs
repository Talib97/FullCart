using FullCart.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Entities
{
    public class OrderStatus : BaseEntity
    {
        public string StatusDesc { get; set; }  
        public virtual List<Order> Order { get; set; }
    }
}
