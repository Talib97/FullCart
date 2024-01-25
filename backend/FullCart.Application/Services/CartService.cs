using FullCart.Application.Model;
using FullCart.Domain.Entities;
using FullCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Services
{
    public class CartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddItemToCartAsync(CartItemCreateDto cartItem)
        {
            await _unitOfWork.CartRepository.AddAsync(new Cart
            {
                Id = Guid.NewGuid(),
                AddedQuantity = cartItem.AddedQuantity,
                UserId = cartItem.UserId,
                InventoryId = cartItem.InventoryId
            });
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<CartDto>> GetCartItemAsync(Guid userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartItemAsync(userId);
            return cart.Select(c =>
            new CartDto(
                c.Id,
                c.InventoryItem.Id,
                c.InventoryItem.ProductName,
                c.InventoryItem.ProductDescription,
                c.InventoryItem.ProductImage,
                c.InventoryItem.ProductPrice,
                c.AddedQuantity
                )).ToList();
        }

        public async Task<int> GetCartItemCountAsync(Guid userId)
        {
            return await _unitOfWork.CartRepository.GetCartItemCountAsync(userId);
        }

        public async Task<UpdateItemQuantityResponse> UpdateCartItemQuantity(CartItemCreateDto cartItem,Guid cartId)
        {

            var inventory = await _unitOfWork.InventoryRepository.GetByIdAsync(cartItem.InventoryId);

            if (cartItem.AddedQuantity > inventory.ProductQuantity)
            {
                return new UpdateItemQuantityResponse(false,inventory.ProductQuantity);
            }


            var cart = await _unitOfWork.CartRepository.GetAsync(c =>
            c.Id == cartId &&
            c.InventoryId == cartItem.InventoryId &&
            c.UserId == cartItem.UserId);

            cart.AddedQuantity = cartItem.AddedQuantity;

            _unitOfWork.CartRepository.Update(cart);
            await _unitOfWork.SaveChangesAsync();

            return new UpdateItemQuantityResponse(true, inventory.ProductQuantity);
        }

        public async Task<List<CartDto>> RemoveCartItemAsync(Guid cartId,Guid userId)
        {
            var cart = await _unitOfWork.CartRepository.GetByIdAsync(cartId);
            _unitOfWork.CartRepository.Remove(cart);
            await _unitOfWork.SaveChangesAsync();
            return await GetCartItemAsync(userId);
        }
    }
}
