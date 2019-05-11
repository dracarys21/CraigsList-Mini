using System.Collections.Generic;
using System.Linq;
using BizLogic.Logic;
using Data.Models.Data;

namespace DB.Database
{
    public static class PostFilter
    {
        public static List<Post> FilterPost(string area = "", string locale = "",
            string category = "", string subCategory = "", string query = "")
        {
            using (var db = new ApplicationDbContext())
            {
                var posts = db.Posts.ToList();

                var filteredPosts = PostActions.FilterPost(db.Posts, area,
                    locale, category, subCategory, query);

                return filteredPosts.ToList();
            }
        }
    }
}
