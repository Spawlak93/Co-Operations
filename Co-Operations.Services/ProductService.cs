using Co_Operations.Data;
using Co_Operations.Models;
using Co_Operations.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Services
{
    public class ProductService
    {
        private readonly string _userID;
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ProductService(string userID)
        {
            _userID = userID;
        }

        public IEnumerable<ProductListItem> GetProducts()
        {
            return _context.Products.ToList().Select(e =>
            {
                var item = new ProductListItem
                {
                    ItemName = e.ItemName,
                    SKU = e.ProductSKU,
                    Price = e.Price.ToString("c"),
                    MakerId = e.MakerID
                };
                return item;
            }).ToList();
        }

        public bool CreateProduct(ProductCreate model)
        {
            var entity = new Product(model.ItemName)
            {
                MakerID = _userID,
                Description = model.Description,
                Price = model.Price
            };

            _context.Products.Add(entity);
            return _context.SaveChanges() == 1;
        }

        public ProductDetail GetProductBySKU(string SKU)
        {
            var entity = _context.Products.Single(e => e.ProductSKU == SKU);
            var detail = new ProductDetail
            {
                ProductSKU = entity.ProductSKU,
                ItemName = entity.ItemName,
                Price = entity.Price.ToString("c"),
                Description = entity.Description,
                MakerName = entity.Maker != null ? entity.Maker.FullName : "NA"
            };

            foreach(var transaction in entity.Transactions)
            {
                detail.Transactions.Add(new ProductTransactionListItem()
                {
                    LocationName = transaction.Transaction.Location.LocationName,
                    Quantity = transaction.NumberSold,
                    TransactionNumber = transaction.TransactionId
                });
            }

            return detail;
        }

        public ProductEdit GetProductEditBySKU(string SKU)
        {
            var entity = _context.Products.Single(e => e.ProductSKU == SKU);
            var edit = new ProductEdit
            {
                ProductSKU = entity.ProductSKU,
                ItemName = entity.ItemName,
                Price = entity.Price,
                Description = entity.Description,
                MakerID = entity.MakerID
            };

            return edit;
        }

        public bool UpdateProduct(ProductEdit model)
        {
            var entity = _context.Products.Single(e => e.ProductSKU == model.ProductSKU && e.MakerID == _userID);
            entity.ItemName = model.ItemName;
            entity.Description = model.Description;
            entity.Price = model.Price;

            return _context.SaveChanges() == 1;
        }

        public bool DeleteNote(string SKU)
        {
            var entity = _context.Products.Single(e => e.ProductSKU == SKU && e.MakerID == _userID);

            _context.Products.Remove(entity);

            return _context.SaveChanges() == 1;
        }
    }
}
