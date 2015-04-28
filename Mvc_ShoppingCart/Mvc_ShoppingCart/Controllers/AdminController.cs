using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_ShoppingCart.Models;
using BusinessLogic;

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

    }
}
