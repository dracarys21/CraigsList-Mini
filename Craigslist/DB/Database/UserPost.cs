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
            PostType postType, Location location)
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

        public static List<Post> GetPostsByUser(ApplicationUser user)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var posts = from post in db.Posts
                        where post.Author.Equals(user)
                              && post.Deleted == false
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

        public static void DeletePostById(ApplicationUser user, int postId,
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
                        
                    if (!PostActions.CanDeletePost(user, post))
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
                    return db.Posts.Find(postId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void UpdatePost(ApplicationUser user, Post post, out StringBuilder errors)
        {
            errors = new StringBuilder();

            try
            {
                if (!PostActions.CanUpdatePost(user, post))
                {
                    errors.Append("Post CANNOT be updated");
                    return;
                }

                using (var db = new ApplicationDbContext())
                {
                    var postFromDb = GetPostById(post.Id);

                    if (postFromDb == null)
                    {
                        errors.Append("Post could not be updated. Please try again later");
                        return;
                    }

                    postFromDb.Body = post.Body;
                    postFromDb.Title = post.Title;
                    postFromDb.ExpirationDate = post.ExpirationDate;
                    postFromDb.Location = post.Location;
                    postFromDb.PostType = post.PostType;
                    postFromDb.LastModifiedBy = user;
                    postFromDb.LastModifiedDate = DateTime.Now;

                    db.Posts.AddOrUpdate(postFromDb);
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
