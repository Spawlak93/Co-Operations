using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Data
{
    public class Location
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public decimal FundsOnHand { get; set; }
        [Required]
        public double SalesCommisionPercent { get; set; }
        [Required]
        public double LocationCommisionPercent { get; set; }
        [Required]
        public double SalesTaxPercent { get; set; }

        public virtual IEnumerable<LocationUser> LocationUsers { get; set; }

        //To Be used for stretch goal of tracking stock
        //public virtual IEnumerable<LocationProduct> Stock { get; set; }
    }
}
