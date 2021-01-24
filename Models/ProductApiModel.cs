using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace animated_spoon.Models
{
    public class ProductApiDTO
    {
        private Product product;
        public ProductApiDTO(Product product)
        {
            this.product = product;
        }
        public int ProductId { get => product.ProductId; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get => product.Name; }

        [StringLength(180, MinimumLength = 3)]
        [Required]
        public string Description { get => product.Description; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get => product.Price; }
        public int ProductCategoryId { get => product.ProductCategory.ProductCategoryId; }

        public string ProductCategoryName { get => product.ProductCategory.Name; }

    }
}
