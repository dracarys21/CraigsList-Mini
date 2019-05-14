using Data.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class HomePageViewModel
    {
        public Dictionary<string, List<Location>> AllLocations;

        public Dictionary<string, List<PostType>> AllCategories;

        public string CurrentLocation;

        public HomePageViewModel(Dictionary<string, List<Location>> locationList, Dictionary<string, List<PostType>> categoryList)
        {
            AllCategories = categoryList;
            AllLocations = locationList;
            CurrentLocation = locationList.Values.First().First().Area;
        }
    }
}
