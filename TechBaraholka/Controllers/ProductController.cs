using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using TechBaraholka.Domain.Enum;
using TechBaraholka.Domain.ViewModels.Product;
using TechBaraholka.Service.Interfaces;

namespace TechBaraholka.Controllers
{
    public class ProductController : Controller
    {

        IWebHostEnvironment _appEnvironment;
        IProductService _productService;
        public ProductController(IWebHostEnvironment appEnvironment, IProductService productService)
        {
            _appEnvironment = appEnvironment;
            _productService = productService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddProduct()
        {
            ViewBag.TV = TypeProduct.TV;
            ViewBag.SmartPhone = TypeProduct.SmartPhone;
            ViewBag.Laptop = TypeProduct.Laptop;
            ViewBag.HomeAppliances = TypeProduct.HomeAppliances;
            ViewBag.Accessory = TypeProduct.Accessory;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProduct(ProductViewModel model, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                string path = "/ProductPics/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var response = await _productService.AddProduct(model, path, User.Identity.Name);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
