using System;

namespace DB.Migrations
{
    using System.Data.Entity.Migrations;
    using Data.Models.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<DB.Database.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DB.Database.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            for (var pId = 1; pId < 11; pId++)
            {
                for (var cId = 1; cId < 6; cId++)
                {
                    context.Locations.AddOrUpdate(new Location
                    {
                        Active = true,
                        Area = $"Area {pId}",
                        Locale = $"Locale {cId}",
                        Slug = $"area-{pId}_locale-{cId}"
                    });

                    context.PostTypes.AddOrUpdate(new PostType
                    {
                        Active = true,
                        Category = $"Category {pId}",
                        SubCategory = $"SubCategory {cId}",
                        Slug = $"category-{pId}_sub-{cId}"
                    });

                    var user = context.Users.Find("c055ac54-b117-4785-bf89-9d6ca98f6d2d");

                    
   
                }
            }


        }
    }
}
