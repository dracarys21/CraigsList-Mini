using Data.Models;
using Data.Models.Data;
using DB.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult HomePage()
        {
            Dictionary<string, List<string>> activeLocs = LocationOps.GetAllLocations();
            Dictionary<string, List<string>> activeCategories = PostTypesOps.GetAllPostTypes();

            HomePageViewModel homePageViewModel = new HomePageViewModel(activeLocs, activeCategories);
            return View(homePageViewModel);
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