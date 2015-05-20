using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLogic;
using System.IO;
using System.Security.Cryptography;
using DataAccess;
using System.Text;

namespace Mvc_ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            List<ShoppingCart> allProducts = new ShoppingCartBL().GetShoppingCartItemsByUsername(User.Identity.Name).ToList();
            return View("Index", allProducts);
        }

        /// <summary>
        /// This method is used to:
        /// 1. Add To shopping Cart
        /// </summary>
        /// <param name="qty">qty bought</param>
        /// <returns>view</returns>
        /// 
        [Authorize]
        public ActionResult AddToShoppingCart(int Quantity = 1)
        {


            ShoppingCart sc = new ShoppingCart();
            if (string.IsNullOrEmpty("Quantity"))
            {
            }
            else if (Quantity == 0)
            {
                ViewBag.Msg = "Please Enter a valid Quantity";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                List<Order> list = new ShoppingCartBL().GetOrdersByUsername(User.Identity.Name).ToList();
                List<OrderDetail> odList = new List<OrderDetail>();

                foreach (Order o in list)
                {
                    odList.AddRange(new ShoppingCartBL().GetOrderDetailsByOrderId(o.ID));
                }

                //if(shopp)
                sc.Email = User.Identity.Name;
                sc.Qty = Quantity;
                sc.ProductID = Convert.ToInt32(Session["ProductID"]);

                foreach (OrderDetail od in odList)
                {
                    if (od.ProductID == sc.ProductID)
                    {
                        ViewBag.Msg = "Product Is already Bought";
                        return View();
                    }
                }

                new ShoppingCartBL().AddProductToCart(sc);
            }


            return RedirectToAction("Index", "Store");

        }

        public ActionResult Checkout()
        {

            return RedirectToAction("createpayment", "paypal", new { description = "new item", price = 99 });

        }

        //
        // GET: /Cart/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Cart/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cart/Create

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
        // GET: /Cart/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Cart/Edit/5

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
        // GET: /Cart/Delete/5

        public ActionResult Delete(string Username, int ProductID)
        {
            new ShoppingCartBL().Delete(Username, ProductID);
            return RedirectToAction("Index");
        }

        public ActionResult DownloadFile(string Username, int ProductID)
        {
            Product p = new ProductBL().GetProductsByID(ProductID);
            User u = new UserBL().GetUserByEmail(p.SellerEmail);
            string pathIn = Server.MapPath(p.ZipPath.ToString());
            string pathOut = Server.MapPath(p.ZipPath.Replace("Enc.","."));
            HybridEncryptionClass he = new HybridEncryptionClass();
            byte[] Key = he.AsymmetricDecryption(u.PrivateKey, p.Key);
            byte[] IV = he.AsymmetricDecryption(u.PrivateKey, p.IV);

            MemoryStream ms = he.Decryption(pathIn, pathOut, Key, IV, ProductID);
            System.IO.File.Delete(pathOut);
            try
            {
                string filename = pathOut.Substring(pathOut.LastIndexOf("\\")).Remove(0,1);
                byte[] bytesInStream = ms.ToArray();
                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment;    filename="+filename+"");
                Response.BinaryWrite(bytesInStream);
                Response.End();
            }
            catch (Exception e)
            {
               
            }

            return RedirectToAction("Index");
        }

    }
}
