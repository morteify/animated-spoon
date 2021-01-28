using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using animated_spoon.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace animated_spoon.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository productRepository;

        public AdminController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public ViewResult Index() => View(productRepository.GetProducts());

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("admin/edit/{productId:int}")]
        public ViewResult Edit(int productId)
        {
            var product = productRepository.GetProducts().FirstOrDefault((product) => product.ProductId == productId);
            return View(product);
        }



        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("Admin/AddNewProduct")]
        public ViewResult AddNewProduct()
        {
            var maxId = productRepository.GetProducts().Select(product => product.ProductId).Max() + 1;
            var newProduct = new Product { ProductId = 0 };
            return View("Edit", newProduct);
        }



        [ApiExplorerSettings(IgnoreApi = true)]
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

        [ApiExplorerSettings(IgnoreApi = true)]
        public ViewResult Create() => View("Edit", new Product());

        [ApiExplorerSettings(IgnoreApi = true)]
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
