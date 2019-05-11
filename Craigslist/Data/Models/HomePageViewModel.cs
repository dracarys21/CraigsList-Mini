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
        public Dictionary<string, List<string>> AllLocations;

        public Dictionary<string, List<string>> AllCategories;

        public Location CurrentLocation;

        public HomePageViewModel(Dictionary<string, List<string>> locationList, Dictionary<string, List<string>> categoryList)
        {
            AllCategories = categoryList;
            AllLocations = locationList;
        }
    }
}
