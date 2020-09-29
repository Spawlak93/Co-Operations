using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.LocationModels
{
    public class LocationCreate
    {
        [Required]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        [Required]
        [Range(0, 100)]
        [Display(Name = "Sales Commision Percentage")]
        public double SalesCommisionPercent { get; set; }

        [Required]
        [Range(0, 100)]

        [Display(Name = "Location Sales Commision Percentage")]
        public double LocationCommisionPercent { get; set; }

        [Required]
        [Range(0, 100)]
        [Display(Name = "Sales Tax Percentage")]
        public double SalesTaxPercent { get; set; }
    }
}
