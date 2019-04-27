using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.Data.Entity;
using Data.Models.Data;

namespace DB.Database
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<PostType> PostType { get; set; }
    }
}
