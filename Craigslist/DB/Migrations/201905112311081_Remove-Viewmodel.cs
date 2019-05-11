namespace DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveViewmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostFilterViewModels", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.PostFilterViewModels", "PostType_Id", "dbo.PostTypes");
            DropIndex("dbo.PostFilterViewModels", new[] { "Location_Id" });
            DropIndex("dbo.PostFilterViewModels", new[] { "PostType_Id" });
            DropTable("dbo.PostFilterViewModels");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.PostFilterViewModels", "PostType_Id");
            CreateIndex("dbo.PostFilterViewModels", "Location_Id");
            AddForeignKey("dbo.PostFilterViewModels", "PostType_Id", "dbo.PostTypes", "Id");
            AddForeignKey("dbo.PostFilterViewModels", "Location_Id", "dbo.Locations", "Id");
        }
    }
}
