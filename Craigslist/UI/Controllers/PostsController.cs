using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Data.Models;
using DB.Database;
using Microsoft.AspNet.Identity;

namespace UI.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Index(string returnUri = "")
        {
            if (!string.IsNullOrEmpty(returnUri))
                return Redirect(returnUri);

            if (User.Identity.IsAuthenticated)
                return View(UserPost.GetPostsByUserId(User.Identity.GetUserId()));
            else
                return RedirectToAction("Login", "Account");
        }

        // GET: Posts/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = UserPost.GetPostById(id.Value);
            
            if (post == null)
                return HttpNotFound();

            var viewModel = new PostViewModel
            {
                Id = id.Value,
                Title = post.Title,
                Body = post.Body,
                CreateDate = post.CreateDate.ToShortDateString()
            };

            if (Request.UrlReferrer != null
                && !string.IsNullOrEmpty(Request.UrlReferrer.PathAndQuery)
                && Request.UrlReferrer.PathAndQuery.Contains("PostFilter"))
                viewModel.ReturnUri = Request.UrlReferrer.PathAndQuery;

            return View(viewModel);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            var locations = LocationOps.GetActiveLocationsList();
            var postTypes = PostTypesOps.GetActivePostTypesList();
            
            var categories = new List<string>
            {
                "Please Select a Category"
            };

            categories.AddRange(postTypes
                .GroupBy(p => p.Category)
                .Select(l => l.Key)
                .ToList());

            var areas = new List<string>
            {
                "Please Select an Area"
            };

            areas.AddRange(locations
                .GroupBy(l => l.Area)
                .Select(l => l.Key)
                .ToList());

            return View(new PostViewModel
            {
                Areas = new SelectList(areas, "Please Select an Area"),
                Locales = null,
                Categories = new SelectList(categories, "Please Select a Category"),
                SubCategories = null
            });
        }

        // POST: Posts/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Title,Body,SelectedArea,SelectedLocale,SelectedCategory,SelectedSubcategory")]
            PostViewModel post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var postType = PostTypesOps.GetPostTypeById(int.Parse(post.SelectedSubcategory));
                    var location = LocationOps.GetLocationById(int.Parse(post.SelectedLocale));

                    UserPost.CreatePost(User.Identity.GetUserId(), post.Title, post.Body,
                        post.SelectedCategory, postType.SubCategory, post.SelectedArea,
                        location.Locale);

                    return RedirectToAction("Index");
                }

                return View(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = UserPost.GetPostById(id.Value);

            if (post == null)
                return HttpNotFound();
            
            return View(post);
        }

        // POST: Posts/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Title,Body")] PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                UserPost.UpdatePost(User.Identity.GetUserId(), id, post.Title, post.Body, out var errors);

                if (errors.Length > 0)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, errors.ToString());
                    
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = UserPost.GetPostById(id.Value);

            if (post == null)
                return HttpNotFound();
            
            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserPost.DeletePostByPostId(User.Identity.GetUserId(), id, out var errors);

            if (errors.Length > 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, errors.ToString());

            return RedirectToAction("Index");
        }
    }
}
