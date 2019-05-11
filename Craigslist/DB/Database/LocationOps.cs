using Data.Models.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizLogic.Logic;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;

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
                    return db.Locations.Find(locationId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static Dictionary<string,  List<string>> GetAllLocations()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var allLocations = from loc in db.Locations
                                       where loc.Active == true
                                       select new
                                       {
                                           area = loc.Area,
                                           locale = loc.Locale
                                       };

                    var locationGroup = from loc in allLocations
                                        group loc by loc.area into newGroup
                                        select newGroup;

                    Dictionary<string, List<string>> ActiveLocations = new Dictionary<string, List<string>>();
                    foreach (var location in locationGroup)
                    {
                        string areaName = location.Key;
                        List<string> localeList = new List<string>();
                        foreach (var locale in location)
                        {
                            localeList.Add(locale.locale);
                        }
                        ActiveLocations.Add(areaName, localeList);
                    }
                    return ActiveLocations;
                }
            }
            catch (Exception e)
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
                                  select loc;
                    return locales;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static void DeleteLocationById(ApplicationUser user, int locationId, out StringBuilder errors)
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
                    string roleId = UserRoles.GetAdminRoleId();
                    if (!LocationActions.CanCRUDLocation(user, location))
                    {
                        errors.Append("Location cannot be deleted");
                        return;
                    }

                    Location loccation = GetLocationById(locationId);
                    location.Active = false;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void UpdateLocation(ApplicationUser user, Location location, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                string roleId = UserRoles.GetAdminRoleId();
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
