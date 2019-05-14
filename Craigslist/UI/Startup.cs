using DB.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;



[assembly: OwinStartupAttribute(typeof(UI.Startup))]
namespace UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();

        }

        // In this method we will create default User roles and Admin user for login   
        private void CreateRolesAndUsers()   
        {   
            ApplicationDbContext context = new ApplicationDbContext();   
   
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
   
            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))   
            {

                // first we create Admin rool   
                var role = new IdentityRole
                {
                    Name = "Admin"
                };

                roleManager.Create(role);   
            }   
   
            // creating Creating Manager role    
            if (!roleManager.RoleExists("User"))   
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };

                roleManager.Create(role);   
            }
        } 
    }
}
