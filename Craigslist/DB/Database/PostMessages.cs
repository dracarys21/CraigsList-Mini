using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models.Data;
using Models;
using System.Data;
using BizLogic.Logic;
using System.Data.Entity;
using Data.Models;
using System.Data.Entity.Migrations;

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
                        SendTo = receiver,
                        CreatedBy = author,
                        CreateDate = DateTime.Now,
                        postId = postId,
                        Deleted = false
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
                                   where message.SendTo.Id.Equals(receiver.Id) && message.Deleted == false
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

                    return messages.FirstOrDefault().OrderByDescending(i => i.CreateDate).Where(m => m.Deleted==false).ToList();
                       
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        } 
        
        public static void DeleteResponse(int messageId,string userId, out StringBuilder errors)//Can be used for deleting a response or marking a response as read.
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    errors = new StringBuilder();
                    Message message = GetMessageById(messageId);
                    ApplicationUser user = UserRoles.GetUserById(userId);
                    Post post = UserPost.GetPostById(message.postId);

                    if (!MessageAction.CanUpdateMessageDatabase(message, user, post))
                    {
                        errors.Append("Can't delete Message");
                        return;
                    }
                    message.Deleted = true;
                    db.Messages.AddOrUpdate(message);
                    db.SaveChanges();
                } 
                
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
        public static Message GetMessageById(int id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var message = db.Messages.Include("CreatedBy").Include("SendTo")
                        .FirstOrDefault(p => p.Id.Equals(id)
                                             && !p.Deleted);
                    return message;
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
