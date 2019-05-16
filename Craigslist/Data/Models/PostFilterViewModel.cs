using System.Collections.Generic;
using System.Web.Mvc;

namespace Data.Models
{
    public class PostFilterViewModel
    {
//        public int Id { get; set; }
//
//        public string Title { get; set; }
//
//        public string Body { get; set; }
//
//        public string CreateDate { get; set; }

        public string Area { get; set; }

        public string Locale { get; set; }

        public string Category { get; set; }

        public string Subcategory { get; set; }

        public string Query { get; set; }

        public string PageAction { get; set; }
        
        public List<PostViewModel> Posts { get; set; }

        public SelectList Areas { get; set; }

        public SelectList Locales { get; set; }
        
        public SelectList Categories { get; set; }
        
        public SelectList SubCategories { get; set; }
    }
}
