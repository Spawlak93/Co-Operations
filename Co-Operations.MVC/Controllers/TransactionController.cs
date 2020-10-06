using Co_Operations.Models.TransactionModels;
using Co_Operations.Models.TransactionProductModels;
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

            return View(model);
        }

        //Post Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionCreate model)
        {
            for (int i = 0; i <= Request.Form.Count; i++)
            {
                var ProductSKU = Request.Form["ProductSKU[" + i + "]"];
                var Quantitystring = Request.Form["Quantity[" + i + "]"];
                if (!string.IsNullOrEmpty(ProductSKU) && int.TryParse(Quantitystring, out int Quantity))
                {
                    //If transaction already contains the product add to the quantity
                    if (model.Products.Where(m => m.ProductSKU == ProductSKU).Count() == 1)
                        model.Products.Single(m => m.ProductSKU == ProductSKU).Quantity += Quantity;

                    //Else add new TransactionProduct model
                    else
                        model.Products.Add(new TranssactionProductCreate { ProductSKU = ProductSKU, Quantity = Quantity });
                }

            }

            if (!ModelState.IsValid)
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
            ViewBag.Locations = PopulateLocationsList();
            return View(model);

        }

        //Get Transaction/Detail/{ID}
        public ActionResult Details(int id)
        {
            var service = CreateTransactionService();
            var model = service.GetTransactionByID(id);

            return View(model);
        }

        //Get Transaction/Edit/{ID}
        public ActionResult Edit(int id)
        {
            ViewBag.Locations = PopulateLocationsList();
            var service = CreateTransactionService();
            var model = service.GetTransactionEdit(id);
            return View(model);
        }

        //Post Transaction/Edit/{ID}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransactionEdit model)
        {
            var service = CreateTransactionService();
            for (int i = 0; i <= Request.Form.Count; i++)
            {
                var ProductSKU = Request.Form["ProductSKU[" + i + "]"];
                var Quantitystring = Request.Form["Quantity[" + i + "]"];
                if (!string.IsNullOrEmpty(ProductSKU) && int.TryParse(Quantitystring, out int Quantity))
                {
                    //If transaction already contains the product add it to the quantity
                    if (model.Products.Where(m => m.ProductSKU == ProductSKU).Count() == 1)
                        model.Products.Single(m => m.ProductSKU == ProductSKU).Quantity += Quantity;

                    //Else add new TransactionProduct model
                    else
                        model.Products.Add(new TransactionProductEdit { ProductSKU = ProductSKU, Quantity = Quantity });
                }

            }

            if (!ModelState.IsValid)
            {
                ViewBag.Locations = PopulateLocationsList();
                return View(model);
            }

            if (service.UpdateTransaction(model))
            {
                ViewBag.SaveResult = "Transaction updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Transaction could not be updated");
            ViewBag.Locations = PopulateLocationsList();
            return View(model);
        }


        //Get Transaction/Delete/{ID}
        public ActionResult Delete(int id)
        {
            var service = CreateTransactionService();
            var model = service.GetTransactionByID(id);

            return View(model);
        }

        //Post Transaction/Delete/{ID}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTransaction(int id)
        {
            var service = CreateTransactionService();

            service.DeleteTransaction(id);
            TempData["SaveResult"] = "The transaction was deleted";

            return RedirectToAction("Index");
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