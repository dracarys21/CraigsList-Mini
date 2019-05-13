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
            Dictionary<string, List<Location>> activeLocs = LocationOps.GetAllLocations();
            if (activeLocs.Count == 0)
            {
                LocationOps.CreateLocation("New York", "Manhattan", "ny-mnh");
                LocationOps.CreateLocation("New York", "Brooklyn", "ny-bklyn");
                LocationOps.CreateLocation("New York", "Queens", "ny-qns");
                LocationOps.CreateLocation("New York", "Bronx", "ny-brx");
                activeLocs = LocationOps.GetAllLocations();
            }
            Dictionary<string, List<PostType>> activeCategories = PostTypesOps.GetAllPostTypes();
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