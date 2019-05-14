﻿using System.Net;
using System.Web.Mvc;
using DB.Database;
using Data.Models.Data;
using System.Text;

namespace UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostTypesController : Controller
    { 

        // GET: PostTypes
        public ActionResult Index()
        {
            var postTypes = PostTypesOps.GetDistinctPostTypes();

            return View(postTypes);
        }

        public ActionResult ListSubCategories(string category)
        {
            var subcategories = PostTypesOps.GetSubCategoriesByCategory(category);

            return View(subcategories);
        }
        // GET: PostTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PostType postType = PostTypesOps.GetPostTypeById(id.Value);

            if (postType == null)
                return HttpNotFound();

            return View(postType);
        }

        // GET: PostTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Category,SubCategory,Slug")] PostType postType)
        {
            if (ModelState.IsValid)
            {
                PostTypesOps.CreatePostTypes(postType.Category, postType.SubCategory, postType.Slug);

                return RedirectToAction("Index");
            }

            return View(postType);
        }

        // GET: PostTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PostType postType = PostTypesOps.GetPostTypeById(id.Value);

            if (postType == null)
                return HttpNotFound();

            return View(postType);
        }

        // POST: PostTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Category,SubCategory,Slug,Active")] PostType postType)
        {
            if (ModelState.IsValid)
            {
                postType.Id = id;
                PostTypesOps.UpdatePostType(postType, out StringBuilder errors);

                if (errors.Length > 0)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, errors.ToString());

                return RedirectToAction("Index");
            }
            return View(postType);
        }

        // GET: PostTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PostType postType = PostTypesOps.GetPostTypeById(id.Value);

            if (postType == null)
                return HttpNotFound();

            return View(postType);
        }

        // POST: PostTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostTypesOps.DeletePostTypeById(id, out StringBuilder errors);

            if (errors.Length > 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, errors.ToString());

            return RedirectToAction("Index");
        }
    }
}
