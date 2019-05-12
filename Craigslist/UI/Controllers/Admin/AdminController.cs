using Data.Models;
using System.Web.Mvc;
using DB.Database;

namespace UI.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
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
        public ActionResult ChangeUserToAdmin(string userId)
        {
            // If you hit this URL, you are already a user with the 'Admin' role
            UserManagement.PromoteUser(userId);

            return RedirectToAction("Index");
        }
    }
}