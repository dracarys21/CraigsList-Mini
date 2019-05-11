using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Data;
using Models;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using BizLogic.Logic;

namespace DB.Database
{
    class PostMessages
    {
        public void CreateMessage(Post Post, ApplicationUser CreatedBy, string Message_String)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    
                    var message = new Message
                    {
                        Body = Message_String,
                        SendTo = Post.Author,
                        CreatedBy= CreatedBy,
                        CreateDate = DateTime.Now,
              
                    };

                    
  
                    Post.Messages.Add(message);
                    db.Message.Add(message);
                    db.SaveChanges();
                }
            }

            
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public List<Message> GetRecentMessagesByUser(ApplicationUser User)
        {
            try
            {
                List<Message> Messages = new List<Message>();
                List<Post> posts = UserPost.GetPostsByUser(User);
                foreach (var post in posts)
                {
                    foreach (var message in post.Messages)
                    {
                        Messages.Add(message);
                    }

                }
                return Messages.OrderByDescending(i => i.CreateDate).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        public List<Message> GetMessageByPost(Post Post)
        {
            try
            {
                return Post.Messages.OrderByDescending(i => i.CreateDate).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        } 

        public void DeleteResponse(Message message, ApplicationUser user, Post post, out StringBuilder errors)//Can be used for deleting a response or marking a response as read.
        {
            try
            {
                errors = new StringBuilder();
                if (!MessageAction.CanUpdateMessageDatabase(message, user, post))
                {
                    errors.Append("Post ");
                    return;
                }
                message.Deleted = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void ReadResponse(Message message, ApplicationUser user, Post post, out StringBuilder errors)//Can be used for deleting a response or marking a response as read.
        {
            try
            {
                errors = new StringBuilder();
                if (!MessageAction.CanUpdateMessageDatabase(message, user, post))
                {
                    errors.Append("Post ");
                    return;
                }
                message.Read = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
