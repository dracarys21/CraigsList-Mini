using System;
using System.Collections.Generic;
using System.Linq;
using BizLogic.Logic;
using Data.Models.Data;
using Models;

namespace DB.Database
{
    public class UserPost
    {
        public void CreatePost(ApplicationUser createdBy, string title, string body)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var post = new Post {
                        Title = title,
                        Body = body,
                        Deleted = false,
                        CreateDate = DateTime.Now,
                        Author = createdBy,
                        LastModifiedDate = DateTime.Now,
                        LastModifiedBy = createdBy
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

        public List<Post> GetPostsByUser(ApplicationUser user)
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

        public void DeletePostById(int postId, ApplicationUser user)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    throw new NotImplementedException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Post GetPostById(int postId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    throw new NotImplementedException();
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
