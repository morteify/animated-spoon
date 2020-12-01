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
                        Name = "Category 1"
                    },
                    new ProductCategory
                    {
                        Name = "Category 2"
                    },
                    new ProductCategory
                    {
                        Name = "Category 3"
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
                        Price = 275,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Description = "Protective and fashionable",
                        Price = 48.95m,
                        ProductCategoryId = 2,
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight",
                        Price = 19.50m,
                        ProductCategoryId = 3,
                    },
                    new Product
                    {
                        Name = "Corner Flags",
                        Description = "Give your playing field a professional touch",
                        Price = 34.95m,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Description = "Flat-packed 35,000-seat stadium",
                        Price = 79500,
                        ProductCategoryId = 3,
                    },
                    new Product
                    {
                        Name = "Thinking Cap",
                        Description = "Improve brain efficiency by 75%",
                        Price = 16,
                        ProductCategoryId = 2,
                    },
                    new Product
                    {
                        Name = "Unsteady Chair",
                        Description = "Secretly give your opponent a disadvantage",
                        Price = 29,
                        ProductCategoryId = 2,
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Price = 75,
                        ProductCategoryId = 1,
                    },
                    new Product
                    {
                        Name = "Bling-Bling King",
                        Description = "Gold-plated, diamond-studded King",
                        Price = 1200,
                        ProductCategoryId = 1,
                    }

                );
            }

            context.SaveChanges();
        }
    }
}