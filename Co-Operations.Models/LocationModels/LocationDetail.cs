using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.LocationModels
{
    public class LocationDetail
    {
        [Display(Name = "Location ID")]
        public int ID { get; set; }

        [Display(Name = "Location Name")]
        public string LocationName { get; set; }
        
        [Display(Name = "Funds on Hand")]
        public decimal FundsOnHand { get; set; }

        [Display(Name = "Sales Commision Percentage")]
        public double SalesCommision { get; set; }

        [Display(Name = "Location Commision Percentage")]
        public double LocationCommision { get; set; }

        [Display(Name = "Sales Tax Percentage")]
        public double SalesTax { get; set; }

        public List<LocationTransactionListItem> Transactions { get; set; } = new List<LocationTransactionListItem>();
    }
}
