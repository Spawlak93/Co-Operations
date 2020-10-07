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
        public decimal TotalFunds
        {
            get
            {
                decimal total = 0;

                foreach (var transaction in Transactions)
                    total += transaction.TotalSaleAmount * (decimal)LocationCommisionPercent;

                return total;
            }
        }

        public decimal Expenses { get; set; } = 0m;
        public decimal FundsOnHand
        {
            get
            {
                return TotalFunds - Expenses;
            }
        }
        [Required]
        public double SalesCommisionPercent { get; set; }
        [Required]
        public double LocationCommisionPercent { get; set; }

        public double MakerCommisionPercent
        {
            get
            {
                return 1 - (LocationCommisionPercent + SalesCommisionPercent);
            }
        }
        [Required]
        public double SalesTaxPercent { get; set; }

        public virtual ICollection<LocationUser> LocationUsers { get; set; } = new List<LocationUser>();

        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        //To Be used for stretch goal of tracking stock
        //public virtual IEnumerable<LocationProduct> Stock { get; set; }
    }
}
