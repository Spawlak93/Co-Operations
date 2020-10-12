using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.TransactionModels
{
    public class TransactionDetail
    {
        [Display(Name = "Transaction Number")]
        public int ID { get; set; }

        [Display(Name ="Seller Name")]
        public string SellerName { get; set; }

        [Display(Name = "Loaction")]
        public string LocationName { get; set; }

        [Display(Name = "Date of Transaction")]
        public DateTimeOffset DateOfSale { get; set; }

        [Display (Name ="Transaction Total")]
        public string Total { get; set; }

        public List<TransactionProductModels.TransactionProductListItem> Products { get; set; } = new List<TransactionProductModels.TransactionProductListItem>();
    }
}
