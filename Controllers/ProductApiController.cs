using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using animated_spoon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Docs.Samples;
using Microsoft.AspNetCore.Http;

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
        /// Returns all products.
        /// </summary>
        [HttpGet("products")]
        public ActionResult<List<ProductApiDTO>> Products()
        {
            var products = productRepository.GetProducts();

            if (products.ToList().Count == 0 || products == null)
                return NotFound();

            return products.Select(product => new ProductApiDTO()
            {
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                ProductCategoryId = product.ProductCategory.ProductCategoryId,
                ProductCategoryName = product.ProductCategory.Name,
                ProductId = product.ProductId,
            }).ToList();
        }

        /// <summary>
        /// Returns a specific product by its ID.
        /// </summary>
        /// <param name="productId"></param>  
        [HttpGet("products/item/{productId:int}")]
        public ActionResult<ProductApiDTO> ProductItem(int productId)
        {
            var productItem = productRepository.GetProducts().Where(p => p.ProductId == productId);
            if (productItem.ToList().Count == 0 || productItem == null) return NotFound();

            var product = productItem.Select(product => new ProductApiDTO()
            {
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                ProductCategoryId = product.ProductCategory.ProductCategoryId,
                ProductCategoryName = product.ProductCategory.Name,
                ProductId = product.ProductId,
            });

            return product.FirstOrDefault();
        }

        /// <summary>
        /// Returns a list of all products from a specific category.
        /// </summary>
        /// <param name="categoryId"></param>  
        [HttpGet("products/category/{categoryId}")]
        public ActionResult<List<ProductApiDTO>> ProductsList(int categoryId)
        {
            try
            {
                var productsByCategory = productRepository.GetProducts().Where(product => product.ProductCategoryId == categoryId);

                if (productsByCategory.ToList().Count == 0)
                    return NotFound();

                var products = productsByCategory.Select(product => new ProductApiDTO()
                {
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    ProductCategoryId = product.ProductCategory.ProductCategoryId,
                    ProductCategoryName = product.ProductCategory.Name,
                    ProductId = product.ProductId,
                });
                return products.ToList();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Adds a new product in the database.
        /// </summary>
        [HttpPost("products/add")]
        public ActionResult<Product> AddProduct(Product newProduct)
        {
            var product = productRepository.SaveProduct(newProduct);
            return product;
        }


        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        [HttpPut("products/update")]
        public ActionResult<Product> UpdateProduct(Product newProduct)
        {
            var product = productRepository.SaveProduct(newProduct);
            return product;
        }

        /// <summary>
        /// Remove a selected product from the database.
        /// </summary>
        /// <param name="productId"></param>  
        [HttpDelete("products/delete/{productId}")]
        public ActionResult<Product> DeleteProduct(int productId)
        {
            var deletedProduct = productRepository.DeleteProduct(productId);
            if (deletedProduct == null)
                return NotFound();
            return deletedProduct;
        }

    }
}
