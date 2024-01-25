using FullCart.Domain.Entities;
using FullCart.Domain.Interfaces;
using FullCart.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Infrastructure.Repositories
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(FullCartDbContext _context) : base(_context)
        {
        }

        public async Task<List<Inventory>> GetInventoryProductAsync(Guid? categoryId,string sortOrder,string searchTerm)
        {
            var inventoryQuery = _context.Set<Inventory>().AsQueryable()
                .Where(i => (i.IsDeleted.HasValue && !i.IsDeleted.Value) || i.IsDeleted == null);

            if (categoryId != null )
            {
                inventoryQuery = inventoryQuery.Where(i => i.CategoryId == categoryId);
            }

            if (sortOrder != null ) 
            {
                if (sortOrder == "ascending")  inventoryQuery = inventoryQuery.OrderBy(i => i.ProductPrice);
                if (sortOrder == "descending") inventoryQuery = inventoryQuery.OrderByDescending(i => i.ProductPrice);
            }

            if (searchTerm != null)
            {
                inventoryQuery = inventoryQuery
                .Where(i => i.ProductName.Contains(searchTerm));
            }

            return await inventoryQuery
                .Include(i => i.Category)
                .Include(i => i.InventoryStatus)
                .ToListAsync();  
        }

        public async Task<List<InventoryStatus>> GetInventoryStatusAsync()
        {
            return await _context.Set<InventoryStatus>().ToListAsync(); 
        }

        
    }
}
