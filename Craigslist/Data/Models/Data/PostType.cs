using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models.Data
{
    public class PostType : IEquatable<PostType>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string SubCategory { get; set; }

        [Required]
        public string Slug { get; set; }

        public bool Active { get; set; } = false;

        public bool Equals(PostType other)
        {
            return !(other is null) && Id.Equals(other.Id);
        }
    }
}
