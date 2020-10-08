using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.AccountModels
{
    public class ProfileProductListItem
    {
        [Display(Name = "Product SKU")]
        public string ProductSKU { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Number Sold")]
        public int NumerSold { get; set; }
    }
}
