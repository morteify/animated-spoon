using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using animated_spoon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace animated_spoon.Data
{
    public class ProductDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductsCategories { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
    }
}
