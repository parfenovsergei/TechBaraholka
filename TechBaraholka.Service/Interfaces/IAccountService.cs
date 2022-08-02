using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechBaraholka.Domain.Response;
using TechBaraholka.Domain.ViewModels.Account;

namespace TechBaraholka.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model, string path);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<bool> CheckEmail(string email);
    }
}
