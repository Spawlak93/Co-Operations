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
        public string FundsOnHand { get; set; }

        [Display(Name = "Sales Commision Percentage")]
        public string SalesCommision { get; set; }

        [Display(Name = "Location Commision Percentage")]
        public string LocationCommision { get; set; }

        [Display(Name = "Sales Tax Percentage")]
        public string SalesTax { get; set; }

        public List<LocationTransactionListItem> Transactions { get; set; } = new List<LocationTransactionListItem>();
    }
}
