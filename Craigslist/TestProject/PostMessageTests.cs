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
    class PostMessageTests
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
            
            foreach( var m in post.Messages)
                Assert.IsTrue(MessageAction.CanCreateMessage(m, m.CreatedBy ,post));
        }

    }
}
