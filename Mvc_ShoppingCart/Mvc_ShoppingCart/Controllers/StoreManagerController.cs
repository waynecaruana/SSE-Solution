using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Common;

namespace Mvc_ShoppingCart.Controllers
{
    public class StoreManagerController : Controller
    {
        //
        // GET: /StoreManager/

        public ActionResult Index()
        {
            var products = new ProductBL().ProductManagment(User.Identity.Name);
            return View(products);
        }

        //
        // GET: /StoreManager/Details/5

        public ActionResult Details(int id)
        {
            Product p = new ProductBL().GetProductsByID(id);
            return View(p);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /StoreManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /StoreManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
