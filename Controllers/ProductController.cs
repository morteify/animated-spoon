using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using animated_spoon.Models;
using Microsoft.AspNetCore.Mvc;

namespace animated_spoon.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ViewResult List() => View(productRepository.Products);

        public IActionResult Index()
        {
            return View();
        }
    }
}
