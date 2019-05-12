using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
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

                    db.PostTypes.Add(postTypes);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static PostType GetPostTypesById(int postTypeId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.PostTypes
                        .FirstOrDefault(p => p.Id.Equals(postTypeId) && p.Active);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static List<PostType> GetActivePostTypesList()
        {
            using (var db = new ApplicationDbContext())
            {
                var postTypes = from postType in db.PostTypes
                    where postType.Active
                    select postType;

                return postTypes.ToList();
            }
        }

//        public static Dictionary<string, List<string>> GetAllPostTypes()
//        {
//            try
//            {
//                using (var db = new ApplicationDbContext())
//                {
//                    var allPostTypes = from postType in db.PostTypes
//                                       where postType.Active == true
//                                       select new
//                                       {
//                                           category = postType.Category,
//                                           subcategory = postType.SubCategory
//                                       };
//
//                    var categoryGroup = from postType in allPostTypes
//                                        group postType by postType.category into newGroup
//                                        select newGroup;
//
//                    Dictionary<string, List<string>> ActivePostTypes = new Dictionary<string, List<string>>();
//                    foreach (var category in categoryGroup)
//                    {
//                        string categoryName = category.Key;
//                        List<string> subcategories = new List<string>();
//                        foreach (var sub in category)
//                        {
//                            subcategories.Add(sub.subcategory);
//                        }
//                        ActivePostTypes.Add(categoryName, subcategories);
//                    }
//                    return ActivePostTypes;
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                throw;
//            }
//        }

        public static PostType GetPostTypeByCategoryAndSubCategory(string category, string subCategory)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var result = from postType in db.PostTypes
                        where postType.Category.Equals(category)
                              && postType.SubCategory.Equals(subCategory)
                              && postType.Active
                        select postType;

                    return result.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public static List<PostType> GetSubCategoriesByCategory(string category)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var postTypes = from postType in db.PostTypes
                                  where postType.Category.Equals(category)
                                  && postType.Active
                                  select postType;
                    return postTypes.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public static void DeletePostTypeById(int postTypeId, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var postType = GetPostTypesById(postTypeId);

                    if (postType == null)
                    {
                        errors.Append("Post Type does not exist\n");
                        return;
                    }
                    
                    postType.Active = false;

                    db.PostTypes.AddOrUpdate(postType);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void UpdatePostType(PostType postType, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var fetchedPostType = GetPostTypesById(postType.Id);

                    if (fetchedPostType == null)
                    {
                        errors.Append("Post Type does not exist.\n");
                        return;
                    }

                    fetchedPostType.Slug = postType.Slug;
                    fetchedPostType.Category = postType.Category;
                    fetchedPostType.SubCategory = postType.SubCategory;

                    db.PostTypes.AddOrUpdate(fetchedPostType);
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
