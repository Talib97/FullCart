using FullCart.Application.Model;
using FullCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Services
{
    public class CategoryService 
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            var dbCategory =  await _unitOfWork.CategoryRepository.GetAllAsync();
            return dbCategory.Select(c => new CategoryDto(c.Id, c.CategoryName));
        }
    }
}
