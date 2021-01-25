using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using animated_spoon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Docs.Samples;

namespace animated_spoon.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductApiController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        /// <summary>
        /// Returns a list of all products from a specific category.
        /// </summary>
        /// <param name="categoryId"></param>  
        [HttpGet("products/category/{categoryId}")]
        public ActionResult<List<Product>> ProductsList(int categoryId)
        {
            var product = productRepository.Products.Where(product => product.ProductCategory.ProductCategoryId == categoryId).Select(product => new ProductApiDTO()
            {
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                ProductCategoryId = product.ProductCategory.ProductCategoryId,
                ProductCategoryName = product.ProductCategory.Name,
                ProductId = product.ProductId,
            });
            return Ok(product.ToList());
        }


        /// <summary>
        /// Returns a specific product by its ID.
        /// </summary>
        /// <param name="productId"></param>  
        [HttpGet("products/item/{productId:int}")]
        public ActionResult<Product> ProductItem(int productId)
        {
            var product = productRepository.Products.Where(p => p.ProductId == productId).Select(product => new ProductApiDTO()
            {
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                ProductCategoryId = product.ProductCategory.ProductCategoryId,
                ProductCategoryName = product.ProductCategory.Name,
                ProductId = product.ProductId,
            });
            return Ok(product);
        }

        /// <summary>
        /// Returns all products.
        /// </summary>
        [HttpGet("products")]
        public ActionResult Products()
        {
            return Ok(productRepository.Products.Select(product => new ProductApiDTO()
            {
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                ProductCategoryId = product.ProductCategory.ProductCategoryId,
                ProductCategoryName = product.ProductCategory.Name,
                ProductId = product.ProductId,
            }).ToList());
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        [HttpPut("products/add")]
        public IActionResult AddProject(Product newProject)
        {

            productRepository.SaveProduct(newProject);
            return Ok();

        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="productId"></param>  
        [HttpDelete("products/delete/{productId}")]
        public IActionResult DeleteProject(int productId)
        {

            productRepository.DeleteProduct(productId);
            return Ok();

        }

    }
}
