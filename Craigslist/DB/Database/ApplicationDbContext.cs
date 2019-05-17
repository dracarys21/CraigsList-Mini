using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        public DbSet<PostType> PostTypes { get; set; }

        public DbSet<Message> Messages { get; set; }

        // public DbSet<Inbox> Inbox { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
