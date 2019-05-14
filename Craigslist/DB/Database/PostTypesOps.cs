using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using Data.Models.Data;
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

        public static PostType GetPostTypeById(int postTypeId)
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
                    var postType = GetPostTypeById(postTypeId);

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
                    var fetchedPostType = GetPostTypeById(postType.Id);

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

        public static ICollection<PostType> GetDistinctPostTypes()
        {
            using (var db = new ApplicationDbContext())
            {
                var pt = from p in db.PostTypes
                         where p.Active == true
                         select p;
                return pt.GroupBy(p => p.Category).Select(p => p.FirstOrDefault()).ToList();

            }
        }

    }
}
