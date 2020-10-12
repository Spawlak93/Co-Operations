using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models
{
    public class ProductListItem
    {
        [Display(Name ="SKU")]
        public string SKU { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Price")]
        public string Price { get; set; }

        public string MakerId { get; set; }
    }
}
