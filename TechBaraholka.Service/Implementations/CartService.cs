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
    public class CartService : ICartService
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<Cart> _cartRepository;
        public CartService(IBaseRepository<Cart> cartRepository, IBaseRepository<Product> productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
        public async Task<BaseResponse<bool>> AddToCart(Product product, string email)
        {
            try
            {
                var cart = await _cartRepository.GetAll().FirstOrDefaultAsync(c => c.User.Email == email);

                product.Cart = cart;
                await _productRepository.Update(product);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = $"Товар {product.Name} добавлен в корзину.",
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
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
