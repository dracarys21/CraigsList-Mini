using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
   public class ChangeUserToAdminViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]//Can be changed to User Name
        public string Email { get; set; }

        public ApplicationUser User { get; set; }
    }
}
