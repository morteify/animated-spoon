using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using animated_spoon.Models;

namespace animated_spoon.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository productRepository;

        public AdminController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ViewResult Index() => View(productRepository.Products);

        [HttpGet]
        [Route("admin/edit/{productId:int}")]
        public ViewResult Edit(int productId)
        {
            var product = productRepository.Products.FirstOrDefault((product) => product.ProductId == productId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpGet]
        [Route("admin/delete/{productId:int}")]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = productRepository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was successfully deleted from the database.";
            }
            return RedirectToAction("Index");
        }
    }
}
