using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechBaraholka.Models;
using TechBaraholka.DAL;
using TechBaraholka.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using TechBaraholka.Domain.Entity;

namespace TechBaraholka.Controllers
{
    public class HomeController : Controller
    {
        IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _productService.GetAll();
            return View(response.Data);
        }
    }
}
