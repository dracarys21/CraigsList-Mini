using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Data.Models;

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
                    var roleId = rolesId.FirstOrDefault().FirstOrDefault();
                    if (roleId == null)
                        return "";
                    var role = db.Roles.Find(roleId.RoleId);
                    return role.Name;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

       public static ICollection<AdminUserDisplayViewModel> GetUsers()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    string admin_roleid = GetAdminRoleId();
                    var users = from u in db.Users
                                select u;
                    List < AdminUserDisplayViewModel > icollection = new List<AdminUserDisplayViewModel>();
                    AdminUserDisplayViewModel t;
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                    foreach (var us in users)
                    {
                        t = new AdminUserDisplayViewModel();
                        t.Email = us.Email;
                        t.UserName = us.UserName;
                        if (userManager.GetRoles(us.Id).Contains("Admin"))
                            t.IsAdmin = true;
                        t.UserId = us.Id;
                        icollection.Add(t);
                    }
                    return icollection;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static bool ChangeUserRole(String userName)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                    ApplicationUser user = db.Users.FirstOrDefault(x => x.UserName == userName);
                    if (user != null)
                    {
                        userManager.AddToRole(user.Id, "Admin");
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public static bool CreateAdminRole()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                    if (!roleManager.RoleExists("Admin"))
                    {
                        roleManager.Create(new IdentityRole("Admin"));
                        db.SaveChanges();
                        return false;
                    }
                    return true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string GetAdminRoleId()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var AdminRoleId = from roles in db.Roles
                                      where roles.Name == "Admin"
                                      select roles.Id;
                    return AdminRoleId.FirstOrDefault();
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static ApplicationUser GetUserById(string id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var users = from u in db.Users
                                where u.Id.CompareTo(id) == 0
                                select u;
                    return users.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static ApplicationUser GetUserByUserName(string username)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var users = from u in db.Users
                                where u.UserName.CompareTo(username) == 0
                                select u;
                    return users.FirstOrDefault();
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
