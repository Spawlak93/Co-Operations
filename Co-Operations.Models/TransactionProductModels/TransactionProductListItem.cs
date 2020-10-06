using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.TransactionProductModels
{
    public class TransactionProductListItem
    {
        [Display(Name ="Product SKU")]
        public string ProductSKU { get; set; }

        [Display(Name ="Product Price")]
        public decimal Price { get; set; }

        [Display(Name ="Product Quantity")]
        public int Quantity { get; set; }

        [Display(Name ="Maker Name")]
        public string MakerName { get; set; }
    }
}
