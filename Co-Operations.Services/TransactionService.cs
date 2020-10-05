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
                decimal total = 0;
                if (e.Products != null)
                    foreach (var product in e.Products)
                    {
                        total += product.Product.Price  * product.NumberSold;
                    }
                item.TransactionTotal = total;
                return item;
            });

            return query;
        }

        public bool CreateTransaction(TransactionCreate transaction)
        {
            var entity = new Transaction()
            {
                DateOfSale = DateTime.Now,
                SellerID = _userID,
                LocationID = transaction.LocationID
            };

            _context.Transactions.Add(entity);
            _context.SaveChanges();

            foreach (var product in transaction.Products)
            {
                if (_context.Products.Where(e=> e.ProductSKU == product.ProductSKU).Count() == 1 && product.Quantity > 0)
                {
                    var productEntity = new TransactionProduct()
                    {
                        NumberSold = product.Quantity,
                        PruductSKU = product.ProductSKU,
                        TransactionId = entity.ID
                    };
                    _context.TransactionProducts.Add(productEntity);
                }
            }

            int test = _context.SaveChanges();
            return true;
        } 
        
        public TransactionDetail GetTransactionByID(int iD)
        {
            var entity = _context.Transactions.Single(e => e.ID == iD);
            var model = new TransactionDetail() { ID = entity.ID, DateOfSale = entity.DateOfSale, LocationName = entity.Location.LocationName, SellerName = entity.Seller.FullName, Total = entity.TotalSaleAmount };

            foreach(var eP in entity.Products)
            {
                model.Products.Add(new Models.TransactionProductModels.TransactionProductListItem()
                {
                    MakerName = eP.Product.Maker != null ? eP.Product.Maker.FullName : "N.A.",
                    Price = eP.Product.Price,
                    ProductSKU = eP.PruductSKU,
                    Quantity = eP.NumberSold                   
                });                
            }

            return model;
        }
    }
}
