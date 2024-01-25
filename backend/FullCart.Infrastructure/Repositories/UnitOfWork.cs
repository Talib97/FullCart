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
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly FullCartDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;


        public UnitOfWork(FullCartDbContext context)
        {
            _context = context;
            _userRepository = new UserRepository(context);
            _categoryRepository = new CategoryRepository(context);
            _inventoryRepository = new InventoryRepository(context);
            _cartRepository = new CartRepository(context);
            _orderRepository = new OrderRepository(context);
        }

        public IUserRepository UserRepository  => _userRepository ?? new UserRepository(_context); 
        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_context);
        public IInventoryRepository InventoryRepository => _inventoryRepository ?? new InventoryRepository(_context);
        public ICartRepository CartRepository => _cartRepository ?? new CartRepository(_context);
        public IOrderRepository OrderRepository => _orderRepository ?? new OrderRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }
    }
}
