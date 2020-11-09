using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using animated_spoon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Docs.Samples;

namespace animated_spoon.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ViewResult List(string category)  
        {
            return View(productRepository.Products.Where(product => product.Category == category));
        }

        [Route("products")]
        public IActionResult Index()
        {
            return View("List", productRepository.Products);
        }

    }
}
