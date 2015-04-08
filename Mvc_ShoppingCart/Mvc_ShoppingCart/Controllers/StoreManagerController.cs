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
        public ActionResult Create(Product model)
        {
            HttpPostedFileBase file = Request.Files[0];
            byte[] imageSize = new byte[file.ContentLength];
            file.InputStream.Read(imageSize, 0, (int)file.ContentLength);
            string image = file.FileName.Split('\\').Last();
            int size = file.ContentLength;

            if (size > 0)
            {
                file.SaveAs(Server.MapPath("~/Content/productImages/" + image.ToString()));
                //Save image url to database
            }
            else
            {
                image = "na.jpg";
            }

            try
            {
                new ProductBL().AddProduct(model.Name, model.Description, "/Content/productImages/"+image.ToString(), Convert.ToInt32(model.Stock), Convert.ToDecimal(model.Price), User.Identity.Name,model.GenreID.ToString());
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                string ex = e.Message;
                return View();
            }
        
        }
        
        //
        // GET: /StoreManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Product p = new ProductBL().GetProductsByID(id);
            return View(p);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Product model)
        {
            try
            {

                HttpPostedFileBase file = Request.Files[0];
                byte[] imageSize = new byte[file.ContentLength];
                file.InputStream.Read(imageSize, 0, (int)file.ContentLength);
                string image = file.FileName.Split('\\').Last();
                int size = file.ContentLength;

                if (size > 0)
                {
                    file.SaveAs(Server.MapPath("~/Content/images/" + image.ToString()));
                    //Save image url to database
                }
                else
                {
                    image = "na.jpg";
                }
                // TODO: Add update logic here
                //Product p = new ProductsBL().GetProductByID(id);
                //new ProductModel(id);
                new ProductBL().UpdateProduct(id,model.Name, model.Description, "/Content/productImages/" + image.ToString(), Convert.ToInt32(model.Stock), Convert.ToDecimal(model.Price), User.Identity.Name, model.GenreID.ToString());

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
            new ProductBL().DeleteProduct(id);
            return RedirectToAction("Index", "StoreManager");
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
