using Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Migrations;

namespace DB.Database
{
    public class LocationOps
    {
        public static void CreateLocation(string area, string locale, string slug)
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
                        Active = true
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

        public static Location GetLocationById(int locationId)
        { 
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.Locations
                        .FirstOrDefault(l => l.Id.Equals(locationId) && l.Active);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static ICollection<Location> GetDistinctAreas()
        {
            using (var db = new ApplicationDbContext())
            {
                var pt = from p in db.Locations
                         where p.Active == true
                         select p;
                var r = pt.GroupBy(p => p.Area).Select(p => p.FirstOrDefault()).ToList();
                return r;
            }
        }

        public static List<Location> GetActiveLocationsList()
        {
            try {
                using (var db = new ApplicationDbContext())
                {
                    var locations = from location in db.Locations
                        where location.Active
                        select location;

                    return locations.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static Location GetLocationByAreaAndLocale(string area, string locale)
        {
            try
            {
                using(var db =  new ApplicationDbContext())
                {
                    var result = from loc in db.Locations
                        where loc.Area.Equals(area) 
                              && loc.Locale.Equals(locale)
                              && loc.Active
                        select loc;

                    return result.FirstOrDefault();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static IEnumerable<Location> GetLocalesByArea(string area)
        {
            try
            {
                using(var db =  new ApplicationDbContext())
                {
                    var locales = from loc in db.Locations
                                  where loc.Area == area
                                        && loc.Active
                                  select loc;

                    return locales.ToList();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public static void DeleteLocationById(int locationId, out StringBuilder errors)
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
                    
                    location.Active = false;
                    db.Locations.AddOrUpdate(location);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void UpdateLocation(Location location, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var fetchedLocation = GetLocationById(location.Id);

                    if (fetchedLocation == null)
                    {
                        errors.Append("Location does not exist.\n");
                        return;
                    }

                    fetchedLocation.Slug = location.Slug;
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

        public static void DeleteLocationByArea(string area, out StringBuilder errors)
        {
            errors = new StringBuilder();
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var location = GetLocalesByArea(area);

                    if (location == null)
                    {
                        errors.Append("Location does not exist\n");
                        return;
                    }           
                    foreach (var l in location)
                    {
                        l.Active = false;
                        db.Locations.AddOrUpdate(l);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string GetAreaByLocale(string locale)
        { 
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var area = from location in db.Locations
                        where location.Active
                              && location.Locale.ToLower().Equals(locale.ToLower())
                        select location.Area;

                    return area.ToList().FirstOrDefault();;
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
