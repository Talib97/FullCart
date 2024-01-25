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
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> CreateUser(User user)
        {
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Email == email);
            return user;
        }

        public async Task<List<string>> GetUserRole(Guid userId)
        {
            return await _unitOfWork.UserRepository.GetUserRoles(userId);
        }


    }
}
