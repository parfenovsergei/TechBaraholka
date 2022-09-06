using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechBaraholka.Service.Interfaces;
using System.Linq;

namespace TechBaraholka.Controllers
{
    public class CartController : Controller
    {
        ICartService _cartService;
        IProductService _productService;
        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _productService.GetSpecificProduct(id);
            var response = await _cartService.AddToCart(product.Data, User.Identity.Name);
            
            return RedirectToAction("Index", "Home");
        }
    }
}
