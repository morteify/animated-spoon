using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace animated_spoon.Models
{
    public class ProductCategory
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ProductCategoryId { get; set; }
        public virtual string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
