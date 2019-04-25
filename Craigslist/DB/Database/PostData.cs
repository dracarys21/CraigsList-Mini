using System;
using BizLogic.Logic;
using Data.Models.Data;
using Models;

namespace DB.Database
{
    public class PostData
    {
        public void CreatePost(Post post, ApplicationUser createdBy)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var newPost = PostActions.CreatePost(post, createdBy);
                    db.Posts.Add(newPost);
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
