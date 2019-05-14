using Data.Models;
using DB.Database;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var activeLocs = LocationOps.GetActiveLocationsList();
            var activeCategories = PostTypesOps.GetActivePostTypesList();

            return View(new HomePageViewModel(activeLocs, activeCategories));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}