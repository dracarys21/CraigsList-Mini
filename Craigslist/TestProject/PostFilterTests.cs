using System;
using System.Collections.Generic;
using System.Linq;
using BizLogic.Logic;
using Data.Models.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace TestProject
{
    [TestClass]
    public class PostFilterTests
    {
        private const int TestPostCount = 50;

        private static List<Post> GetDummyPosts()
        {
            var posts = new List<Post>();
            var counter = 0;
            var innerCounter = 0;

            for (var i = 0; i < TestPostCount; i++)
            {
                var post = new Post
                {
                    Id = i,
                    Title = $"Post title {i}",
                    Body = $"Body of post for post id {i}",
                    Author = new ApplicationUser
                    {
                        UserName = $"test{i}",
                        Email = $"test{i}@test.com"
                    },
                    Location = new Location
                    {
                        Id = i,
                        Active = true,
                        Area = $"State {counter}",
                        Locale = $"City for {innerCounter}",
                        Slug = $"state-{counter}-city-{innerCounter}"
                    },
                    PostType = new PostType
                    {
                        Id = i,
                        Active = true,
                        Category = $"Category {counter}",
                        SubCategory = $"SubCategory {innerCounter}",
                        Slug = $"category-{counter}-subcategory-{innerCounter}"
                    },
                    Deleted = i % 5 == 0,
                    ExpirationDate = DateTime.Now.AddDays(2)
                };

                posts.Add(post);

                if (i % 5 == 0)
                {
                    counter++;
                    innerCounter = 0;
                }

                innerCounter++;
            }

            return posts.ToList();
        }

        [TestMethod]
        public void GetAllPostsWithNoFilter()
        {
            var posts = GetDummyPosts();

            // Make sure the dummy data is good
            Assert.AreEqual(TestPostCount, posts.Count);

            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());
        }

        [TestMethod]
        public void VerifyPostsAreNotExpired()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            posts[3].ExpirationDate = DateTime.Now.AddDays(-3);
            filteredPosts = PostActions.FilterPost(posts);

            Assert.AreNotEqual(activePostCount, filteredPosts.Count());
        }
        
        [TestMethod]
        public void VerifyPostsNotReturnedWithNoExpiration()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            posts[3].ExpirationDate = (DateTime?) null;
            filteredPosts = PostActions.FilterPost(posts);

            Assert.AreNotEqual(activePostCount, filteredPosts.Count());
        }
        
        [TestMethod]
        public void VerifyPostsWithOnlyArea()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            filteredPosts = PostActions.FilterPost(posts, "State 1");

            Assert.AreEqual(filteredPosts.Count(), 4);
        }
        
        [TestMethod]
        public void VerifyPostsWithOnlyLocale()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            filteredPosts = PostActions.FilterPost(posts, locale: "City for 1");

            Assert.AreEqual(filteredPosts.Count(), 10);
        }
        
        [TestMethod]
        public void VerifyPostsWithBothAreaAndLocale()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            filteredPosts = PostActions.FilterPost(posts, "State 1",
                locale: "City for 1");

            Assert.AreEqual(filteredPosts.Count(), 1);
        }
        
        [TestMethod]
        public void VerifyPostsWithOnlyCategory()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            filteredPosts = PostActions.FilterPost(posts, category: "Category 1");

            Assert.AreEqual(filteredPosts.Count(), 4);
        }
        
        [TestMethod]
        public void VerifyPostsWithOnlySubCategory()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            filteredPosts = PostActions.FilterPost(posts, subCategory: "SubCategory 1");

            Assert.AreEqual(filteredPosts.Count(), 10);
        }
        
        [TestMethod]
        public void VerifyPostsWithBothCategorySubCategory()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            filteredPosts = PostActions.FilterPost(posts, category: "Category 1",
                subCategory: "SubCategory 2");

            Assert.AreEqual(filteredPosts.Count(), 1);
        }
        
        [TestMethod]
        public void VerifyPostsNoQueryMatches()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            filteredPosts = PostActions.FilterPost(posts, query: "Something");

            Assert.AreEqual(filteredPosts.Count(), 0);
        }
        
        [TestMethod]
        public void VerifyPostsQueryMatchesTitle()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            filteredPosts = PostActions.FilterPost(posts, query: "title");

            Assert.AreEqual(filteredPosts.Count(), activePostCount);
        }
        
        [TestMethod]
        public void VerifyPostsQueryMatchesBody()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            filteredPosts = PostActions.FilterPost(posts, query: "body");

            Assert.AreEqual(filteredPosts.Count(), activePostCount);
        }
        
        [TestMethod]
        public void VerifyPostsQueryMatchesBothBodyAndTitle()
        {
            var posts = GetDummyPosts().ToList();
            var filteredPosts = PostActions.FilterPost(posts);

            // 20% of posts are deleted
            var activePostCount = TestPostCount - (TestPostCount * .2);

            Assert.AreEqual(activePostCount, filteredPosts.Count());

            // Update the Dummy Posts data and make sure 1 less record is returned
            filteredPosts = PostActions.FilterPost(posts, query: "post");

            Assert.AreEqual(filteredPosts.Count(), activePostCount);
        }
    }
}
