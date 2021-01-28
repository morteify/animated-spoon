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

        [HttpGet]
        public ViewResult List(string category)
        {
            return View(productRepository.GetProducts().Where(product => product.ProductCategory.Name == category));
        }


        [HttpGet]
        [Route("products/item/{productId:int}")]
        public ViewResult Item(int productId)
        {
            return View("List", productRepository.GetProducts().Where(p => p.ProductId == productId));
        }

        [HttpGet]
        [Route("products")]
        public IActionResult Index()
        {
            return View("List", productRepository.GetProducts());
        }

    }
}
