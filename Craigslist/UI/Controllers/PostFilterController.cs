using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Data.Models;
using Data.Models.Data;
using DB.Database;

namespace UI.Controllers
{
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
//                    area = "New York";
//                    locale = "brooklyn";
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
//                    category = "Housing";
//                    subcategory = "apartments";
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
        public List<PostFilterViewModel> Filter(string query = "")
        {
            // Cache all values
            var location = Location;
            var postType = PostType;

            var posts = PostFilter.FilterPost(location.Area, location.Locale,
                postType.Category, postType.SubCategory, query);

            var postViewModels = new List<PostFilterViewModel>();

            foreach (var post in posts)
            {
                postViewModels.Add(new PostFilterViewModel(post));
            }


            return postViewModels;
        }
    }
}
