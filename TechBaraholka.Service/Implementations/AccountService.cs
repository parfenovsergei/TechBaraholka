using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TechBaraholka.DAL.Interfaces;
using TechBaraholka.Domain.Entity;
using TechBaraholka.Domain.Enum;
using TechBaraholka.Domain.Helpers;
using TechBaraholka.Domain.Response;
using TechBaraholka.Domain.ViewModels.Account;
using TechBaraholka.Service.Interfaces;


namespace TechBaraholka.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Cart> _cartRepository;

        public AccountService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Email 
                && x.Password == HashPasswordHelper.HashPassowrd(model.Password));
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный логин и(или) пароль"
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model, string path)
        {
            try
            {
                User newUser = new User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Address = model.Address,
                    Role = "User",
                    AvatarPath = path,
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                    Balance = 0
                };
                await _userRepository.Create(newUser);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"Пользователь {newUser.Name} {newUser.Surname} зарегистрировался",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<bool> AddNewCart(string email)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == email);
                user.Cart = new Cart()
                {
                    User = user,
                    Sum = 0
                };
                await _userRepository.Update(user);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
