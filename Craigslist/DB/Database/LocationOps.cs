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

        public static List<Location> GetActiveLocationsList()
        {
            try {
                using (var db = new ApplicationDbContext())
                {
                    var locations = from location in db.Locations
                        where location.Active
                              && location.Active
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

//        public static Dictionary<string,  List<string>> GetAllLocations()
//        {
//            try
//            {
//                using (var db = new ApplicationDbContext())
//                {
//                    var allLocations = from loc in db.Locations
//                                       where loc.Active == true
//                                       select new
//                                       {
//                                           area = loc.Area,
//                                           locale = loc.Locale
//                                       };
//
//                    var locationGroup = from loc in allLocations
//                                        group loc by loc.area into newGroup
//                                        select newGroup;
//
//                    Dictionary<string, List<string>> ActiveLocations = new Dictionary<string, List<string>>();
//                    foreach (var location in locationGroup)
//                    {
//                        string areaName = location.Key;
//                        List<string> localeList = new List<string>();
//                        foreach (var locale in location)
//                        {
//                            localeList.Add(locale.locale);
//                        }
//                        ActiveLocations.Add(areaName, localeList);
//                    }
//                    return ActiveLocations;
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                throw;
//            }
//        }

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
    }
}
