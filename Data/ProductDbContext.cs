using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using animated_spoon.Models;


namespace animated_spoon.Data
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public ProductDbContext (DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
    }
}
