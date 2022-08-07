using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechBaraholka.Domain.Entity;
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

        [HttpGet]
        public async Task<IActionResult> Menu(int id)
        {
            TypeProduct typeProduct;
            switch (id)
            {
                case 0:
                    ViewBag.TypeProduct = "Телевизоры";
                    typeProduct = TypeProduct.TV;
                    break;
                case 1:
                    ViewBag.TypeProduct = "Смартфоны";
                    typeProduct = TypeProduct.SmartPhone;
                    break;
                case 2:
                    ViewBag.TypeProduct = "Ноутбуки";
                    typeProduct = TypeProduct.Laptop;
                    break;
                case 3:
                    ViewBag.TypeProduct = "Товары для дома";
                    typeProduct = TypeProduct.HomeAppliances;
                    break;
                case 4:
                    ViewBag.TypeProduct = "Акссесуары для компьютера";
                    typeProduct = TypeProduct.Accessory;
                    break;
                default:
                    return RedirectToAction("Index", "Home");
            }

            var response = await _productService.GetSpecificTypeProduct(typeProduct);
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> SpecificProduct(int id)
        {
            var response = await _productService.GetSpecificProduct(id);
            return View(response.Data);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyProducts()
        {
            var response = await _productService.GetAll(User.Identity.Name);
            return View(response.Data);
        }
    }
}
