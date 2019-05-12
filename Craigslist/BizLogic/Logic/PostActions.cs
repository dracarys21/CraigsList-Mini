using System;
using System.Collections.Generic;
using System.Linq;
using Data.Models.Data;
using Models;
namespace BizLogic.Logic
{
    public static class PostActions
    {
        public static bool CanCreatePost(ApplicationUser user, Post post)
        {
            var containsTitle = !string.IsNullOrEmpty(post.Title);
            var containsBody = !string.IsNullOrEmpty(post.Body);
            var isCurrentUserAuthor = post.Author.Equals(user);
            var hasLocation = !(post.Location is null) && !string.IsNullOrEmpty(post.Location.Area)
                && !string.IsNullOrEmpty(post.Location.Locale);
            var hasPostType = !(post.PostType is null) && !string.IsNullOrEmpty(post.PostType.Category)
                && !string.IsNullOrEmpty(post.PostType.SubCategory);

            return containsBody && containsTitle && isCurrentUserAuthor
                && hasLocation && hasPostType;
        }

        public static bool CanDeletePost(string userId, Post post)
        {
            var isPostOwner = post.Author.Id.Equals(userId);

            return isPostOwner;
        }

        public static bool CanUpdatePost(ApplicationUser user, Post post)
        {
            var isOwner = post.Author.Equals(user);
            var isPostExpiredOrDeleted = post.Deleted
                || (post.ExpirationDate.HasValue && post.ExpirationDate < DateTime.Now);

            return isOwner && !isPostExpiredOrDeleted;
        }

        public static IEnumerable<Post> FilterPost(IEnumerable<Post> posts,
            string area = "", string locale = "", string category = "",
            string subCategory = "", string query = "")
        {
            var filteredPosts = from post in posts
                where post.Deleted.Equals(false)
                      && post.ExpirationDate.HasValue 
                      && post.ExpirationDate.Value.CompareTo(DateTime.Now.Date) > 0
                select post;

            if (!string.IsNullOrEmpty(area) || !string.IsNullOrEmpty(locale))
                filteredPosts = from post in filteredPosts
                    where (string.IsNullOrEmpty(area) || post.Location.Area.ToLower().Equals(area.ToLower()))
                        && (string.IsNullOrEmpty(locale) || post.Location.Locale.ToLower().Equals(locale.ToLower()))
                    select post;

            if (!string.IsNullOrEmpty(category) || !string.IsNullOrEmpty(subCategory))
                filteredPosts = from post in filteredPosts
                    where (string.IsNullOrEmpty(category) || post.PostType.Category.ToLower().Equals(category.ToLower()))
                        && (string.IsNullOrEmpty(subCategory) || post.PostType.SubCategory.ToLower().Equals(subCategory.ToLower()))
                    select post;

            if (!string.IsNullOrEmpty(query))
                filteredPosts = from post in filteredPosts
                    where post.Title.ToLower().Contains(query.ToLower())
                          || post.Body.ToLower().Contains(query.ToLower())
                    select post;

            return filteredPosts;
        }
    }
}
