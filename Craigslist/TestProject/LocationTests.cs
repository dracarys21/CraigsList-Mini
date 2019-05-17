using BizLogic.Logic;
using Data.Models.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class LocationTests
    {
        private Location GetDummyLocation()
        {
            return new Location
            {
                
                Id = 1,
                Active = true,
                Area = "New York",
                Locale = "Brooklyn",
                Slug = "NY-BK"
                
            };
        }

        [TestMethod]
        public void CreateLocationRequiresArea()
        {
            var location = GetDummyLocation();
            location.Area = null;
            ApplicationUser user = new ApplicationUser();
//            user.UserRole = "admin";
            Assert.IsFalse(LocationActions.CanCRUDLocation(user, location));
        }

        [TestMethod]
        public void CreateLocationRequiresLocale()
        {
            var location = GetDummyLocation();
            location.Locale = null;
            ApplicationUser user = new ApplicationUser();
//            user.UserRole = "admin";
            Assert.IsFalse(LocationActions.CanCRUDLocation(user, location));
        }

        [TestMethod]
        public void CreateLocationRequiresSlugString()
        {
            var location = GetDummyLocation();
            location.Slug = null;
            ApplicationUser user = new ApplicationUser();
//            user.UserRole = "admin";
            Assert.IsFalse(LocationActions.CanCRUDLocation(user, location));
        }

        [TestMethod]
        public void CreateLocationRequiresValidSlugString()
        {
            var location = GetDummyLocation();
            location.Slug = "!@#$%^&*()";
            ApplicationUser user = new ApplicationUser();
//            user.UserRole = "admin";
            Assert.IsFalse(LocationActions.CanCRUDLocation(user, location));
        }

        [TestMethod]
        public void CreateLocationRequiresAdminAccess()
        {
            var location = GetDummyLocation();
            ApplicationUser user = new ApplicationUser();
//            user.UserRole = "user";
            Assert.IsFalse(LocationActions.CanCRUDLocation(user, location));
        }

        [TestMethod]
        public void CheckSlugStringValidator()
        {
            var location = GetDummyLocation();
            string[] invalidStrings = { "@#$%^&*()", "-my-location", "my-location_", "" };
            string[] validStrings = { "mylocation", "my-location" };
            foreach (string invalid in invalidStrings)
            {
                Assert.IsFalse(LocationActions.IsValidSlug(invalid));
            }
            foreach (string valid in validStrings)
            {
                Assert.IsTrue(LocationActions.IsValidSlug(valid));
            }
        }
    }
}
