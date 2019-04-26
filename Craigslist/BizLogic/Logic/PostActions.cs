using Data.Models.Data;
using Models;
using System;

namespace BizLogic.Logic
{
    public static class PostActions
    {
        public static bool CanDeletePost(ApplicationUser user, Post post)
        {
            return true;
        }
    }
}
