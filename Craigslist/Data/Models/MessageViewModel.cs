using Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class MessageViewModel
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string MessageBody { get; set; }
        public  int postId { get; set; }
    }
}
