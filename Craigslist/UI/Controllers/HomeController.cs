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
                    .Select(l => l.Locale)
                    .ToList();

            locales.Sort();

            return View(new HomePageViewModel(activeCategories)
            {
                Area = area,
                Areas = areas,
                Locales = locales,
                Locale = locale
            });
        }

        public ActionResult CreatePost(string area = "New York", string locale = "",
            string category = "", string subcategory = "")
        {
            return RedirectToAction("Create", "Posts", 
                new { area, locale, category, subcategory });
        }
    }
}