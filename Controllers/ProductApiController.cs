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
        /// <param name="category"></param>  
        [HttpGet("products/{category}")]
        public ActionResult<List<Product>> List(string category)
        {
            var product = productRepository.Products.Where(product => product.ProductCategory.Name == category).Select(p => new ProductApiDTO(p));
            return Ok(product.ToList());
        }


        /// <summary>
        /// Returns a specific product by its ID.
        /// </summary>
        /// <param name="productId"></param>  
        [HttpGet("products/item/{productId:int}")]
        public ActionResult<Product> Item(int productId)
        {
            var product = productRepository.Products.Where(p => p.ProductId == productId).Select(p => new ProductApiDTO(p));
            return Ok(product);
        }

        /// <summary>
        /// Returns all products.
        /// </summary>
        [HttpGet("products")]
        public ActionResult Index()
        {
            return Ok(productRepository.Products.Select(p => new ProductApiDTO(p)).ToList());
        }

    }
}
