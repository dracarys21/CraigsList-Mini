using Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizLogic.Logic;

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
    }

    public class Db1
    {

        public static void DoDatabaseOperation()
        {
            // You can place your models into ApplicationDbContext
            // or create your own context
            using (var db = new ApplicationDbContext())
            {
                // Create and manipulate your model
                Example ex = new Example();
                ex.Name = "Hello";

                // Call into biz logic
                Biz1.IsExampleValid(ex);

                // Do something useful here

                db.SaveChanges();
            }
        }

    }
}
