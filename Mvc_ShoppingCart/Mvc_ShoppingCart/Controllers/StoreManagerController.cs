using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Common;
using DataAccess;
using System.Security.Cryptography;
using System.IO;
using System.Text;


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
            if(model.Name == null || model.Description ==null || model.GenreID == null || model.Price == null || model.Stock == null)
            {

                ModelState.AddModelError("", "Make sure that all fields are filled Correctly");
                return View(model);
            }
            //for image
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



            //for zip
            HttpPostedFileBase zip = Request.Files[1];
            byte[] zipSize = new byte[zip.ContentLength];
            zip.InputStream.Read(zipSize, 0, (int)zip.ContentLength);
            string prog = zip.FileName.Split('\\').Last();
            int zsize = zip.ContentLength;
            var hexString = BitConverter.ToString(zipSize);

            if (zsize > 0)
            {
                if (hexString.Replace("-", " ").StartsWith("52 61 72 21 1A 07 00") || hexString.Replace("-", " ").StartsWith("50 4B 03 04"))
                {
                  

                   

                   User u = new UserBL().GetUserByEmail(User.Identity.Name);
                   string pathIn = Server.MapPath("~/Content/productZip/" + prog.ToString());

                  

                   zip.SaveAs(Server.MapPath("~/Content/productZip/" + prog.ToString()));

                   string pathOut = Server.MapPath("~/Content/productZip/" + prog.ToString().Replace(".","Enc."));

                   HybridEncryptionClass he = new HybridEncryptionClass();
                   he.Encryption(pathIn, pathOut, u.Password);

                   string signigure = HybridEncryptionClass.SignFile(pathIn, u.PrivateKey);
                   

                   System.IO.File.Delete(pathIn);
                   string StringKey = he.AsymmetricEncryption(u.PublicKey, he.key);
                   string StringIV = he.AsymmetricEncryption(u.PublicKey, he.IV);

                  

                    try
                    {
                        new ProductBL().AddProduct(model.Name, model.Description, "/Content/productImages/" + image.ToString(), "/Content/productZip/" + prog.Replace(".", "Enc.").ToString(), Convert.ToInt32(model.Stock), Convert.ToDecimal(model.Price), User.Identity.Name, model.GenreID.ToString(),StringKey,StringIV, signigure);
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        string ex = e.Message;
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Invalid File Type");
                    return View(model);
                }
                //Save image url to database
            }
            else
            {
                ModelState.AddModelError("", "Please Insert a Zip File");
                return View(model);
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



                //for zip
                HttpPostedFileBase zip = Request.Files[1];
                byte[] zipSize = new byte[zip.ContentLength];
                zip.InputStream.Read(zipSize, 0, (int)zip.ContentLength);
                string prog = zip.FileName.Split('\\').Last();
                int zsize = zip.ContentLength;
                var hexString = BitConverter.ToString(zipSize);
                if (zsize > 0)
                {
                    if (hexString.Replace("-", " ").StartsWith("52 61 72 21 1A 07 00") || hexString.Replace("-", " ").StartsWith("50 4B 03 04"))
                    {
                        zip.SaveAs(Server.MapPath("~/Content/productZip/" + prog.ToString()));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid File Type");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please Insert a Zip File");
                    return View(model);
                }
                // TODO: Add update logic here
                //Product p = new ProductsBL().GetProductByID(id);
                //new ProductModel(id);
                new ProductBL().UpdateProduct(id, model.Name, model.Description, "/Content/productImages/" + image.ToString(), "/Content/productZip/" + prog.ToString(), Convert.ToInt32(model.Stock), Convert.ToDecimal(model.Price), User.Identity.Name, model.GenreID.ToString());

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
