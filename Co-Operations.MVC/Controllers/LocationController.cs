using Co_Operations.Models.LocationModels;
using Co_Operations.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Co_Operations.MVC.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        [AllowAnonymous]
        // GET: Location
        public ActionResult Index()
        {
            var service = CreateLocationService();
            var model = service.GetLocations();
            return View(model);
        }

        //Get: Location/Create
        public ActionResult Create()
        {
            TempData["SaveResult"] = "Location Added";
            //Putting in default percantages
            return View(new LocationCreate() { LocationCommisionPercent = 20, SalesCommisionPercent = 10 });
        }

        //Post Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.LocationCommisionPercent + model.SalesCommisionPercent + model.SalesTaxPercent >= 100)
            {
                ModelState.AddModelError("", "Combined total of taxes and commisions must be lower than 100%");
                return View(model);
            }

            var service = CreateLocationService();
            if (service.CreateLocation(model))
            {
                ViewBag.SaveResult = "Location Added";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Location could not be added.");
            return View(model);
        }

        //Get Location/Detail/{ID}
        public ActionResult Details(int id)
        {
            var service = CreateLocationService();
            var model = service.GetLocationByID(id);

            return View(model);
        }

        //Get Location/Edit/{ID}
        public ActionResult Edit(int id)
        {
            var service = CreateLocationService();
            var detail = service.GetLocationByID(id);
            var model = new LocationEdit { ID = detail.ID, LocationCommisionPercent = detail.LocationCommision, LocationName = detail.LocationName, SalesCommisionPercent = detail.SalesCommision, SalesTaxPercent = detail.SalesTax };
            return View(model);
        }

        //Post Location/Edit{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LocationEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if(model.ID != id)
            {
                ModelState.AddModelError("", "ID mismatch");
            }

            if (model.LocationCommisionPercent + model.SalesCommisionPercent + model.SalesTaxPercent >= 100)
            {
                ModelState.AddModelError("", "Combined total of taxes and commisions must be lower than 100%");
                return View(model);
            }

            var service = CreateLocationService();
            if (service.UpdateLaction(model))
            {
                ViewBag.SaveResult = "Location Updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Location could not be Updated.");
            return View(model);
        }

        //Get Location/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateLocationService();
            var model = service.GetLocationByID(id);

            return View(model);
        }

        //Post Location/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLocation(int id)
        {
            var service = CreateLocationService();
            service.DeleteLocation(id);
            TempData["SaveResult"] = "Your Product was deleted";

            return RedirectToAction("Index");
        }


        private LocationService CreateLocationService()
        {
            var userID = User.Identity.GetUserId();
            var service = new LocationService(userID);
            return service;
        }
    }
}