using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess
{
    public class ShoppingCartRepository : ConnectionClass
    {
        public ShoppingCartRepository()
            : base()
        {

        }


        /// <summary>
        /// With this method one can add products to shopping cart
        /// </summary>
        /// <param name="cart">an instance of type shopping cart</param>
        public void AddProductToCart(ShoppingCart cart)
        {

            entities.AddToShoppingCarts(cart);
            entities.SaveChanges();
        }


        //update data
        public void Update(ShoppingCart sc)
        {

            entities.ShoppingCarts.Attach(GetItemByUsernameAndProductID(sc.Email, sc.ProductID));

            entities.ShoppingCarts.ApplyCurrentValues(sc);

            entities.SaveChanges();

        }

        /// <summary>
        /// This method is used to delete an item from shopping cart
        /// </summary>
        /// <param name="username">user pk</param>
        /// <param name="pid">product id pk</param>
        //delete an entry
        public void Delete(string Username, int ProductID)
        {
            entities.ShoppingCarts.DeleteObject(GetItemByUsernameAndProductID(Username, ProductID));
            entities.SaveChanges();
        }

        //method to get an entry by product id and username
        public ShoppingCart GetItemByUsernameAndProductID(string username, int id)
        {
            return entities.ShoppingCarts.SingleOrDefault(x => x.Email == username && x.ProductID == id);
        }





        public IEnumerable<ShoppingCart> GetShoppingCartItemsByUsername(string username)
        {


            var list =
              (
                  from sc in entities.ShoppingCarts
                  where sc.Email == username
                  select sc
              );

            return list.Distinct().AsQueryable();
        }


        public IEnumerable<OrderDetail> GetItemBoughtByEmail(string email)
        {
            List<Common.Order> orders = entities.Orders.Where(o => o.Email == email).ToList();
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (Order o in orders)
            {
               orderDetails.AddRange(entities.OrderDetails.Where(od => od.OrderID == o.ID).ToList());
            }

            return orderDetails;
        }

        /// <summary>
        /// a method to add orders
        /// </summary>
        /// <param name="o">an instance of type orders</param>
        public void AddOrder(Order o)
        {
            entities.Orders.AddObject(o);//to add a new product
            entities.SaveChanges();//to save and confirm modifications
        }

        /// <summary>
        /// a method to add order details
        /// </summary>
        /// <param name="od">an instance of type order details</param>
        public void AddOrderDetails(OrderDetail od)
        {
            entities.OrderDetails.AddObject(od);//to add a new product
            entities.SaveChanges();//to save and confirm modifications
        }


        public IQueryable<Order> GetOrdersByUsername(string username)
        {
            var list =
              (
                  from sc in entities.Orders
                  where sc.Email == username
                  select sc
              );

            return list.Distinct().AsQueryable();

           
        }

        public IQueryable<OrderDetail> GetOrderDetailsByOrderId(int id)
        {


            return entities.OrderDetails.Where(p => p.OrderID == id);


        }
    }
}
