using System;
using System.ComponentModel.DataAnnotations;
using Models;

namespace Data.Models.Data
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(140)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Body { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }

        [Required]
        public ApplicationUser LastModifiedBy { get; set; }

//        [Required]
//        public LocationModel Location { get; set; }
//
//        [Required]
//        public PostTypeModel PostType { get; set; }

        [Required]
        public bool Deleted { get; set; }

//        public List<Message> Messages { get; set; }
    }
}
