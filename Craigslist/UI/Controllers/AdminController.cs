using Data.Models;
using Data.Models.Data;
using DB.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DB.Database;

namespace UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DisplayUsers()
        {
            return View(UserRoles.GetUsers());
        }

        public ActionResult UserPosts(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = UserRoles.GetUserByUserName(username);
            var posts = UserPost.GetPostsByUserId(user.Id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // Get: ChangeUserToAdmin
        // GET: Locations/Delete/5
        public ActionResult ChangeUserToAdmin(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = UserRoles.GetUserByUserName(username);

            if (user == null)
            {
                return HttpNotFound();
            }

            AdminUserDisplayViewModel u = new AdminUserDisplayViewModel();
            u.Email = user.Email;
            u.UserName = user.UserName;
            u.UserId = user.Id;
            return View(u);
        }

        public ActionResult ChangeUserToAdminConfirmed(string username)
        {
            ApplicationUser user = UserRoles.GetUserByUserName(username);
            AdminUserDisplayViewModel u = new AdminUserDisplayViewModel();
            u.Email = user.Email;
            u.UserName = user.UserName;
            u.UserId = user.Id;

            if (UserRoles.ChangeUserRole(username))
                return RedirectToAction("Index");
            ModelState.AddModelError("","User Name Does not exists");
            return View(u);
        }
    }
}