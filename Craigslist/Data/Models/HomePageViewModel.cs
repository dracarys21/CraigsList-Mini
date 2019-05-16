using Data.Models.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Data.Models
{
    public class HomePageViewModel
    {
//        public Dictionary<string, List<Location>> AllLocations;

        public Dictionary<string, List<PostType>> AllCategories;

        public List<string> Areas { get; set; }

        public List<string> Locales { get; set; }
        
        public string Area { get; set; }

        public string Locale { get; set; }

        public string Category { get; set; }

        public string Subcategory { get; set; }

        public HomePageViewModel(List<PostType> postTypes)
        {
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
        }
    }
}
