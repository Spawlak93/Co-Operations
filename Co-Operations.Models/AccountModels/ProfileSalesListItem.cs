using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Models.AccountModels
{
    public class ProfileSalesListItem
    {
        public int TransactionNumber { get; set; }

        public DateTimeOffset DateOfSale { get; set; }

        public decimal SaleAmount { get; set; }

        public decimal CommisionAmount { get; set; }
    }
}
