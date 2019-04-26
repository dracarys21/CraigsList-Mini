using BizLogic.Logic;
using Data.Models.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class PostMessageTests
    {
        int messageCount = 5;

        private Post GetPostWithMessages()
        {
            Post post= UserPostTests.GetDummyPost();
            
            for(int i = 0; i<messageCount; i++)
            {
                var message = new Message
                {
                    Id = i,
                    Body = "testing",
                    CreatedBy = new ApplicationUser { Id = i + "" },
                    SendTo = post.Author,
                };

                post.Messages.Add(message);
            }

            return post;
            
        }

        [TestMethod]
        public void CreateTestSuccessfull()
        {
            var post = GetPostWithMessages();

            foreach (var m in post.Messages)
            {
                Assert.IsTrue(MessageAction.CanCreateMessage(m, m.CreatedBy, post));
            }
        }

        [TestMethod]
        public void  RequiredMessageBody()
        {
            var post = GetPostWithMessages();
            foreach (var m in post.Messages)
            {
                    m.Body = "";
                    Assert.IsFalse(MessageAction.CanCreateMessage(m, m.CreatedBy, post));
            }
        }


        [TestMethod]
        public void RequiredSender()
        {
            var post = GetPostWithMessages();

            int i = 0;
            foreach (var m in post.Messages)
            {
                    Assert.IsFalse(MessageAction.CanCreateMessage(m, null, post));
            }
        }
        [TestMethod]
        public void RequiredReciever()
        {
            var post = GetPostWithMessages();
            foreach (var m in post.Messages)
            {
                m.SendTo = null;
                Assert.IsFalse(MessageAction.CanCreateMessage(m, m.CreatedBy, post));
            }
        }
        [TestMethod]
        public void CheckRecieverIsAuthor()
        {
            var post = GetPostWithMessages();
            foreach (var m in post.Messages)
           {
                    m.SendTo = new ApplicationUser {Id = "555" };
                    Assert.IsFalse(MessageAction.CanCreateMessage(m, m.CreatedBy, post));
           }
        }


        [TestMethod]
        public void CheckPostForMessages()
        {
            var post = GetPostWithMessages();
            var newpost = UserPostTests.GetDummyPost();
            newpost.Id = 2;
            foreach (var m in post.Messages)
            {
                    m.Body = "";
                    Assert.IsFalse(MessageAction.CanCreateMessage(m, m.CreatedBy, newpost));
            }
        }

        [TestMethod]
        public void NotSenderMessageDelete()
        {
            var post = GetPostWithMessages();
            foreach (var m in post.Messages)
            {
                Assert.IsFalse(MessageAction.CanDeleteMessage(m, m.CreatedBy, post));
            }
        }

        [TestMethod]
        public void RequiredUserToDelete()
        {
            var post = GetPostWithMessages(); 
            foreach (var m in post.Messages)
            {
                    Assert.IsFalse(MessageAction.CanDeleteMessage(m, null, post));
            }
        }
        [TestMethod]
        public void RequiredMessageToDelete()
        {
            var post = GetPostWithMessages();
            Assert.IsFalse(MessageAction.CanDeleteMessage(null, post.Author, post));
            
        }

    }
 }
