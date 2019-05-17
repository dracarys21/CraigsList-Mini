namespace DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateinMessageModel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Messages", new[] { "Post_Id" });
            RenameColumn(table: "dbo.Messages", name: "Post_Id", newName: "postId");
            AlterColumn("dbo.Messages", "postId", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "postId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Messages", new[] { "postId" });
            AlterColumn("dbo.Messages", "postId", c => c.Int());
            RenameColumn(table: "dbo.Messages", name: "postId", newName: "Post_Id");
            CreateIndex("dbo.Messages", "Post_Id");
        }
    }
}
