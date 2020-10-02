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
        public string LocationName { get; set; }
        public decimal FundsOnHand { get; set; } = 0m;
        [Required]
        public double SalesCommisionPercent { get; set; }
        [Required]
        public double LocationCommisionPercent { get; set; }
        [Required]
        public double SalesTaxPercent { get; set; }

        public virtual ICollection<LocationUser> LocationUsers { get; set; } = new List<LocationUser>();

        //To Be used for stretch goal of tracking stock
        //public virtual IEnumerable<LocationProduct> Stock { get; set; }
    }
}
