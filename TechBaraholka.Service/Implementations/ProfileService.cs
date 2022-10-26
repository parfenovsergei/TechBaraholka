using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBaraholka.DAL.Interfaces;
using TechBaraholka.Domain.Entity;
using TechBaraholka.Domain.Enum;
using TechBaraholka.Domain.Response;
using TechBaraholka.Service.Interfaces;

namespace TechBaraholka.Service.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IBaseRepository<User> _userRepository;
        public ProfileService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<User>> GetProfile(string email)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == email);

                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = $"Просмотрен профиль",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> Refill(User user, int sum)
        {
            try
            {
                user.Balance += sum;
                await _userRepository.Update(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Счёт пополнен",
                    StatusCode = StatusCode.InternalServerError
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
