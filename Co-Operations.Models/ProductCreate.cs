using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models
{
    public class ProductCreate
    {
        [Required]
        [Display(Name ="Item Name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name ="Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }        
    }
}
