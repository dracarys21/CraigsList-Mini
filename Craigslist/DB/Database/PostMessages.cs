using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models.Data;
using Models;
using System.Data;
using BizLogic.Logic;
using System.Data.Entity;

namespace DB.Database
{
    public class PostMessages
    {
        public static void CreateMessage(int postId, string userId, string userMessage)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    Post post = UserPost.GetPostById(postId);
                    ApplicationUser author =  (from user in db.Users
                                                where user.Id.Equals(userId)
                                                select user).FirstOrDefault();
                    ApplicationUser receiver = (from user in db.Users
                                              where user.Id.Equals(post.Author.Id)
                                              select user).FirstOrDefault();
                    var message = new Message
                    {
                        Body = userMessage,
                        SendTo = receiver ,
                        CreatedBy = author,
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

        public static List<Message> GetRecentMessagesByUser(string userid)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {

                    ApplicationUser receiver = (from user in db.Users
                                            where user.Id.Equals(userid)
                                            select user).FirstOrDefault();

                    var messages = from message in db.Messages
                        where message.SendTo.Id.Equals(receiver.Id)
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
        
        public static List<Message> GetMessagesByPost(int postId)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var messages = from p in db.Posts
                        where p.Id.Equals(postId)
                        select p.Messages;

                    return messages.FirstOrDefault().OrderByDescending(i => i.CreateDate).ToList();
                       
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        } 

        public static void DeleteResponse(Message message, ApplicationUser user, Post post, out StringBuilder errors)//Can be used for deleting a response or marking a response as read.
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

        public static void ReadResponse(Message message, ApplicationUser user, Post post, out StringBuilder errors)//Can be used for deleting a response or marking a response as read.
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
