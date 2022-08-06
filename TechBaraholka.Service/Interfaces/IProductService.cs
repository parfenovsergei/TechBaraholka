using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBaraholka.Domain.Entity;
using TechBaraholka.Domain.Enum;
using TechBaraholka.Domain.Response;
using TechBaraholka.Domain.ViewModels.Product;

namespace TechBaraholka.Service.Interfaces
{
    public interface IProductService
    {
        Task<BaseResponse<Product>> AddProduct(ProductViewModel model, string path, string userName);
        Task<BaseResponse<List<Product>>> GetAll();

        Task<BaseResponse<List<Product>>> GetSpecificTypeProduct(TypeProduct productType);
        Task<BaseResponse<Product>> GetSpecificProduct(int productId);
    }
}
