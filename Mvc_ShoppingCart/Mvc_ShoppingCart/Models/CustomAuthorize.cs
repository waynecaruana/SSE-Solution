using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Common;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Routing;


namespace Mvc_ShoppingCart.Models
{
    public class CustomAuthorize : AuthorizeAttribute
    {


        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {            
            string email = httpContext.User.Identity.Name;
            bool admin = Roles.Contains("Admin");
            bool buyer = Roles.Contains("Buyer");
            bool seller = Roles.Contains("Seller");
            List<Role> userRoles = new List<Role>();
            try
            {
                userRoles = new RoleBL().GetUserRoles(email).ToList();
            }
            catch
            {
                return false;
            }
            if (admin)
            {
                foreach (Role r in userRoles)
                {
                    if (r.Name == "Admin")
                    {
                        return true;
                    }
                    else return false;
                }
            }
            if (seller)
            {
                foreach (Role r in userRoles)
                {
                    if (r.Name == "Seller")
                    {
                        return true;
                    }
                    else return false;

                }
            }

            if (buyer)
            {
                foreach (Role r in userRoles)
                {
                    if (r.Name == "Buyer")
                    {
                        return true;
                    }
                    else return false;

                }
            }
            return false;

          
        }




       
    }

    
}