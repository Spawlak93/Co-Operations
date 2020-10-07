using Co_Operations.Models;
using Co_Operations.Models.ProductModels;
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
    public class ProductController : Controller
    {
        [AllowAnonymous]
        // GET: Product
        public ActionResult Index()
        {
            var service = CreateProductService();
            var model = service.GetProducts();
            return View(model);
        }

        //Get: Product/Create
        public ActionResult Create()
        {
            TempData["SaveResult"] = "Product Added.";
            return View();
        }
        //Post: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCreate model)
        {
            var service = CreateProductService();

            if (!ModelState.IsValid)
                return View(model);

            if(service.CreateProduct(model))
            {
                ViewBag.SaveResult = "Product Added";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Product could not be created.");

            return View(model);
        }
        //GET: Product/Detail/{SKU}
        public ActionResult Details(string id)
        {
            if (id is null)
                return RedirectToAction("Index");
            var service = CreateProductService();
            var model = service.GetProductBySKU(id);

            return View(model);
        }

        //Get Product/Edit/{SKU}
        public ActionResult Edit(string id)
        {
            var service = CreateProductService();
            var detail = service.GetProductBySKU(id);
            var model = new ProductEdit { ProductSKU = detail.ProductSKU, Description = detail.Description, ItemName = detail.ItemName, Price = detail.Price };
            return View(model);
        }

        //Post Product/Edit/{SKU}
        [HttpPost]
        public ActionResult Edit(string id, ProductEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ProductSKU != id)
            {
                ModelState.AddModelError("", "SKU Mismatch");
                return View(model);
            }
            var service = CreateProductService();

            if (service.UpdateProduct(model))
            {
                TempData["SaveResult"] = "Your Product was updated";
                return RedirectToAction("index");
            }

            ModelState.AddModelError("", "Product could not be updated");
            return View(model);
        }
        //Get Product/Delete/{SKU}
        [ActionName("Delete")]
        public ActionResult Delete(string id)
        {
            var service = CreateProductService();
            var model = service.GetProductBySKU(id);

            return View(model);
        }

        //Post Product/Delete/{SKU}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(string id)
        {
            var service = CreateProductService();

            service.DeleteNote(id);

            TempData["SaveResult"] = "Your Product was deleted";

            return RedirectToAction("Index");
        }

        private ProductService CreateProductService()
        {
            var userID = User.Identity.GetUserId();
            var service = new ProductService(userID);
            return service;
        }
    }
}