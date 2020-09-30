using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.TransactionModels
{
    public class TransactionListItem
    {
        [Display(Name ="Transaction Number")]
        public int ID { get; set; }

        [Display(Name = "Seller Name")]
        public string SellerName { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Display(Name ="Transaction Total")]
        public decimal TransactionTotal { get; set; }

        [Display(Name ="Date of transaction")]
        public DateTimeOffset DateOfSale { get; set; }
    }
}
