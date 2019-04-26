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


namespace DB.Database
{
    class UserInbox
    {
        public void CreateMessage(Post Post, ApplicationUser CreatedBy, string Message_String)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    
                    var message = new Message
                    {
                        MessageString = Message_String,
                        SendTo = Post.Author,
                        CreatedBy= CreatedBy,
                        CreateDate = DateTime.Now,
              
                    };

                    Post.Author.Inbox.Messages.Add(message);
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
            var messages =  User.Inbox.Messages.OrderByDescending(i => i.CreateDate);
            return messages.ToList();
        }
        
        public List<Message> GetMessageByPost(Post Post)
        {
            return Post.Messages.OrderByDescending(i => i.CreateDate).ToList();
        }
    }
}
