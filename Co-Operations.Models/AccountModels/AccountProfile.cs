using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.AccountModels
{
    public class AccountProfile
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [Display(Name = "Commisions Earned")]
        public decimal FundsEarned { get; set; }

        [Display(Name = "Commisions Payed Out")]
        public decimal FundsPayedOut { get; set; }

        [Display(Name = "Comisions Owed")]
        public decimal FundsOwed { get; set; }

        public string TotalSaleAmount
        {
            get
            {
                decimal total = 0;
                foreach (var sale in Sales)
                {
                    total += sale.SaleAmount;
                }
                return string.Format("{0:C}", total);
            }
        }
        public string TotalCommisionAmount
        {
            get
            {
                decimal total = 0;
                foreach (var sale in Sales)
                {
                    total += sale.CommisionAmount;
                }
                return string.Format("{0:C}", total);
            }
        }

        public List<ProfileProductListItem> Products { get; set; } = new List<ProfileProductListItem>();

        public List<ProfileSalesListItem> Sales { get; set; } = new List<ProfileSalesListItem>();
    }
}

