using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.ProductModels
{
    public class ProductDetail
    {
        [Display(Name ="Product SKU")]
        public string ProductSKU { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name ="Maker")]
        public string MakerName { get; set; }

        public List<ProductTransactionListItem> Transactions { get; set; } = new List<ProductTransactionListItem>();
    }
}
