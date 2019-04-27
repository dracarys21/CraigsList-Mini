using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Data
{
    public class Inbox
    {
        public int Id { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
