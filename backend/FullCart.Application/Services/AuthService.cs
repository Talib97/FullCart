using FullCart.Application.Constants;
using FullCart.Application.Model;
using FullCart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Application.Services
{
    public class AuthService
    {
        private readonly UserService _userService;
        private readonly PasswordHasherService _passwordHasherService;
        private readonly JwtProvider _jwtProvider;

        public AuthService(UserService userService, 
            PasswordHasherService passwordHasherService, 
            JwtProvider jwtProvider)
        {
            _userService = userService;
            _passwordHasherService = passwordHasherService;
            _jwtProvider = jwtProvider;
        }

        public async Task<AuthResponse> Register(UserCreateRequest request)
        {
            //generate salt
            var salt = _passwordHasherService.GenerateSalt();
            //hash password
            var hashedPassword = _passwordHasherService.HashPassword(request.Password, salt);

            var userId = Guid.NewGuid();
            //create user
            var createdUser = await _userService.CreateUser(new User
            {
                Id = userId,
                Name = request.Name,
                Email = request.Email,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        RoleId = Guid.Parse(RoleConstant.CUSTOMER)
                    }
                }
            });
            //generate token
            var token = _jwtProvider.Generate(createdUser);

            var roles = await _userService.GetUserRole(userId);

            return new AuthResponse(token,new AuthUser(createdUser.Id.ToString(), createdUser.Name, roles));
        }
        public async Task<AuthResponse> Login(UserCreateRequest request)
        {
            var user = await _userService.GetUserByEmail(request.Email);

            if (user is null)
            {
                throw new Exception($"User with given email : {request.Email} doesn't exist !");
            }

            if (!user.PasswordHash.SequenceEqual(
                _passwordHasherService.HashPassword(request.Password, user.PasswordSalt)))
            {
                throw new Exception($"Incorrect Password");
            }

            var token = _jwtProvider.Generate(user);

            var roles = await _userService.GetUserRole(user.Id);

            return new AuthResponse(token, new AuthUser(user.Id.ToString(), user.Name, roles));
        }
    }
}
