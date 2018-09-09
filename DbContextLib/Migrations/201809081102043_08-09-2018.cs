namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08092018 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PageViews", "UserAgent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PageViews", "UserAgent", c => c.String(maxLength: 128));
        }
    }
}
