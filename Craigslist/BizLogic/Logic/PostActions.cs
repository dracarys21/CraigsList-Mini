using Data.Models.Data;
using Models;
using System;

namespace BizLogic.Logic
{
    public static class PostActions
    {
        public static bool IsValidPost(Post post)
        {
            var meetsMinimumRequirements = !string.IsNullOrEmpty(post.Title)
                                           && !string.IsNullOrEmpty(post.Body);

            return meetsMinimumRequirements;
        }

        public static Post CreatePost(Post post, ApplicationUser createdBy)
        {
            if (!IsValidPost(post))
                return null;

            // Set default values for a new post
            post.Deleted = false;
            post.CreateDate = DateTime.Now;
            post.CreatedBy = createdBy;
            post.LastModifiedDate = DateTime.Now;
            post.LastModifiedBy = createdBy;

            return post;
        }
    }
}
