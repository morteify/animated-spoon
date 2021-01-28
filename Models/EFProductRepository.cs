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

        public EFProductRepository() { }

        public virtual IQueryable<Product> GetProducts()
        {
            return context.Products.Include(entity => entity.ProductCategory);
        }

        public virtual Product SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
                context.SaveChanges();
                return product;
            }
            else
            {
                Product dbEntry = context.Products
                    .FirstOrDefault(p => p.ProductId == product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.ProductCategory = product.ProductCategory;
                }
                context.SaveChanges();
                return dbEntry;
            }
        }

        public virtual Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products
               .FirstOrDefault(p => p.ProductId == productId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
