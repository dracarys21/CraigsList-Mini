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

            for (int i = 0; i < 10; i++)
            {
                context.Locations.Add(new Location
                {
                    Id = i,
                    Active = true,
                    Area = $"Area {i.ToString()}",
                    Locale = $"Locale {i.ToString()}",
                    Slug = $"area-{i.ToString()}_locale-{i.ToString()}"
                });

                context.PostType.Add(new PostType
                {
                    Id = i,
                    Active = true,
                    Category = $"Category {i.ToString()}",
                    SubCategory = $"SubCategory {i.ToString()}",
                    Slug = $"category-{i.ToString()}_sub-{i.ToString()}"
                });
            }
        }
    }
}
