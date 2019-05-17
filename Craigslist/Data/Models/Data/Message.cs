using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Data
{
    public class Message { 
        public Message()
        {
            CreateDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public ApplicationUser SendTo { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        [Display(Name ="Sender")]
        public ApplicationUser CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public bool Read { get; set; }
        [Display(Name ="PostId")]
        public int postId { get; set; }
    }
}
