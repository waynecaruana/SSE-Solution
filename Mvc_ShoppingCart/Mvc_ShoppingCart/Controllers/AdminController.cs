using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_ShoppingCart.Models;
using BusinessLogic;
using System.Net;
using Newtonsoft.Json;
using Common;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Mvc_ShoppingCart.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [CustomAuthorize()]
        public ActionResult AllocateRoles()
        {
            RoleModel roleModel = new RoleModel();
            roleModel.Roles = new SelectList(new RoleBL().GetAllRoles(), "RoleID", "Name");
            roleModel.Users = new SelectList(new UserBL().GetUsers(), "Email", "Email");
            return View(roleModel);
        }

        [HttpPost]
        public ActionResult AllocateRoles(RoleModel model)
        {
            int role = model.SelectedRole;
            string email = model.SelectedUser;

            new RoleBL().AllocateRole(role, email, model.SelectedOption);

            RoleModel roleModel = new RoleModel();
            roleModel.Roles = new SelectList(new RoleBL().GetAllRoles(), "RoleID", "Name");
            roleModel.Users = new SelectList(new UserBL().GetUsers(), "Email", "Email");
            return View(roleModel);
        }

        //[CustomAuthorize(Roles = "Admin")]
        //public ActionResult ManageUsers()
        //{
        //    return View();
        //}

        //[CustomAuthorize()]
        public ActionResult CallWebApi(string input)
        {
            try
            {
                WebClient client = new WebClient();
                string url = "http://localhost:52387/api/WebAPI/GetUsers/hash?message=" + input;
                //client.Headers.Add("username", User.Identity.Name);
                client.Headers.Add("key", "25892e17-80f6-415f-9c65-7395632f0223");
                string myString = client.DownloadString("http://localhost:52387/api/WebAPI/GetUsers");
                List<MyUsers> posts = JsonConvert.DeserializeObject<List<MyUsers>>(myString);
                return View("CallWebAPI", posts);
            }
            catch
            {
                return View("Error");
            }
        }

       
        public ActionResult Error()
        {
            return View("Error");
        }


    }

   
}

public class MyUsers
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class CustomDelegatingHandler : DelegatingHandler
{
    protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        if (request.Headers.GetValues("key").Contains("25892e17-80f6-415f-9c65-7395632f0223"))
            return base.SendAsync(request, cancellationToken);

        else
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent("Can't Access Web Service");
            response.StatusCode = System.Net.HttpStatusCode.Forbidden;
            return Task<HttpResponseMessage>.Factory.StartNew(() => response);
        }
    }
}




