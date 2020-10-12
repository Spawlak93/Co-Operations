using Co_Operations.Data;
using Co_Operations.Models.LocationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Co_Operations.Services
{
    public class LocationService
    {
        private readonly string _userID;
        private ApplicationDbContext _context = new ApplicationDbContext();

        public LocationService(string userID)
        {
            _userID = userID;
        }

        public IEnumerable<LocationListItem> GetLocations()
        {
            return _context.Locations.Select(e => new LocationListItem
            {
                ID = e.ID,
                Name = e.LocationName
            }).ToList();
        }

        public bool CreateLocation(LocationCreate model)
        {
            var entity = new Location()
            {
                LocationName = model.LocationName,
                LocationCommisionPercent = (model.LocationCommisionPercent * .01),
                SalesCommisionPercent = (model.SalesCommisionPercent * .01),
                SalesTaxPercent = (model.SalesTaxPercent * .01)
            };
            _context.Locations.Add(entity);
            return _context.SaveChanges() == 1;
        }

        public LocationDetail GetLocationByID(int ID)
        {
            var entity = _context.Locations.Single(e => e.ID == ID);
            var detail = new LocationDetail
            {
                ID = entity.ID,
                FundsOnHand = entity.FundsOnHand.ToString("c"),
                LocationCommision = entity.LocationCommisionPercent.ToString("p"),
                SalesCommision = entity.SalesCommisionPercent.ToString("p"),
                SalesTax = entity.SalesTaxPercent.ToString("p"),
                LocationName = entity.LocationName
            };
            foreach (var transaction in entity.Transactions)
            {
                detail.Transactions.Add(new LocationTransactionListItem()
                {
                    TransactionID = transaction.ID,
                    TransactionDate = transaction.DateOfSale,
                    TransactionTotal = transaction.TotalSaleAmount.ToString("c")
                });
            }

            return detail;
        }

        public LocationEdit GetLocationEditbyID(int ID)
        {
            var entity = _context.Locations.Single(e => e.ID == ID);
            var edit = new LocationEdit
            {
                ID = entity.ID,
                LocationName = entity.LocationName,
                LocationCommisionPercent = entity.LocationCommisionPercent * 100,
                SalesCommisionPercent = entity.SalesCommisionPercent * 100,
                SalesTaxPercent = entity.SalesTaxPercent * 100
            };
            return edit;
        }

        public bool UpdateLaction(LocationEdit model)
        {
            var entity = _context.Locations.Single(e => e.ID == model.ID);
            entity.LocationName = model.LocationName;
            entity.LocationCommisionPercent = model.LocationCommisionPercent / 100;
            entity.SalesCommisionPercent = model.SalesCommisionPercent / 100;
            entity.SalesTaxPercent = model.SalesTaxPercent / 100;

            return _context.SaveChanges() == 1;
        }

        public bool DeleteLocation(int ID)
        {
            var entity = _context.Locations.Single(e => e.ID == ID);

            _context.Locations.Remove(entity);

            return _context.SaveChanges() == 1;
        }
    }
}
