using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBaraholka.Domain.Entity;
using TechBaraholka.Domain.Response;

namespace TechBaraholka.Service.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<User>> GetProfile(string email);
        Task<BaseResponse<bool>> Refill(User user, int sum);
    }
}
