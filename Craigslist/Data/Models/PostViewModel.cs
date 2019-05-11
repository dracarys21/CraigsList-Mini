using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.Models.Data;
using Models;

namespace Data.Models
{
    public class PostViewModel
    {
        [Required]
        [MinLength(3), MaxLength(140)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Body { get; set; }

        private DateTime CreateDate { get; set; }

        private DateTime LastModifiedDate { get; set; }

        private DateTime? ExpirationDate { get; set; }

        private ApplicationUser Author { get; set; }

        private ApplicationUser LastModifiedBy { get; set; }

        public Location Location { get; set; }

        public PostType PostType { get; set; }

        [Required]
        public string SelectedArea { get; set; }

        public List<string> Areas { get; set; }


        [Required]
        public string SelectedLocale { get; set; }
        
        public List<string> Locales { get; set; }
        
        [Required]
        public string SelectedCategory { get; set; }
        
        public List<string> Categories { get; set; }
        
        [Required]
        public string SelectedSubCategory { get; set; }
        
        public List<string> SubCategories { get; set; }

    }
}
