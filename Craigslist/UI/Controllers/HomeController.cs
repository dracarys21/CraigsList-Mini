using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB.Database;
using System.Web.Mvc;
using Data.Models;
using Data.Models.Data;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string area = "New York", string locale = "",
            string category = "", string subcategory = "")
        {

//            var categories = PostTypesOps.GetDistinctCategories().ToList();
//            var subcategories = new List<string>();
//            if (!string.IsNullOrEmpty(category))
//                subcategories = PostTypesOps.GetSubCategoriesByCategory(category)
//                    .Select(c => c.SubCategory).ToList();

//            var activeLocs = LocationOps.GetActiveLocationsList();
//            var model = new HomePageViewModel(activeLocs, activeCategories);

//            if (Request.Cookies["CurrentLocation"] != null && activeLocs.Count != 0)
//                 model.CurrentLocation = Request.Cookies["CurrentLocation"].Value;

//            HttpCookie cookie = new HttpCookie("CurrentLocation")
//            {
//                Value = model.CurrentLocation
//            };
//
//            ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            if (!string.IsNullOrEmpty(category) || !string.IsNullOrEmpty(subcategory))
                return RedirectToAction("Index", "PostFilter",
                    new {area, locale, category, subcategory});

            var areas = LocationOps.GetDistinctAreas()
                .Select(a => a.Area)
                .ToList();

            var locales = new List<string>();
            var activeCategories = PostTypesOps.GetActivePostTypesList();

            if (!string.IsNullOrEmpty(area))
                locales = LocationOps.GetLocalesByArea(area)
                    .Select(l => l.Locale).ToList();

            return View(new HomePageViewModel(activeCategories)
            {
                Area = area,
                Areas = areas,
                Locales = locales
            });
        }

//        // Get: /{location}/{posttype}
//        [HttpPost]
//        public ActionResult FilterPosts(string area = "", string locale = "",
//            string category = "", string subcategory = "")
//        {
//            return RedirectToAction("Index", "PostFilter", 
//                new { area, locale, category, subcategory });
//        }

//        [HttpGet]
//        public ActionResult SetArea(string area)
//        {
//            var areas = LocationOps.GetDistinctAreas().ToList();
//            var activeCategories = PostTypesOps.GetActivePostTypesList();
//            var locales = new List<string>();
//
//            if (!string.IsNullOrEmpty(area))
//                locales = LocationOps.GetLocalesByArea(area)
//                    .Select(l => l.Locale).ToList();
//
//            return View("Index", new HomePageViewModel(areas, activeCategories)
//            {
//                Areas = new SelectList(areas.Select(a => a.Area), "New York"),
//                Locales = locales
//            });
//        }
    }
}