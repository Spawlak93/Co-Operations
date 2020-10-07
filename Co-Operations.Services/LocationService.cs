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
            return new LocationDetail
            {
                ID = entity.ID,
                FundsOnHand = entity.FundsOnHand,
                LocationCommision = entity.LocationCommisionPercent * 100,
                SalesCommision = entity.SalesCommisionPercent * 100,
                SalesTax = entity.SalesTaxPercent * 100,
                LocationName = entity.LocationName
            };
        }

        public bool UpdateLaction(LocationEdit model)
        {
            var entity = _context.Locations.Single(e => e.ID == model.ID);
            entity.LocationCommisionPercent = model.LocationCommisionPercent;
            entity.LocationName = model.LocationName;
            entity.SalesCommisionPercent = model.SalesCommisionPercent;
            entity.SalesTaxPercent = model.SalesTaxPercent;

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
