﻿using Data.Models.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLogic.Logic
{
    public class MessageAction
    {
        public static bool CanCreateMessage(Message Message, ApplicationUser createdBy, Post post)
        {
            var isMessageEmpty = string.IsNullOrEmpty(Message.Body);
            var createdByFlag = Message.CreatedBy.Equals(createdBy);
            var sendToFlag = Message.SendTo.Equals(post.Author);
            return isMessageEmpty && sendToFlag && createdByFlag;
        }

        public static bool CanDeleteMessage(Message message, ApplicationUser user,  Post post)
        {
            return message.SendTo.Equals(user) && message.SendTo.Equals(post.Author);
        }
    }
}
