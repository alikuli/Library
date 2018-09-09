namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _060920181 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PageViews", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.PageViews", new[] { "UserId" });
            AddColumn("dbo.PageViews", "UserName", c => c.String());
            AddColumn("dbo.PageViews", "ActionName", c => c.String());
            AlterColumn("dbo.PageViews", "UserId", c => c.String());
            DropColumn("dbo.PageViews", "PageName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PageViews", "PageName", c => c.String());
            AlterColumn("dbo.PageViews", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.PageViews", "ActionName");
            DropColumn("dbo.PageViews", "UserName");
            CreateIndex("dbo.PageViews", "UserId");
            AddForeignKey("dbo.PageViews", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
