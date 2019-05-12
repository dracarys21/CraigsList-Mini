using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Data.Models;
using DB.Database;
using Data.Models.Data;
using Microsoft.AspNet.Identity;

namespace UI.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Index(string query = "")
        {
            return View(UserPost.GetPostsByUserId(User.Identity.GetUserId()));
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = UserPost.GetPostById(id.Value);

            if (post == null)
                return HttpNotFound();
            
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            var locations = LocationOps.GetActiveLocationsList();
            var postTypes = PostTypesOps.GetActivePostTypesList();
            
            var categories = postTypes
                .GroupBy(p => p.Category)
                .Select(l => l.Key)
                .ToList();

            var areas = locations
                .GroupBy(l => l.Area)
                .Select(l => l.Key)
                .ToList();

            var defaultArea = new SelectListItem
            {
                Value = "0",
                Text = @"Please Select an Area",
                Selected = true
            };

            return View(new PostViewModel
            {
                Areas = new SelectList(areas),
                Locales = null,
                Categories = new SelectList(categories),
                SubCategories = null
            });
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var something = post.SelectedCategory;

//                    UserPost.CreatePost(User.Identity.GetUserId(), post.Title, post.Body,
//                        post.SelectedCategory, post.SelectedSubCategory, post.SelectedArea,
//                        post.SelectedLocale);

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

            var subCategorySelectList = new SelectList(subCategorySelectItems,
                "Value", "Text");

            return Json(subCategorySelectList);
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

            var localesSelectList = new SelectList(localeSelectItems,
                "Value", "Text");

            return Json(localesSelectList);
        }

        #endregion
        // GET: Posts/Edit/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Title,Body,SelectedArea,SelectedLocale,SelectedCategory,SelectedSubCategory")] Post post)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
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
