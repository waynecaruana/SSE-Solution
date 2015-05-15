using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class ShoppingCartBL
    {

        /// <summary>
        /// With this method one can add products to shopping cart
        /// </summary>
        /// <param name="cart">an instance of type shopping cart</param>
        public void AddProductToCart(ShoppingCart cart)
        {

            new ShoppingCartRepository().AddProductToCart(cart);
        }


        //update data
        public void Update(ShoppingCart sc)
        {

            new ShoppingCartRepository().Update(sc);


        }

        /// <summary>
        /// This method is used to delete an item from shopping cart
        /// </summary>
        /// <param name="username">user pk</param>
        /// <param name="pid">product id pk</param>
        //delete an entry
        public void Delete(string Username, int ProductID)
        {
            new ShoppingCartRepository().Delete(Username, ProductID);
        }

        //method to get an entry by product id and username
        public ShoppingCart GetItemByUsernameAndProductID(string username, int id)
        {
            return new ShoppingCartRepository().GetItemByUsernameAndProductID(username, id);
        }


        public IEnumerable<ShoppingCart> GetShoppingCartItemsByUsername(string username)
        {


            return new ShoppingCartRepository().GetShoppingCartItemsByUsername(username);
        }

        public IEnumerable<OrderDetail> GetItemBoughtByEmail(string email)
        {
            return new ShoppingCartRepository().GetItemBoughtByEmail(email);
        }


        /// <summary>
        /// a method to add order
        /// </summary>
        /// <param name="orderId">order id</param>
        /// <param name="username">username</param>
        /// <param name="totalPrice">total price</param>
        public int AddOrder(string email, decimal totalPrice)
        {

            Order o = new Order();
            o.Email = email;
            o.TotalPrice = totalPrice;
            o.Date = DateTime.Now.Date;

            new ShoppingCartRepository().AddOrder(o);

            return o.ID;
        }

        public void AddOrderDetails(int productId, decimal productPrice, int orderId)
        {
            OrderDetail od = new OrderDetail();
            od.ProductID = productId;
            od.Price = productPrice;
            od.Qty = 1;
            od.OrderID = orderId;


            new ShoppingCartRepository().AddOrderDetails(od);
        }

        public IQueryable<Order> GetOrdersByUsername(string username)
        {
            return new ShoppingCartRepository().GetOrdersByUsername(username);
        }

        public IQueryable<OrderDetail> GetOrderDetailsByOrderId(int id)
        {
            return new ShoppingCartRepository().GetOrderDetailsByOrderId(id);
        }


       

    }
}
