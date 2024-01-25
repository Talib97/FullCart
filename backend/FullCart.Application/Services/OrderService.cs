using FullCart.Application.Model;
using FullCart.Domain.Constants;
using FullCart.Domain.Entities;
using FullCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderStatus = FullCart.Domain.Constants.OrderStatus;

namespace FullCart.Application.Services
{
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateOrder(CreateOrderDto orderRequest)
        {
            var orderId = Guid.NewGuid();


            await _unitOfWork.OrderRepository.AddAsync(new Order
            {
                Id = orderId,
                UserId = orderRequest.UserId,
                OrderPrice = orderRequest.OrderPrice,
                ShippingAddress = orderRequest.ShippingAddress,
                OrderStatusId = Guid.Parse(OrderStatus.CREATED)
            });

            var cart = await _unitOfWork.CartRepository
                .WhereAsync(c => c.UserId == orderRequest.UserId && c.OrderId == null);

            _unitOfWork.CartRepository.UpdateRange(cart.Select(c =>
            {
                c.OrderId = orderId;
                return c;
            }));

            var cartItem = await _unitOfWork.CartRepository.GetCartItemAsync(orderRequest.UserId);

            _unitOfWork.InventoryRepository.UpdateRange(cartItem.Select(c =>
            {
                c.InventoryItem.ProductQuantity = c.InventoryItem.ProductQuantity - c.AddedQuantity;
                return c.InventoryItem;
            }));

            await _unitOfWork.SaveChangesAsync();  
            
        }

        public async Task<List<OrderDto>> GetOrder(Guid userId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(userId);

            return order.Select(o => new OrderDto(
                o.Id,
                o.OrderStatus.StatusDesc,
                o.DateCreated,
                o.UserOrder.UserCart.Where(c => c.OrderId == o.Id)
                .Select(c => new OrderDetailDto(
                    c.InventoryItem.ProductName,
                    c.InventoryItem.ProductImage,
                    c.AddedQuantity
                    )).ToList()
                )).ToList();
        }

        public async Task CancelOrder(Guid orderId, CreateOrderDto orderRequest)
        {
            var order = await _unitOfWork.OrderRepository
                .GetAsync(o => o.UserId == orderRequest.UserId && o.Id == orderId);

            order.OrderStatusId = Guid.Parse(OrderStatus.CANCELLED);

             _unitOfWork.OrderRepository.Update(order);

            var cartItem = await _unitOfWork.CartRepository.GetCartItemAsync(orderRequest.UserId);

            _unitOfWork.InventoryRepository.UpdateRange(cartItem.Select(c =>
            {
                c.InventoryItem.ProductQuantity = c.InventoryItem.ProductQuantity + c.AddedQuantity;
                return c.InventoryItem;
            }));

            await _unitOfWork.SaveChangesAsync();
        }
    }


    
}
