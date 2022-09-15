using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechBaraholka.Service.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace TechBaraholka.Controllers
{
    public class CartController : Controller
    {
        ICartService _cartService;
        IProductService _productService;
        IProfileService _profileService;
        public CartController(ICartService cartService, IProductService productService, IProfileService profileService)
        {
            _cartService = cartService;
            _productService = productService;
            _profileService = profileService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _productService.GetSpecificProduct(id);
            var response = await _cartService.AddToCart(product.Data, User.Identity.Name);

            return RedirectToAction("CartMenu", "Cart");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CartMenu()
        {
            ViewBag.Sum = 0;
            var userResponse = await _profileService.GetProfile(User.Identity.Name);
            var productsResponse = await _productService.GetMyCartProducts(userResponse.Data.Id);

            return View(productsResponse.Data);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _cartService.DeleteProduct(id, User.Identity.Name);

            return RedirectToAction("CartMenu", "Cart");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> BuyProduct(int id)
        {
            var response = await _cartService.BuyProduct(id, User.Identity.Name);

            return RedirectToAction("CartMenu", "Cart");

        }
    }
}
