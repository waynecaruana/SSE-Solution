using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_ShoppingCart.Models;
using BusinessLogic;
using Common;


namespace Mvc_ShoppingCart.Controllers
{
    public class StoreController : Controller
    {
       //
        // GET: /Store/
        //[Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var genres = new GenreBL().GetAllGenres().ToList();
            return View(genres);
        }
        //
        // GET: /Store/Browse
        public ActionResult Browse(string genre)
        {
            //var genreModel = new Mvc_ShoppingCart.Models.Genre { Name = genre };
            //return View(genreModel);

            var genreModel = new ProductBL().GetProductsByGenre(genre);
            return View(genreModel);
        }

        //
        // GET: /Store/Details
        public ActionResult Details(int id)
        {
            var product = new ProductBL().GetProductsByID(id);

            return View(product);
        }
    

    }
}
