﻿using Co_Operations.Data;
using Co_Operations.Models;
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
            return _context.Products.Select(e => new ProductListItem
            {
                ItemName = e.ItemName,
                SKU = e.ProductSKU,
                Price = e.Price
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
    }
}