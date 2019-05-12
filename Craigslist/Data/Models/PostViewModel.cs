using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Data.Models.Data;

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

        public string SelectedArea { get; set; }

        public string SelectedLocale { get; set; }
        
        public string SelectedCategory { get; set; }

        public string SelectedSubCategory { get; set; }

        public SelectList Areas { get; set; }

        public SelectList Locales { get; set; }
        
        public SelectList Categories { get; set; }
        
        public SelectList SubCategories { get; set; }

    }
}
