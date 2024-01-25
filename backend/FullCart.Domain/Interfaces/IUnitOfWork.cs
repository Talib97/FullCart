using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IInventoryRepository InventoryRepository { get; }
        ICartRepository CartRepository { get; }
        IOrderRepository OrderRepository { get; }
    }
}
