using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;

namespace DB.Database
{
    public static class UserManagement
    {
        public static bool IsFirstUser()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var users = db.Users;
                    return users.ToList().Count == 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
