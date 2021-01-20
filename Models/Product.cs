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

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(180, MinimumLength = 3)]
        [Required]
        public string Description { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [ForeignKey("ProductCategoryId")]
        public int? ProductCategoryId { get; set; }

        [DisplayFormat(NullDisplayText = "No category")]
        public virtual ProductCategory ProductCategory { get; set; }

    }
}
