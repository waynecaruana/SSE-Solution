using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic;
using System.ComponentModel.DataAnnotations;
using Common;
using System.Web.Mvc;

namespace Mvc_ShoppingCart.Models
{
    public class Product
    {

        public int ID { get; set; }
        public int GenreId { get; set; }
        public int SellerId { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        [Required]
        public string ZipPath { get; set; }
        public Genre Genre { get; set; }
        public Seller Seller { get; set; }

        [Display(Name = "Genre: ")]
        public IEnumerable<SelectListItem> Genres { get; set; }
        public int SelectedGenre { get; set; }

      
    }
}