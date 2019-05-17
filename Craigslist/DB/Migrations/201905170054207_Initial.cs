namespace DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Area = c.String(nullable: false),
                        Locale = c.String(nullable: false),
                        Slug = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Read = c.Boolean(nullable: false),
                        CreatedBy_Id = c.String(nullable: false, maxLength: 128),
                        SendTo_Id = c.String(nullable: false, maxLength: 128),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SendTo_Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.SendTo_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 140),
                        Body = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(),
                        Deleted = c.Boolean(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                        LastModifiedBy_Id = c.String(maxLength: 128),
                        Location_Id = c.Int(nullable: false),
                        PostType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LastModifiedBy_Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.PostTypes", t => t.PostType_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.LastModifiedBy_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.PostType_Id);
            
            CreateTable(
                "dbo.PostTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(nullable: false),
                        SubCategory = c.String(nullable: false),
                        Slug = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Posts", "PostType_Id", "dbo.PostTypes");
            DropForeignKey("dbo.Messages", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Posts", "LastModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "SendTo_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Posts", new[] { "PostType_Id" });
            DropIndex("dbo.Posts", new[] { "Location_Id" });
            DropIndex("dbo.Posts", new[] { "LastModifiedBy_Id" });
            DropIndex("dbo.Posts", new[] { "Author_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Messages", new[] { "Post_Id" });
            DropIndex("dbo.Messages", new[] { "SendTo_Id" });
            DropIndex("dbo.Messages", new[] { "CreatedBy_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PostTypes");
            DropTable("dbo.Posts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Messages");
            DropTable("dbo.Locations");
        }
    }
}
