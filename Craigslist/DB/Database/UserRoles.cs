using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Database
{
    public class UserRoles
    {
        public static string GetUserRole(string userName)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var rolesId = from u in db.Users
                                where u.UserName == userName
                                select u.Roles;
                    var roleId = rolesId.FirstOrDefault();
                    var role = db.Roles.Find(roleId.FirstOrDefault().RoleId);
                    return role.Name;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static bool ChangeUserRole(String userName)
        {
            using (var db = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                ApplicationUser user = db.Users.FirstOrDefault(x => x.UserName == userName);
                if (user != null)
                {
                    userManager.AddToRole(user.Id, "Admin");
                    return true;
                }
                return false;
            }
        }
    }
}
