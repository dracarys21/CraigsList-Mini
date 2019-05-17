using System.Collections.Generic;
using System.Linq;
using BizLogic.Logic;
using Data.Models.Data;

namespace DB.Database
{
    public static class PostFilter
    {
        public static List<Post> FilterPost(string area = "",
            string locale = "", string category = "",
            string subcategory = "", string query = "")
        {
            using (var db = new ApplicationDbContext())
            {
                var posts = db.Posts
                    .Include("Location")
                    .Include("PostType");

                var filteredPosts = PostActions.FilterPost(posts, area,
                    locale, category, subcategory, query);

                return filteredPosts.ToList();
            }
        }
    }
}
