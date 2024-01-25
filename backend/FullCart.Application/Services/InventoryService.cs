using FullCart.Application.Model;
using FullCart.Domain.Entities;
using FullCart.Domain.Interfaces;
using FullCart.Infrastructure.Migrations;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Services
{
    public class InventoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<InventoryDto>> GetInventoryProductAsync(Guid? categoryId, string sortOrder, string searchTerm)
        {
             var dbProduct = await _unitOfWork.InventoryRepository.GetInventoryProductAsync(categoryId, sortOrder, searchTerm);
            return dbProduct.Select(p => new InventoryDto(
                p.Id,
                p.ProductName,
                p.ProductDescription,
                p.ProductImage,
                p.ProductPrice,
                new CategoryDto(p.Category.Id,p.Category.CategoryName),
                new InventoryStatusDto(p.InventoryStatus.Id,p.InventoryStatus.StatusDesc),
                p.ProductQuantity
                )).ToList();
        }


        public async Task<List<InventoryStatusDto>> GetInventoryStatusAsync()
        {
            var inventoryStatuses = await _unitOfWork.InventoryRepository.GetInventoryStatusAsync();
            return inventoryStatuses
                .Select(s => new InventoryStatusDto(s.Id,s.StatusDesc))
                .ToList();   
        }


        public async Task SaveInventoryProduct(InventoryDto inventoryProduct)
        {
            await _unitOfWork.InventoryRepository.AddAsync(new Inventory
            {
                ProductName = inventoryProduct.Name,
                ProductDescription = inventoryProduct.Description,
                ProductImage = inventoryProduct.Image,
                ProductPrice = inventoryProduct.Price,
                ProductQuantity = inventoryProduct.Quantity,
                InventoryStatusId = inventoryProduct.Status.Id,
                CategoryId = inventoryProduct.Category.Id,
            });

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task updateInventoryProduct(InventoryDto inventoryProduct)
        {

            var inventory = await _unitOfWork.InventoryRepository.GetByIdAsync(inventoryProduct.Id);

            inventory.ProductName = inventoryProduct.Name;
            inventory.ProductDescription = inventoryProduct.Description;
            inventory.ProductImage = inventoryProduct.Image;
            inventory.ProductPrice = inventoryProduct.Price;
            inventory.ProductQuantity = inventoryProduct.Quantity;
            inventory.InventoryStatusId = inventoryProduct.Status.Id;
            inventory.CategoryId = inventoryProduct.Category.Id;

           _unitOfWork.InventoryRepository.Update(inventory);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteInventoryProduct(Guid inventoryId)
        {
            var inventory = await _unitOfWork.InventoryRepository.GetByIdAsync(inventoryId);

            inventory.IsDeleted = true;

            _unitOfWork.InventoryRepository.Update(inventory);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
