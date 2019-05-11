using Data.Models;
using DB.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // Get: ChangeUserToAdmin
        public ActionResult ChangeUserToAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Post: ChangeUserToAdmin
        public ActionResult ChangeUserToAdmin([Bind(Include = "Id,Email")] ChangeUserToAdminViewModel cua)
        {
            if (ModelState.IsValid)
            {
                if (UserRoles.ChangeUserRole(cua.Email))
                  return  RedirectToAction("Index");
                ModelState.AddModelError("","User Name does not exists");
            }
            return View(cua);
        }
    }
}