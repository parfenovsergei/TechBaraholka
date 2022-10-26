using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBaraholka.Domain.Entity;
using TechBaraholka.Domain.Response;

namespace TechBaraholka.Service.Interfaces
{
    public interface ICartService
    {
        Task<BaseResponse<bool>> AddToCart(Product product, string email);
        Task<BaseResponse<bool>> DeleteProduct(int productId, string userEmail);
        Task<BaseResponse<bool>> BuyProduct(int productId, string email);
    }
}
