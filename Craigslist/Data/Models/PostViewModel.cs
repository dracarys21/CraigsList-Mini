using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Data.Models.Data;

namespace Data.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(140)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Body { get; set; }

        public string CreateDate { get; set; }

        public string Area { get; set; }

        public string Locale { get; set; }

        public string Category { get; set; }

        public string Subcategory { get; set; }

        public string SelectedArea { get; set; }

        public string SelectedLocale { get; set; }
        
        public string SelectedCategory { get; set; }

        public string SelectedSubcategory { get; set; }

        public string ReturnUri { get; set; }

        public SelectList Areas { get; set; }

        public SelectList Locales { get; set; }
        
        public SelectList Categories { get; set; }
        
        public SelectList SubCategories { get; set; }

    }
}
