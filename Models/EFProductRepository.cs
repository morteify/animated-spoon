﻿using animated_spoon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace animated_spoon.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ProductDbContext context;

        public EFProductRepository(ProductDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;
    }
}
