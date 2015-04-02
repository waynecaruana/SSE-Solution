using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_ShoppingCart.Models
{
    public class Product
    {
        public int GenreId { get; set; }
        public int SellerId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public Genre Genre { get; set; }
        public Seller Seller { get; set; }
    }
}