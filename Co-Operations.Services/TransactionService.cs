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
        
        public TransactionDetail GetTransactionByID(int id)
        {
            var entity = _context.Transactions.Single(e => e.ID == id);
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

        public TransactionEdit GetTransactionEdit(int id)
        {
            var entity = _context.Transactions.Single(e => e.ID == id);
            var model = new TransactionEdit { TransactionID = entity.ID, LocationID = entity.LocationID };

            foreach (var product in entity.Products)
            {
                model.Products.Add(new Models.TransactionProductModels.TransactionProductEdit()
                {
                    Quantity = product.NumberSold,
                    ProductSKU = product.PruductSKU
                });
            }

            return model;
        }

        public bool UpdateTransaction(TransactionEdit model)
        {
            var entity = _context.Transactions.Single(e => e.ID == model.TransactionID);

            entity.LocationID = model.LocationID;

            foreach (var product in entity.Products)
            {
                //if product is in the new transaction update quantity
                if (model.Products.Where(m => m.ProductSKU == product.PruductSKU).Count() == 1)
                {
                    product.NumberSold = model.Products.Single(m => m.ProductSKU == product.PruductSKU).Quantity;
                }
                //else set quantity to 0
                else
                {
                    product.NumberSold = 0;
                }
            }

            while(entity.Products.Where(e => e.NumberSold == 0).Count() > 0)
                _context.TransactionProducts.Remove(entity.Products.Where(e => e.NumberSold == 0).FirstOrDefault());

            foreach(var product in model.Products)
            {
                //If entity contains the product continue
                if (entity.Products.Where(e => e.PruductSKU == product.ProductSKU).Count() == 1)
                    continue;

                else
                {
                    //If the product exists make new TransactionProductTable
                    if (_context.Products.Where(e => e.ProductSKU == product.ProductSKU).Count() == 1 && product.Quantity > 0)
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
            }

            int test = _context.SaveChanges();
            return true;
        }

        public bool DeleteTransaction(int id)
        {
            var entity = _context.Transactions.Single(e => e.ID == id);

            _context.Transactions.Remove(entity);

            return _context.SaveChanges() == 1;
        }
    }
}
