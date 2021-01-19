using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace animated_spoon.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void DeleteProduct(string productId);
        void SaveProduct(Product product);
    }
}
