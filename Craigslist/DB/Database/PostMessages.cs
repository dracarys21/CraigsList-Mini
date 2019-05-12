using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models.Data;
using Models;
using System.Data;
using BizLogic.Logic;

namespace DB.Database
{
    public class PostMessages
    {
        public void CreateMessage(Post post, ApplicationUser createdBy, string userMessage)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    
                    var message = new Message
                    {
                        Body = userMessage,
                        SendTo = post.Author,
                        CreatedBy = createdBy,
                        CreateDate = DateTime.Now,
                    };
  
                    post.Messages.Add(message);
                    db.Messages.Add(message);
                    db.SaveChanges();
                }
            }

            
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Message> GetRecentMessagesByUser(ApplicationUser user)
        {
            try
            {
//                List<Message> messages = new List<Message>();
//                List<Post> posts = UserPost.GetPostsByUserId(user);
//
//                foreach (var post in posts)
//                {
//                    foreach (var message in post.Messages)
//                    {
//                        messages.Add(message);
//                    }
//
//                }
//                return messages.OrderByDescending(i => i.CreateDate).ToList();

                using (var db = new ApplicationDbContext())
                {
                    var messages = from message in db.Messages
                        where message.CreatedBy.Equals(user)
                        orderby message.CreateDate descending
                        select message;

                    return messages.ToList();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public List<Message> GetMessageByPost(Post post)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var messages = from p in db.Posts
                        where p.Equals(post)
                        select p.Messages;

                    return messages
                        .Select(i => i.FirstOrDefault())
                        .OrderByDescending(i => i.CreateDate).ToList();
                }
//                return post.Messages.OrderByDescending(i => i.CreateDate).ToList();
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
