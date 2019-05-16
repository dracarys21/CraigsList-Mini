using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Models;
using Data.Models.Data;
using DB.Database;

namespace UI.Controllers
{
    [AllowAnonymous]
    public class PostFilterController : Controller
    {
        public Location Location
        {
            get
            {
                string area = string.Empty;
                string locale = string.Empty;

                if (Request.Cookies["location"] != null )
                {
                    var locationValues = Request.Cookies["location"].Value.Split('|');
                    area = locationValues.Length > 0 ? locationValues[0] : string.Empty;
                    locale = locationValues.Length > 1 ? locationValues[1] : string.Empty;
                }
                else
                {
                    // TODO: Remove this. The cookie should be set from the home page
                    var cookie = new HttpCookie("location");
                    area = string.Empty;
                    locale = string.Empty;

                    cookie.Value = $"{area}|{locale}";
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }

                var location = new Location();

                if (!string.IsNullOrEmpty(area))
                    location.Area = area;

                if (!string.IsNullOrEmpty(locale))
                    location.Locale = locale;

                return location;
            }
        }

        public PostType PostType
        {
            get
            {
                string category = string.Empty;
                string subcategory = string.Empty;

                if (Request.Cookies["post_type"] != null )
                {
                    var locationValues = Request.Cookies["post_type"].Value.Split('|');
                    category = locationValues.Length > 0 ? locationValues[0] : string.Empty;
                    subcategory = locationValues.Length > 1 ? locationValues[1] : string.Empty;
                }
                else
                {
                    // TODO: Remove this. The cookie should be set from the home page
                    var cookie = new HttpCookie("post_type");
                    category = string.Empty;
                    subcategory = string.Empty;

                    cookie.Value = $"{category}|{subcategory}";
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }

                var postType = new PostType();

                if (!string.IsNullOrEmpty(category))
                    postType.Category = category;

                if (!string.IsNullOrEmpty(subcategory))
                    postType.SubCategory = subcategory;

                return postType;
            }
        }

        public List<string> Areas
        {
            get
            {
                var locations = LocationOps.GetActiveLocationsList();
                var areas = new List<string>
                {
                    "Please Select an Area"
                };

                areas.AddRange(locations
                    .GroupBy(l => l.Area)
                    .Select(l => l.Key)
                    .OrderBy(l => l)
                    .ToList());

                return areas;
            }
        }

        public List<string> Categories
        {
            get
            {
                var postTypes = PostTypesOps.GetActivePostTypesList();
                var categories = new List<string>
                {
                    "Please Select a Category"
                };

                categories.AddRange(postTypes
                    .GroupBy(p => p.Category)
                    .Select(l => l.Key)
                    .OrderBy(l => l)
                    .ToList());

                return categories;
            }
        }

        // GET: PostFilter
        [HttpGet]
        public ActionResult Index(string area = "", string category = "",
            string locale = "", string subcategory = "",
            string query = "", string pageAction = "")
        {
//            var pageNo = int.Parse(pNo);
            
            if (!int.TryParse(pageAction, out var pageNo))
            {
                pageNo = 1;
            }

            var selectedArea = string.IsNullOrEmpty(area) ? "Please Select an Area" : area;
            var selectedCategory = string.IsNullOrEmpty(category) ? "Please Select a Category" : category;
            var selectedSubcategory = string.IsNullOrEmpty(subcategory) ? "Please Select a Subcategory" : subcategory;
            var selectedLocale = string.IsNullOrEmpty(locale) ? "Please Select a Locale" : locale;
            var actualArea = area.Equals("Please Select an Area") ? "" : area;
            var actualLocale = locale.Equals("Please Select a Locale") ? "" : locale;
            var actualCategory = category.Equals("Please Select a Category") ? "" : category;
            var actualSubcategory = subcategory.Equals("Please Select a Subcategory") ? "" : subcategory;
            var posts = PostFilter.FilterPost(actualArea, actualLocale,
                actualCategory, actualSubcategory, query);

            var filteredPosts = posts
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Body = p.Body,
                    Area = p.Location.Area,
                    Locale = p.Location.Locale,
                    Category = p.PostType.Category,
                    Subcategory = p.PostType.SubCategory,
                    CreateDate = p.CreateDate.ToString("MMM dd")
                }).ToList();

            var pageSize = 10;

            ViewBag.PageSize = pageSize;
            ViewBag.PageCount = Convert.ToInt32(Math.Ceiling(filteredPosts.Count * 1.0 / pageSize));

            if (pageNo < 1)
                pageNo = 1;

            if (pageNo > ViewBag.PageCount)
                pageNo = ViewBag.PageCount;

            ViewBag.RightPageIndex = Convert.ToInt32(Math.Min(pageNo + pageSize, ViewBag.PageCount));
            ViewBag.LeftPageIndex = ViewBag.RightPageIndex - pageSize;
            ViewBag.VisibleRightIndex = Convert.ToInt32(Math.Min(ViewBag.RightPageIndex, ViewBag.PageCount));
            ViewBag.VisibleLeftPageIndex = Convert.ToInt32(Math.Max(ViewBag.LeftPageIndex, 1));

            ViewBag.CurrentPage = pageNo;

            var pagedPosts = filteredPosts.Skip((pageNo - 1) * pageSize).Take(pageSize);
            var subcategories = new List<string> { selectedSubcategory };
            var locales = new List<string> { selectedLocale };

            if (string.IsNullOrEmpty(actualCategory) && !string.IsNullOrEmpty(actualSubcategory))
                category = PostTypesOps.GetCategoryBySubcategoryName(actualSubcategory);
            
            if (string.IsNullOrEmpty(actualArea) && !string.IsNullOrEmpty(actualLocale))
                area = LocationOps.GetAreaByLocale(actualLocale);

            if (!string.IsNullOrEmpty(category))
                subcategories.AddRange(PostTypesOps.GetSubCategoriesByCategory(category)
                    .Select(s => s.SubCategory)
                    .OrderBy(l => l)
                    .ToList());
            
            if (!string.IsNullOrEmpty(area))
                locales.AddRange(LocationOps.GetLocalesByArea(area)
                    .Select(l => l.Locale)
                    .OrderBy(l => l)
                    .ToList());

            
            return View(new PostFilterViewModel
            {
                Query = query,
                Posts = pagedPosts.ToList(),
                Categories = new SelectList(Categories, selectedCategory),
                Areas = new SelectList(Areas, selectedArea),
                SubCategories = new SelectList(subcategories, selectedSubcategory),
                Locales = new SelectList(locales, selectedLocale)
            });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return RedirectToAction("Details", "Posts", new { id });
        }

        #region Ajax Methods

        [HttpPost]
        public ActionResult GetSubCategoriesByCategory(string category)
        {
            var subCategories = PostTypesOps.GetSubCategoriesByCategory(category);
            var subCategorySelectItems = subCategories
                .Select(l => new SelectListItem {
                    Value = l.Id.ToString(),
                    Text = l.SubCategory})
                .ToList();

//            var subCategorySelectList = new SelectList(subCategorySelectItems,
//                "Value", "Text");

            return Json(subCategorySelectItems);
        }

        [HttpPost]
        public ActionResult GetLocalesByArea(string area)
        {
            var locales = LocationOps.GetLocalesByArea(area);
            var localeSelectItems = locales
                .Select(l => new SelectListItem {
                    Value = l.Id.ToString(),
                    Text = l.Locale})
                .ToList();

//            var localesSelectList = new SelectList(localeSelectItems,
//                "Value", "Text");

            return Json(localeSelectItems);
        }

        #endregion
    }
}
