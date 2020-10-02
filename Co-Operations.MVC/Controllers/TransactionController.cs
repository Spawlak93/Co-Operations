using Co_Operations.Models.TransactionModels;
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
    public class TransactionController : Controller
    {
        [AllowAnonymous]
        // GET: Transaction
        public ActionResult Index()
        {
            var service = CreateTransactionService();
            var model = service.GetTransactions();
            return View(model);
        }

        //Get: Transaction/Create
        public ActionResult Create()
        {
            TempData["SaveResult"] = "TransactionCreated.";
            //Populate Viewbag
            ViewBag.Locations = PopulateLocationsList();
            //Populate TransactionProductList
            var model = new TransactionCreate();
            for(int i = 0; i < 5; i++)
                model.Products.Add(new Co_Operations.Models.TransactionProductModels.TranssactionProductCreate());
            return View(model);
        }

        //Post Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Locations = PopulateLocationsList();
                return View(model);
            }
            var service = CreateTransactionService();
            
            if (service.CreateTransaction(model))
            {
                ViewBag.SaveResult = "Transaction Added";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Transaction could not be Added");

            return View(model);

        }
        private List<SelectListItem> PopulateLocationsList()
        {
            var service = new LocationService(User.Identity.GetUserId());
            List<SelectListItem> locations = new List<SelectListItem>();
            foreach (var location in service.GetLocations())
                locations.Add(new SelectListItem { Text = location.ID + ". " + location.Name, Value = location.ID.ToString() });
            return locations;
        }
        private TransactionService CreateTransactionService()
        {
            var userID = User.Identity.GetUserId();
            var service = new TransactionService(userID);
            return service;
        }
    }
}