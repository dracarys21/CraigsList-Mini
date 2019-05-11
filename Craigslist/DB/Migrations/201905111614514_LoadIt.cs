namespace DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoadIt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostFilterViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        CreateDate = c.String(),
                        Location_Id = c.Int(),
                        PostType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .ForeignKey("dbo.PostTypes", t => t.PostType_Id)
                .Index(t => t.Location_Id)
                .Index(t => t.PostType_Id);
            
            AddColumn("dbo.AspNetUsers", "UserRole", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostFilterViewModels", "PostType_Id", "dbo.PostTypes");
            DropForeignKey("dbo.PostFilterViewModels", "Location_Id", "dbo.Locations");
            DropIndex("dbo.PostFilterViewModels", new[] { "PostType_Id" });
            DropIndex("dbo.PostFilterViewModels", new[] { "Location_Id" });
            DropColumn("dbo.AspNetUsers", "UserRole");
            DropTable("dbo.PostFilterViewModels");
        }
    }
}
