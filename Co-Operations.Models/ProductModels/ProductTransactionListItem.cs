using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.ProductModels
{
    public class ProductTransactionListItem
    {
        [Display(Name = "Transaction Number")]
        public int TransactionNumber { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Display(Name ="Quantity")]
        public int Quantity { get; set; }

    }
}
