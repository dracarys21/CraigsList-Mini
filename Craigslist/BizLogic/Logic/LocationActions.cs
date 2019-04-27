using Data.Models.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BizLogic.Logic
{
    public static class LocationActions
    {
        public static bool CanCRUDLocation(ApplicationUser user, Location newLocation)
        {

            var hasPermission = (user.UserRole == "admin") ? true : false;
            var isValidArea = !string.IsNullOrEmpty(newLocation.Area);
            var isValidLocale = !string.IsNullOrEmpty(newLocation.Locale);
            var isValidSlug = IsValidSlug(newLocation.Slug);

            return hasPermission && isValidArea && isValidLocale && isValidSlug;
        }

        public static bool IsValidSlug(string slugString)
        {
            Regex regex = new Regex(@"^[a-z\d](?:[a-z\d_-]*[a-z\d])?$");
            return regex.IsMatch(slugString);
        }
    }
}
