using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BusinessLogic;

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
        public ActionResult AddToShoppingCart(int Quantity = 0)
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
                sc.Email = Session["Username"].ToString();
                sc.Qty = Quantity;
                sc.ProductID = Convert.ToInt32(Session["ProductID"]);

                new ShoppingCartBL().AddProductToCart(sc);
            }


            return RedirectToAction("Index", "Product");

        }

        //public ActionResult Checkout(string Token)
        //{
        //        User u = new UserBL().GetUserByEmail(User.Identity.Name);
            

        //        string orderID = Guid.NewGuid().ToString();//create a bew guid for orderID
        //        IEnumerable<ShoppingCart> productsList = new ShoppingCartBL().GetShoppingCartItemsByUsername(User.Identity.Name);
        //        decimal granTotal = 0;//store the total price

        //        foreach (ShoppingCart sc in productsList)
        //        {
        //            Product p = new ProductBL().GetProductsByID(sc.ProductID);
        //            granTotal += Convert.ToDecimal(p.Price);
        //        }

        //        foreach (ShoppingCart sc in productsList)
        //        {

        //            int productId = Convert.ToInt32(sc.ProductID);
        //            int newStock;
        //            Product p = new ProductBL().GetProductsByID(productId);

        //            if (new WCFOrderService.WCF_OrdersClient().GetOrderByID(orderID) == null)
        //            {
        //                //add order
        //                Order o = new Order();
        //                o.ID = orderID;
        //                o.Username = Session["Username"].ToString();
        //                o.Date = DateTime.Now.Date;
        //                o.TotalPrice = granTotal;
        //                new WCFOrderService.WCF_OrdersClient().AddOrder(o);
        //            }

        //            newStock = Convert.ToInt32(p.Stock) - sc.Qty;

        //            //update product
        //            p.Stock = newStock;
        //            new WCFProductService.WCF_ProductsClient().Update(p);

        //            //clear shopping cart allocated to that user
        //            new WCFCartService.WCF_CartClient().Delete(Session["Username"].ToString(), p.ID);

        //            //add order details
        //            OrderDetail od = new OrderDetail();
        //            od.ProductID = p.ID;
        //            od.Price = p.Price;
        //            od.Qty = sc.Qty;
        //            od.WarrantyExpiryDate = DateTime.Now.AddYears(2);
        //            od.OrderID = orderID;
        //            new WCFOrderService.WCF_OrdersClient().AddOrderDetails(od);//add order detail
                
        //    }

         


        //    return RedirectToAction("Index", "Product");

        //}

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

    }
}
