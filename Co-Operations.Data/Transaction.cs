using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Data
{
    public class Transaction
    {
        [Key]
        public int ID { get; set; }


        public virtual IEnumerable<TransactionProduct> Products { get; set; } = new List<TransactionProduct>();

        [ForeignKey(nameof(Seller))]
        public string SellerID { get; set; }
        public virtual ApplicationUser Seller { get; set; }

        [ForeignKey(nameof(Location))]
        public int LocationID { get; set; }
        public virtual Location Location { get; set; }

        public DateTimeOffset DateOfSale { get; set; }
    }
}
