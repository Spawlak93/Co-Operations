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
            var model = new TransactionCreate();
            model.Products.Add(new Co_Operations.Models.TransactionProductModels.TranssactionProductCreate);
            return View(model);
        }
        private TransactionService CreateTransactionService()
        {
            var userID = User.Identity.GetUserId();
            var service = new TransactionService(userID);
            return service;
        }
    }
}