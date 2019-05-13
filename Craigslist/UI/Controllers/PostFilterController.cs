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

        // GET: PostFilter
        [HttpGet]
        public ActionResult Index(string pNo = "1", string pAction = "", string query = "")
        {
            var pageNo = int.Parse(pNo);

            if (pAction.Equals("next"))
                pageNo++;

            if (pAction.Equals("prev"))
                pageNo--;

            if (pageNo < 1)
                pageNo = 1;

            // Cache all values
            var location = Location;
            var postType = PostType;

            var posts = PostFilter.FilterPost(location.Area,
                location.Locale, postType.Category, postType.SubCategory, query);

            var filteredPosts = posts
                .Select(p => new PostFilterViewModel
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

            
            var pageSize = 5;
            ViewBag.PageCount = Math.Ceiling(filteredPosts.Count * 1.0 / pageSize);
            ViewBag.CurrentPage = pageNo;

            if (pageNo > ViewBag.PageCount)
                pageNo = ViewBag.PageCount + 1;

            var pagedPosts = filteredPosts.Skip((pageNo - 1) * pageSize).Take(pageSize);

            return View(pagedPosts.ToList());
        }

        // TODO: Remove this. The cookie should be set from the home page
        [HttpGet]
        public ActionResult SetCookie(string cookieName, string value)
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
    }
}
