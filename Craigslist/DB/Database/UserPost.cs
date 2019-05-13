using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using BizLogic.Logic;
using Data.Models.Data;
using Models;

namespace DB.Database
{
    public static class UserPost
    {
        public static void CreatePost(string createdByUserId, string title, string body,
            string category, string subCategory, string area, string locale)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var createdBy = (from user in db.Users
                        where user.Id.Equals(createdByUserId)
                        select user).FirstOrDefault();

                    if (createdBy == null)
                        throw new Exception("User not allowed to create a post");

                    var postType = PostTypesOps.GetPostTypeByCategoryAndSubCategory(
                        category, subCategory);

                    if (postType == null)
                        throw new Exception("Post Type is invalid");

                    var location = LocationOps.GetLocationByAreaAndLocale(area, locale);

                    if (location == null)
                        throw new Exception("Location is invalid");

                    var post = new Post {
                        Title = title,
                        Body = body,
                        Deleted = false,
                        CreateDate = DateTime.Now,
                        Author = createdBy,
                        LastModifiedDate = DateTime.Now,
                        LastModifiedBy = createdBy,
                        PostType = postType,
                        Location = location,
                        ExpirationDate = DateTime.Now.AddDays(5)
                    };

                    db.Posts.Add(post);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static List<Post> GetPostsByUserId(string userId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var posts = from post in db.Posts
                        where post.Author.Id.Equals(userId)
                              && !post.Deleted
                        select post;

                    return posts.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void DeletePostByPostId(string userId, int postId,
            out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var post = GetPostById(postId);

                    if (post == null)
                    {
                        errors.Append("Post does not exist\n");
                        return;
                    }
                        
                    if (!PostActions.CanDeletePost(userId, post))
                    {
                        errors.Append("Post cannot be deleted");
                        return;
                    }

                    post.Deleted = true;
                    db.Posts.AddOrUpdate(post);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static Post GetPostById(int postId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var post = db.Posts
                        .Include("Author")
                        .FirstOrDefault(p => p.Id.Equals(postId)
                                             && !p.Deleted);

                    if (post != null)
                    {
                        if (post.ExpirationDate.HasValue
                            && post.ExpirationDate.Value.CompareTo(DateTime.Now.Date) > 0)
                        {
                            return post;
                        }
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void UpdatePost(string userId, int postId,
            string title, string body, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var post = GetPostById(postId);
                    var updateBy = db.Users.Find(userId);

                    if (!PostActions.CanUpdatePost(updateBy, post))
                    {
                        errors.Append("Post CANNOT be updated");
                        return;
                    }

                    if (post == null)
                    {
                        errors.Append("Post could not be updated. Please try again later");
                        return;
                    }

                    post.Body = body;
                    post.Title = title;
                    post.LastModifiedBy = updateBy;
                    post.LastModifiedDate = DateTime.Now;

                    db.Posts.AddOrUpdate(post);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void RespondToPost(ApplicationUser user, int postId, string message)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //Addition GetUserPost.
    }
}
