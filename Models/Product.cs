using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace animated_spoon.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
  
        [ForeignKey("ProductCategoryId")]
        public int? ProductCategoryId { get; set; }

        [DisplayFormat(NullDisplayText = "No category")]
        public virtual ProductCategory ProductCategory { get; set; }

    }
}
