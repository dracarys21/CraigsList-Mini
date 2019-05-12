using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DB.Database;
using Data.Models.Data;
using System.Text;
using Microsoft.AspNet.Identity;

namespace UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LocationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Locations
        public ActionResult Index()
        {
            return View(db.Locations.ToList());
        }


        //Get: Locales
        public ActionResult ListLocale(string area)
        {
             var locales = from loc in db.Locations
                              where loc.Area == area
                              select loc;
            return View(locales.ToList());
        }
        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            } 
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Area,Locale,Slug")] Location location)
        {
            if (ModelState.IsValid)
            {
                LocationOps.CreateLocation(location.Area, location.Locale, location.Slug);
                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Area,Locale,Slug")] Location location)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();
                LocationOps.UpdateLocation(UserRoles.GetUserById(userid),location, out StringBuilder errors);
                return RedirectToAction("ListLocale",new {area = location.Area });
            }
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string userid = User.Identity.GetUserId();
            LocationOps.DeleteLocationById(UserRoles.GetUserById(userid), id, out StringBuilder error);
            return RedirectToAction("Index");
        }


        public ActionResult DeleteArea(string area )
        {
            if (area == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = LocationOps.GetLocalesByArea(area).FirstOrDefault();
            if (location == null)
            {
                return HttpNotFound();
            }
            DeleteAreaOrCategoryViewModel v = new DeleteAreaOrCategoryViewModel();
            v.Upper = location.Area;
            return View(v);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("DeleteArea")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAreaConfirmed(string area)
        {
            string userid = User.Identity.GetUserId();
            LocationOps.DeleteLocationByArea(area, out StringBuilder error);
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
