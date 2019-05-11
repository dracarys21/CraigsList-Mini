using Data.Models.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizLogic.Logic;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace DB.Database
{
    class LocationOps
    {
        public void CreateLocation(string area, string locale, string slug)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var location = new Location
                    {
                        Area = area,
                        Locale = locale,
                        Slug = slug,
                        Active = false
                    };

                    db.Locations.Add(location);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Location GetLocationById(int locationId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.Locations.Find(locationId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DeleteLocationById(ApplicationUser user, int locationId, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var location = GetLocationById(locationId);

                    if (location == null)
                    {
                        errors.Append("Location does not exist\n");
                        return;
                    }

                    if (!LocationActions.CanCRUDLocation(user, location))
                    {
                        errors.Append("Location cannot be deleted");
                        return;
                    }

                    db.Locations.Remove(location);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateLocation(ApplicationUser user, Location location, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                if (!LocationActions.CanCRUDLocation(user, location))
                {
                    errors.Append("Location cannot be updated.");
                    return;
                }

                using (var db = new ApplicationDbContext())
                {
                    var fetchedLocation = GetLocationById(location.Id);

                    if (fetchedLocation == null)
                    {
                        errors.Append("Location does not exist.");
                        return;
                    }

                    fetchedLocation.Active = location.Active;
                    fetchedLocation.Slug = location.Slug;

                    db.Locations.AddOrUpdate(fetchedLocation);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void EditLocation(ApplicationUser user, Location location, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                if (!LocationActions.CanCRUDLocation(user, location))
                {
                    errors.Append("Location cannot be edited.");
                    return;
                }

                using (var db = new ApplicationDbContext())
                {
                    var fetchedLocation = GetLocationById(location.Id);

                    if (fetchedLocation == null)
                    {
                        errors.Append("Location does not exist.");
                        return;
                    }

                    fetchedLocation.Area = location.Area;
                    fetchedLocation.Locale = location.Locale;

                    db.Locations.AddOrUpdate(fetchedLocation);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
