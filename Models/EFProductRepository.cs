using animated_spoon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace animated_spoon.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ProductDbContext context;

        public EFProductRepository(ProductDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products.Include(entity => entity.ProductCategory);

        public void SaveProduct(Product product)
        {
            context.Products.Add(product);
        }

        public Product DeleteProduct(int productId)
        {
            Product product = context.Products.FirstOrDefault(p => p.ProductId == productId);
            context.Products.Remove(product);
            return product;
        }
    }
}
