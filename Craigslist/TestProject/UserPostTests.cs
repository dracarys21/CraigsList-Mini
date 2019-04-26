using System;
using BizLogic.Logic;
using Data.Models.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace TestProject
{
    [TestClass]
    public class UserPostTests
    {
        private Post GetDummyPost()
        {
            return new Post
            {
                Title = "Post 1",
                Body = "Body of post",
                Author = new ApplicationUser
                {
                    UserName = "testuser",
                    Email = "test@test.com"
                },
                Location = new Location
                {
                    Id = 1,
                    Active = true,
                    Area = "New York",
                    Locale = "Brooklyn",
                    Slug = "NY-BK"
                },
                PostType = new PostType
                {
                    Id = 1,
                    Active = true,
                    Category = "Housing",
                    SubCategory = "Apartment",
                    Slug = "housing-apartment"
                }
            };
        }

        [TestMethod]
        public void CreatePostHappyPath()
        {
            var post = GetDummyPost();

            Assert.IsTrue(PostActions.CanCreatePost(post.Author, post));
        }

        [TestMethod]
        public void CreatePostRequiresTitle()
        {
            var post = GetDummyPost();
            post.Title = string.Empty;

            Assert.IsFalse(PostActions.CanCreatePost(post.Author, post));
        }

        [TestMethod]
        public void CreatePostRequiresBody()
        {
            var post = GetDummyPost();
            post.Body = string.Empty;

            Assert.IsFalse(PostActions.CanCreatePost(post.Author, post));
        }

        [TestMethod]
        public void CreatePostRequiresLocationObject()
        {
            var post = GetDummyPost();
            post.Location = null;

            Assert.IsFalse(PostActions.CanCreatePost(post.Author, post));
        }

        public void CreatePostRequiresLocationArea()
        {
            var post = GetDummyPost();
            post.Location = new Location
            {
                Locale = "Brooklyn"
            };

            Assert.IsFalse(PostActions.CanCreatePost(post.Author, post));
        }

        public void CreatePostRequiresLocationLocale()
        {
            var post = GetDummyPost();
            post.Location = new Location
            {
                Area = "Brooklyn"
            };

            Assert.IsFalse(PostActions.CanCreatePost(post.Author, post));
        }

        [TestMethod]
        public void CreatePostRequiresPostTypeObject()
        {
            var post = GetDummyPost();
            post.PostType = null;

            Assert.IsFalse(PostActions.CanCreatePost(post.Author, post));
        }

        [TestMethod]
        public void CreatePostRequiresPostTypeCategory()
        {
            var post = GetDummyPost();
            post.PostType = new PostType
            {
                SubCategory = "Apartment"
            };

            Assert.IsFalse(PostActions.CanCreatePost(post.Author, post));
        }

        [TestMethod]
        public void CreatePostRequiresPostTypeSubCategory()
        {
            var post = GetDummyPost();
            post.PostType = new PostType
            {
                Category = "Apartment"
            };

            Assert.IsFalse(PostActions.CanCreatePost(post.Author, post));
        }

        [TestMethod]
        public void DeletePostHappyPath()
        {
            var post = GetDummyPost();

            Assert.IsTrue(PostActions.CanDeletePost(post.Author, post));
        }

        [TestMethod]
        public void DeletePostCannotDelete()
        {
            var post = GetDummyPost();
            var user = new ApplicationUser
            {
                Id = "Some-random-string",
                UserName = "testuser2",
                Email = "test2@test.com"
            };

            Assert.IsFalse(PostActions.CanDeletePost(user, post));
        }

        [TestMethod]
        public void UpdatePostHappyPath()
        {
            var post = GetDummyPost();

            Assert.IsTrue(PostActions.CanUpdatePost(post.Author, post));
        }

        [TestMethod]
        public void UpdatePostCannotUpdateWrongUser()
        {
            var post = GetDummyPost();
            var user = new ApplicationUser
            {
                Id = "testuser2",
                UserName = "testuser2",
                Email = "test2@test.com"
            };

            Assert.IsFalse(PostActions.CanUpdatePost(user, post));
        }

        [TestMethod]
        public void UpdatePostCannotUpdateExpiredPost()
        {
            var post = GetDummyPost();
            post.ExpirationDate = DateTime.Now.AddDays(-1);

            Assert.IsFalse(PostActions.CanUpdatePost(post.Author, post));
        }
    }
}
