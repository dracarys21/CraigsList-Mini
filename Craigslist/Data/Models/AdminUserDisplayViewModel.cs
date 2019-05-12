using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AdminUserDisplayViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }

        public string UserId { get; set; }
    }
}
