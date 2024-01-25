using FullCart.Application.Contracts;
using FullCart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<List<Cart>> GetCartItemAsync(Guid userId);
        Task<int> GetCartItemCountAsync(Guid userId);
    }
}
