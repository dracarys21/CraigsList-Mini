using System;
using Data.Models.Data;

namespace Data.Models
{
    public class PostFilterViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string CreateDate { get; set; }

        public Location Location { get; set; }

        public PostType PostType { get; set; }

        public PostFilterViewModel(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Body = post.Body;
            CreateDate = post.CreateDate.ToShortDateString();
            Location = post.Location;
            PostType = post.PostType;
        }
    }
}
