using Data.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace Data.Models
{
    public class HomePageViewModel
    {
        public Dictionary<string, List<string>> AllLocations;

        public Dictionary<string, List<string>> AllCategories;

        public Location CurrentLocation;

        public HomePageViewModel(List<Location> locations, List<PostType> postTypes)
        {
            AllLocations = new Dictionary<string, List<string>>();
            AllCategories = new Dictionary<string, List<string>>();

            var categories = from postType in postTypes
                group postType.Category by postType.Category into category
                select category.Key;

            foreach (var category in categories)
            {
                var subCategories = from postType in postTypes
                    where postType.Category.Equals(category)
                    select postType.SubCategory;

                AllCategories.Add(category, subCategories.ToList());
            }

            var areas = from location in locations
                group location.Area by location.Area into area
                select area.Key;

            foreach (var area in areas)
            {
                var locales = from location in locations
                    where location.Area.Equals(area)
                    select location.Locale;

                AllLocations.Add(area, locales.ToList());
            }
        }
    }
}
