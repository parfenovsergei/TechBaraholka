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
using TechBaraholka.Domain.ViewModels.Product;
using TechBaraholka.Service.Interfaces;

namespace TechBaraholka.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Product> _productRepository;

        public ProductService(IBaseRepository<User> userRepository, IBaseRepository<Product> productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }
        public async Task<BaseResponse<Product>> AddProduct(ProductViewModel model, string path, string userName)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == userName);
                Product newProduct = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    User = user,
                    //UserId = user.Id,
                    DateAdded = DateTime.Now,
                    TypeProduct = model.TypeProduct,
                    PicturePath = path
                };

                await _productRepository.Create(newProduct);

                return new BaseResponse<Product>()
                {
                    Description = $"Добавлен товар - {newProduct.Name}",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<Product>>> GetAll()
        {
            try
            {
                var users = await _productRepository.GetAll().OrderByDescending(u => u.DateAdded).ToListAsync();

                return new BaseResponse<List<Product>>()
                {
                    Data = users,
                    Description = "Получены все продукты из БД.",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
