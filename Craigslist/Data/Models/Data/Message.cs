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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MessageString { get; set; }
        [Required]
        public virtual ApplicationUser SendTo { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ApplicationUser CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public bool Read { get; set; }
    }
}
