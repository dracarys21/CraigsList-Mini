using System.Linq;
using System.Net;
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
        // GET: Locations
        public ActionResult Index()
        {
            return View(LocationOps.GetDistinctLocation());
        }


        //Get: Locations/{area}
        public ActionResult ListLocale(string area)
        {
            return View(LocationOps.GetLocalesByArea(area).ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Location location = LocationOps.GetLocationById(id.Value);
            
            if (location == null)
                return HttpNotFound();

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
        public ActionResult Create([Bind(Include = "Area,Locale,Slug")] Location location)
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
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Location location = LocationOps.GetLocationById(id.Value);
            
            if (location == null)
                return HttpNotFound();

            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Area,Locale,Slug,Active")] Location location)
        {
            if (ModelState.IsValid)
            {
                location.Id = id;
                LocationOps.UpdateLocation(location, out StringBuilder errors);

                if (errors.Length > 0)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, errors.ToString());

                return RedirectToAction("ListLocale", routeValues: new { area = location.Area });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Location Data");
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var location = LocationOps.GetLocationById(id.Value);

            if (location == null)
                return HttpNotFound();

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocationOps.DeleteLocationById(id, out StringBuilder errors);

            if (errors.Length > 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, errors.ToString());

            return RedirectToAction("Index");
        }
    }
}
