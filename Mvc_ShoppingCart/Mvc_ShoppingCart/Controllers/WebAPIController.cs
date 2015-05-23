using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using BusinessLogic;
using Common;
using Mvc_ShoppingCart.Models;

namespace Mvc_ShoppingCart.Controllers
{
    public class MyUsers
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class WebAPIController : ApiController
    {
        //
        // GET: /WebAPI/
        //[CustomAuthorize(Roles = "Admin")]
        public IEnumerable<MyUsers> GetUsers()
        {
            List<MyUsers> users =  new List<MyUsers>();
            List<User> dbUsers = new UserBL().GetUsers().ToList();

            foreach (User item in dbUsers)
            {
                MyUsers u = new MyUsers();
                u.Email = item.Email;
                u.Name = item.Firstname;
                u.Surname = item.Lastname;

                users.Add(u);
            }

            return users.ToList<MyUsers>();
        }

       

    }
}
