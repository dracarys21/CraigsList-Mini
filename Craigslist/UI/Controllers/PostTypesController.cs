﻿using System.Net;
using System.Web.Mvc;
using DB.Database;
using Data.Models.Data;
using System.Text;
using Microsoft.AspNet.Identity;

namespace UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostTypesController : Controller
    { 

        // GET: PostTypes
        public ActionResult Index()
        {
            return View(PostTypesOps.GetDistinctCategories());
        }

        public ActionResult ListSubCategories(string category)
        {
            return View(PostTypesOps.GetSubCategoriesByCategory(category));
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


        public ActionResult DeleteCategory(string category)
        {
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostType postType = PostTypesOps.GetSubCategoriesByCategory(category)[0];
            if (postType == null)
            {
                return HttpNotFound();
            }
            DeleteAreaOrCategoryViewModel v = new DeleteAreaOrCategoryViewModel();
            v.Upper = postType.Category;
            return View(v);
        }


        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(string category)
        {
            string userid = User.Identity.GetUserId();
            PostTypesOps.DeletePostTypeByCategory(category, out StringBuilder error);
            return RedirectToAction("Index");
        }
    }
}
