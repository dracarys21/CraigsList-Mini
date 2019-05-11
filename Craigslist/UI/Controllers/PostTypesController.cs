﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB.Database;
using Data.Models.Data;
using Microsoft.AspNet.Identity;
using System.Text;

namespace UI.Controllers
{
    public class PostTypesController : Controller
    { 

        // GET: PostTypes
        public ActionResult Index()
        {
            return View(PostTypesOps.GetDistinctPostTypes());
        }


        public ActionResult ListSubCategories(string category)
        {
            return View(PostTypesOps.GetPostTypesByCategory(category));
        }
        // GET: PostTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostType postType = PostTypesOps.GetPostTypesById(id);
            if (postType == null)
            {
                return HttpNotFound();
            }
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
        public ActionResult Create([Bind(Include = "Id,Category,SubCategory,Slug")] PostType postType)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostType postType = PostTypesOps.GetPostTypesById(id);
            if (postType == null)
            {
                return HttpNotFound();
            }
            return View(postType);
        }

        // POST: PostTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Category,SubCategory,Slug,Active")] PostType postType)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();
                PostTypesOps.UpdatePostTypes(UserRoles.GetCurrentUser(userid), postType, out StringBuilder errors);
                return RedirectToAction("Index");
            }
            return View(postType);
        }

        // GET: PostTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostType postType = PostTypesOps.GetPostTypesById(id);
            if (postType == null)
            {
                return HttpNotFound();
            }
            return View(postType);
        }

        // POST: PostTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string userid = User.Identity.GetUserId();
            PostTypesOps.DeletePostTypeById(UserRoles.GetCurrentUser(userid), id, out StringBuilder error);

            return RedirectToAction("Index");
        }
    }
}
