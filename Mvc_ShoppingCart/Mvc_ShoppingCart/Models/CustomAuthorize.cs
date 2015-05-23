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
            string page = httpContext.Request.RawUrl;

            //here add another table with url and role and check
            List<Role> userRoles = new RoleBL().GetUserRoles(email).ToList();
            List<Role> pageRoles = new RoleBL().GetPageRoles(page).ToList();

            foreach (var pr in pageRoles)
            {
                foreach (var ur in userRoles)
                {
                    if (pr.Name == ur.Name)
                    {
                        return true;
                    }
                }
            }

            
            return false;

          
        }




       
    }

    
}