using Co_Operations.Data;
using Co_Operations.Models.TransactionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Services
{
    public class TransactionService
    {
        private readonly string _userID;
        private ApplicationDbContext _context = new ApplicationDbContext();

        public TransactionService(string userID)
        {
            _userID = userID;
        }

        public IEnumerable<TransactionListItem> GetTransactions()
        {
            var query = _context.Transactions.ToList().Select(e => 
            {
                var item = new TransactionListItem
                {
                    LocationName = e.Location.LocationName,
                    DateOfSale = e.DateOfSale,
                    ID = e.ID
                };
                item.SellerName = e.Seller.FullName;
                item.TransactionTotal = e.TotalSaleAmount;
                return item;
            });

            return query;
        }

        //public bool CrateTransaction
    }
}
