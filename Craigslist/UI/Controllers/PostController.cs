using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Models;
using DB.Database;
using Data.Models.Data;
using Microsoft.AspNet.Identity;
using DB.Database;

namespace UI.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private Location Location
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

        private PostType PostType
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

        // GET: Posts
        public ActionResult Index(string query = "")
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

            return View(postViewModels);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            var locations = db.Locations.ToList();
            var postTypes = db.PostType.ToList();

            var areas = new List<string>();
            var locales = new List<string>();
            var categories = new List<string>();
            var subCategories = new List<string>();

            foreach (var location in locations)
            {
                areas.Add(location.Area);
                locales.Add(location.Locale);
            }

            foreach (var postType in postTypes)
            {
                categories.Add(postType.Category);
                subCategories.Add(postType.SubCategory);
            }

            return View(new PostViewModel
            {
                Areas = areas,
                Locales = locales,
                Categories = categories,
                SubCategories = subCategories
            });
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Body,Location,SelectedArea,SelectedLocale,SelectedCategory,SelectedSubCategory")] PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                UserPost.CreatePost(User.Identity.GetUserId(),
                    post.Title, post.Body, post.PostType, post.Location);

                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,CreateDate,LastModifiedDate,ExpirationDate,Deleted")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
