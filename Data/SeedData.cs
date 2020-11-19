using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using animated_spoon.Data;

namespace animated_spoon.Models
{

    public static class SeedData
    {

        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ProductDbContext context = app.ApplicationServices
                .GetRequiredService<ProductDbContext>();
            context.Database.Migrate();

            if (!context.ProductsCategories.Any())
            {
                context.ProductsCategories.AddRange(
                    new ProductCategory
                    {
                        Name = "Test 1"
                    },
                    new ProductCategory
                    {
                        Name = "Test 2"
                    },
                    new ProductCategory
                    {
                        Name = "Test 3"
                    }
                );
            }

            context.SaveChanges();

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Category = "Watersports",
                        Price = 275,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Description = "Protective and fashionable",
                        Category = "Watersports",
                        Price = 48.95m,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight",
                        Category = "Soccer",
                        Price = 19.50m,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Corner Flags",
                        Description = "Give your playing field a professional touch",
                        Category = "Soccer",
                        Price = 34.95m,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Description = "Flat-packed 35,000-seat stadium",
                        Category = "Soccer",
                        Price = 79500,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Thinking Cap",
                        Description = "Improve brain efficiency by 75%",
                        Category = "Chess",
                        Price = 16,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Unsteady Chair",
                        Description = "Secretly give your opponent a disadvantage",
                        Category = "Chess",
                        Price = 29,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Category = "Chess",
                        Price = 75,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Bling-Bling King",
                        Description = "Gold-plated, diamond-studded King",
                        Category = "Chess",
                        Price = 1200,
                        ProductCategoryId = 1,
                    }

                );
            }

            context.SaveChanges();
        }
    }
}