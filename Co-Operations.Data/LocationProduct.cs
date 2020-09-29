using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Data
{
    public class LocationProduct
    {
        //To Be used in stretch goal of tracking stock
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Location))]
        public int LocationID { get; set; }
        public virtual Location Location { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Product))]
        public string ProductSKU { get; set; }
        public virtual Product Product { get; set; }

        public int NumberInStock { get; set; }
    }
}
