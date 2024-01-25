using FullCart.Domain.Entities;
using FullCart.Domain.Interfaces;
using FullCart.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(FullCartDbContext _context) : base(_context)
        {
        }

        public async Task<List<Order>> GetOrder(Guid userId)
        {
            return await _context.Set<Order>().AsQueryable()
                .Include(o => o.OrderStatus)
                .Include(o => o.UserOrder)
                .ThenInclude(u => u.UserCart.Where(c => c.OrderId != null))
                .ThenInclude(c => c.InventoryItem)
                .Where(o => o.UserId == userId)
                .ToListAsync(); 
        }
    }
}
