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
    }
}
