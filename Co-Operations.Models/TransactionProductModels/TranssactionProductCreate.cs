using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.TransactionProductModels
{
    public class TranssactionProductCreate
    {
        [Display(Name = "Product SKU")]
        public string ProductSKU { get; set; }

        [Display(Name ="Quantity of Item")]
        public int Quantity { get; set; }
    }
}
