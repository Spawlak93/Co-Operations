using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.LocationModels
{
    public class LocationTransactionListItem
    {
        [Display(Name ="Transaction Number")]
        public int TransactionID { get; set; }

        [Display(Name = "Transaction Date")]
        public DateTimeOffset TransactionDate { get; set; }

        [Display(Name = "Transaction Total")]
        public decimal TransactionTotal { get; set; }
    }
}
