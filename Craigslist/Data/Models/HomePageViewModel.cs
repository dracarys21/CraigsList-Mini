using Data.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace Data.Models
{
    public class HomePageViewModel
    {
        public Dictionary<string, List<Location>> AllLocations;

        public Dictionary<string, List<PostType>> AllCategories;

        public string CurrentLocation;

        public HomePageViewModel(List<Location> locations, List<PostType> postTypes)
        {
            AllLocations = new Dictionary<string, List<Location>>();
            AllCategories = new Dictionary<string, List<PostType>>();

            var categories = from postType in postTypes
                group postType.Category by postType.Category into category
                select category.Key;

            foreach (var category in categories)
            {
                var subCategories = from postType in postTypes
                    where postType.Category.Equals(category)
                    select postType;

                AllCategories.Add(category, subCategories.ToList());
            }

            var areas = from location in locations
                group location.Area by location.Area into area
                select area.Key;

            foreach (var area in areas)
            {
                var locales = from location in locations
                    where location.Area.Equals(area)
                    select location;

                AllLocations.Add(area, locales.ToList());
            }
        }
    }
}
