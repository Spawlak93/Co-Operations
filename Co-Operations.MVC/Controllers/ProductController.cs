using Co_Operations.Models;
using Co_Operations.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Co_Operations.MVC.Controllers
{
    public class ProductController : Controller
    {

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
        //GET: Product/Detail
        //public ActionResult Details(int id)
        //{
        //    var service = CreateProductService();
        //    var model = service.GetProductByID(id);

        //    return model;
        //}
        private ProductService CreateProductService()
        {
            var userID = User.Identity.GetUserId();
            var service = new ProductService(userID);
            return service;
        }
    }
}