﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Data;
using Models;
namespace DB.Database
{
    public class PostTypesOps
    {
        public static void CreatePostTypes(string category, string subcategory, string slug)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var postTypes= new PostType
                    {
                        Category= category,
                        SubCategory = subcategory,
                        Slug = slug,
                        Active = true
                    };

                    db.PostType.Add(postTypes);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static ICollection<PostType> GetDistinctPostTypes()
        {
            using (var db = new ApplicationDbContext())
            {
                var pt = from p in db.PostType
                         where p.Active == true
                         select p;
                return pt.GroupBy(p => p.Category).Select(p => p.FirstOrDefault()).ToList();
                        
            }
        }

        public static PostType GetPostTypesById(int? PostTypeId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.PostType.Find(PostTypeId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void DeletePostTypeByCategory(string category, out StringBuilder errors)
        {
            errors = new StringBuilder();
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var postTypes = GetPostTypesByCategory(category);

                    if (postTypes == null)
                    {
                        errors.Append("Location does not exist\n");
                        return;
                    }

                    foreach (var l in postTypes)
                    {
                        l.Active = false;
                        db.PostType.AddOrUpdate(l);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }

        }


        public static ICollection<PostType> GetPostTypesByCategory(string category)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var locales = from loc in db.PostType
                                  where loc.Category == category && loc.Active
                                  select loc;
                    return locales.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static void DeletePostTypeById(ApplicationUser user, int postTypeId, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var location = GetPostTypesById(postTypeId);

                    if (location == null)
                    {
                        errors.Append("Location does not exist\n");
                        return;
                    }
                    string roleId = UserRoles.GetAdminRoleId();
                  /*  if (!LocationActions.CanCRUDLocation(user, location))
                    {
                        errors.Append("Location cannot be deleted");
                        return;
                    }*/

                    PostType Postype = GetPostTypesById(postTypeId);
                   Postype.Active = false;
                    db.PostType.AddOrUpdate(Postype);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void UpdatePostTypes(ApplicationUser user, PostType postType, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                string roleId = UserRoles.GetAdminRoleId();
               /* if (!LocationActions.CanCRUDLocation(user, location))
                {
                    errors.Append("Location cannot be updated.");
                    return;
                }*/

                using (var db = new ApplicationDbContext())
                {
                    var fetchedPostType = GetPostTypesById(postType.Id);

                    if (fetchedPostType == null)
                    {
                        errors.Append("Location does not exist.");
                        return;
                    }

                    fetchedPostType.Slug = postType.Slug;
                    fetchedPostType.Category = postType.Category;
                    fetchedPostType.SubCategory = postType.SubCategory;

                    db.PostType.AddOrUpdate(fetchedPostType);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
