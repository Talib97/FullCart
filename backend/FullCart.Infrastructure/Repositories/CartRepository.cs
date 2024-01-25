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
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(FullCartDbContext _context) : base(_context)
        {
        }

        public async Task<List<Cart>> GetCartItemAsync(Guid userId)
        {
            return await _context.Set<Cart>().AsQueryable()
                .Where(c => c.UserId == userId && c.OrderId == null)
                .Include(c => c.InventoryItem)
                .ToListAsync();
        }

        public async Task<int> GetCartItemCountAsync(Guid userId)
        {
            return await _context.Set<Cart>().AsQueryable()
                .Where(c => c.UserId == userId && c.OrderId == null)
                .CountAsync();
        }

    }
}
