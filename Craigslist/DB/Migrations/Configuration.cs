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



            for (int i = 1; i < 11; i++)
            {
                var displayId = i.ToString();

                context.Locations.AddOrUpdate(new Location
                {
                    Id = i,
                    Active = true,
                    Area = $"Area {displayId}",
                    Locale = $"Locale {displayId}",
                    Slug = $"area-{displayId}_locale-{displayId}"
                });

                context.PostType.AddOrUpdate(new PostType
                {
                    Id = i,
                    Active = true,
                    Category = $"Category {displayId}",
                    SubCategory = $"SubCategory {displayId}",
                    Slug = $"category-{displayId}_sub-{displayId}"
                });
            }
        }
    }
}
