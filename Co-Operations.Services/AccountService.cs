using Co_Operations.Data;
using Co_Operations.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Services
{
    public class AccountService
    {
        private readonly string _userID;
        private ApplicationDbContext _context = new ApplicationDbContext();
        public AccountService(string userID)
        {
            _userID = userID;
        }

        public AccountProfile GetProfile()
        {
            var entity = _context.Users.Single(e => e.Id == _userID);
            var profile = new AccountProfile
            {
                DOB = entity.DOB,
                FundsEarned = entity.FundsEarned,
                FundsOwed = entity.FundsOwed,
                FundsPayedOut = entity.FundsPayedOut,
                Name = entity.FullName
            };
            foreach(var product in entity.Products)
            {
                profile.Products.Add(new ProfileProductListItem
                {
                    ProductSKU = product.ProductSKU,
                    ProductName = product.ItemName,
                    Price = product.Price,
                    NumerSold = NumberSoldHelper(product.Transactions),
                    CommisionsEarned = CommisionEarnedHelper(product.Transactions)
                });
            }
            foreach(var transaction in entity.Sales)
            {
                profile.Sales.Add(new ProfileSalesListItem
                {
                    SaleAmount = transaction.TotalSaleAmount,
                    DateOfSale = transaction.DateOfSale,
                    TransactionNumber = transaction.ID,
                    CommisionAmount = transaction.TotalSaleAmount * (decimal)transaction.Location.SalesCommisionPercent
                });
            }

            return profile;
        }

        public CollectCommisionDetail GetCollectCommision()
        {
            var entity = _context.Users.Single(e => e.Id == _userID);
            var model = new CollectCommisionDetail
            {
                DateOfCollection = DateTimeOffset.Now,
                AmountOwed = entity.FundsOwed
            };

            return model;
        }

        public bool PostCollectCommision(CollectCommisionDetail model)
        {
            var entity = new CollectedCommission()
            {
                UserId = _userID,
                AmountCollected = model.AmountBeingCollected,
                DateOfCollection = model.DateOfCollection
            };
            _context.CollectedCommissions.Add(entity);
            return _context.SaveChanges() == 1;
        }

        private string CommisionEarnedHelper(ICollection<TransactionProduct> transactionProducts)
        {
            decimal total = 0;
            foreach (var transaction in transactionProducts)
                total += transaction.Product.Price * (decimal)transaction.Transaction.Location.MakerCommisionPercent * transaction.NumberSold;

            return string.Format("{0:C}", total);
        }
        private int NumberSoldHelper(ICollection<TransactionProduct> transactionProducts)
        {
            int total = 0;
            foreach(var transaction in transactionProducts)
            {
                total += transaction.NumberSold;
            }
            return total;
        }
    }
}
