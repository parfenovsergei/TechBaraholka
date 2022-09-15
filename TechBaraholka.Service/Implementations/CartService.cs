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
        private readonly IBaseRepository<User> _userRepository;

        public CartService(IBaseRepository<Cart> cartRepository, IBaseRepository<Product> productRepository, IBaseRepository<User> userRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
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
                    Description = $"Товар добавлен в корзину.",
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

        public async Task<BaseResponse<bool>> BuyProduct(int productId, string email)
        {
            try
            {
                var buyer = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Email == email);
                var buyerCart = await _cartRepository.GetAll().FirstOrDefaultAsync(c => c.User.Email == email);
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(p => p.Id == productId);
                var seller = product.User;

                if (buyer.Balance >= product.Price)
                {
                    buyerCart.Products.Remove(product);
                    seller.Products.Remove(product);
                    buyer.Balance -= product.Price;
                    seller.Balance += product.Price;

                    await _cartRepository.Update(buyerCart);
                    await _userRepository.Update(seller);
                    await _userRepository.Update(buyer);
                    await _productRepository.Delete(product);

                    return new BaseResponse<bool>()
                    {
                        Data = true,
                        Description = $"Товар успешно приобретен.",
                        StatusCode = StatusCode.OK
                    };
                }
                else
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = $"Недостаточно средств на счёте.",
                        StatusCode = StatusCode.OK
                    };
                }

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

        public async Task<BaseResponse<bool>> DeleteProduct(int productId, string userEmail)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(p => p.Id == productId);
                var cart = await _cartRepository.GetAll().FirstOrDefaultAsync(c => c.User.Email == userEmail);

                cart.Products.Remove(product);

                await _cartRepository.Update(cart);
                await _productRepository.Update(product);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = $"Товар удален из корзину.",
                    StatusCode = StatusCode.OK
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
