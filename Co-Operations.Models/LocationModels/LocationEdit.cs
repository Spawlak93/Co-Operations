using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.LocationModels
{
    public class LocationEdit
    {
        [Required]
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        [Required]
        [Display(Name = "Sales Commision Percentage")]
        public double SalesCommisionPercent { get; set; }

        [Required]
        [Display(Name = "Location Sales Commision")]
        public double LocationCommisionPercent { get; set; }

        [Required]
        [Display(Name = "Sales Tax Percentage")]
        public double SalesTaxPercent { get; set; }

        [Required]
        [Display(Name = "Funds On Hand")]
        public decimal FundsOnHand { get; set; }
    }
}
