using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using BusinessLogic;
using System.Data.Entity;

namespace Mvc_ShoppingCart.Models
{
    public class ShoppingCartModel
    {
        public string Username { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public List<ShoppingCartModel> items { get; set; }

        public SecureShoppingCartDBEntities myProducts { get; set; }

         //constructor
        public ShoppingCartModel(IEnumerable<ShoppingCart> sc)
        {

            foreach (ShoppingCart item in sc)
            {
                Common.Product p =  new ProductBL().GetProductsByID(item.ProductID);
                Name = p.Name;
            }
        }
    }
}