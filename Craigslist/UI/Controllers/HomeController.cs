using System.Web;
using DB.Database;
using System.Web.Mvc;
using Data.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var activeLocs = LocationOps.GetActiveLocationsList();
            var activeCategories = PostTypesOps.GetActivePostTypesList();
            var model = new HomePageViewModel(activeLocs, activeCategories);

            if (Request.Cookies["CurrentLocation"] != null)
                model.CurrentLocation = Request.Cookies["CurrentLocation"].Value;

            HttpCookie cookie = new HttpCookie("CurrentLocation")
            {
                Value = model.CurrentLocation
            };

            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            return View(model);
        }

        public ActionResult SetCookies(string cookieName, string value)
        {
            HttpCookie cookie = null;
            if (Request.Cookies[cookieName] != null)
            {
                Request.Cookies[cookieName].Value = value;
                cookie = Request.Cookies[cookieName];
            }
            else
            {
                cookie = new HttpCookie(cookieName)
                {
                    Value = value
                };
            }

            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }

        public ActionResult ChangeLocation(string newLocationName)
        {
            HttpCookie cookie = new HttpCookie("CurrentLocation")
            {
                Value = newLocationName
            };
            
            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }
    }
}