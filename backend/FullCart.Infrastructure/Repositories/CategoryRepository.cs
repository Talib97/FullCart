using FullCart.Domain.Entities;
using FullCart.Domain.Interfaces;
using FullCart.Infrastructure.Context;
using FullCart.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(FullCartDbContext _context) : base(_context)
        {

        }
    }
}
