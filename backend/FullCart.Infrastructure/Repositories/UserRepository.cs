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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(FullCartDbContext _context) : base(_context)
        {
        }

        public async Task<List<string>> GetUserRoles(Guid userId)
        {
            return await _context.Set<User>()
                .AsQueryable()
                .Include(u => u.UserRoles)
                .ThenInclude(r => r.Role)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.UserRoles)
                .Select(u => u.Role.Name)
                .ToListAsync();
        }
    }
}
