using System.Collections.Generic;
using Data.Models.Data;
using DB.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;

namespace DB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DB.Database.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DB.Database.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Default Locations
            var nyLocations = new List<string>() { "Brooklyn", "Bronx", "Manhattan", "Queens", "Staten Island" };
            var virginiaLocations = new List<string>() { "Hilltop", "Alanton", "Lagomar", "Kempsville", "Virginia Beach" };
            var maineLocations = new List<string>() { "South Portland", "Scarborough", "Brunswick", "Cumberland", "Topsham" };
            var CaliforniaLocations = new List<string>() { "Central LA", "Hollywood", "Wilshire", "Downtown", "Harbor", "San Pedro" };
            
            foreach (var location in nyLocations)
            {
                context.Locations.AddOrUpdate(new Location
                {
                    Area = "New York",
                    Locale = location,
                    Slug = $"new-york_{location.ToLower().Replace(" ", "-")}",
                    Active = true
                }); 
            }

            foreach (var location in virginiaLocations)
            {
                context.Locations.AddOrUpdate(new Location
                {
                    Area = "Virginia",
                    Locale = location,
                    Slug = $"virginia_{location.ToLower().Replace(" ", "-")}",
                    Active = true
                }); 
            }

            foreach (var location in maineLocations)
            {
                context.Locations.AddOrUpdate(new Location
                {
                    Area = "Maine",
                    Locale = location,
                    Slug = $"maine_{location.ToLower().Replace(" ", "-")}",
                    Active = true
                }); 
            }

            foreach (var location in CaliforniaLocations)
            {
                context.Locations.AddOrUpdate(new Location
                {
                    Area = "California",
                    Locale = location,
                    Slug = $"california_{location.ToLower().Replace(" ", "-")}",
                    Active = true
                }); 
            }
        }
    }
}
