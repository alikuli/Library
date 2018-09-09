namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _060920182 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PageViews", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.PageViews", "UserName", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PageViews", "UserName", c => c.String());
            AlterColumn("dbo.PageViews", "UserId", c => c.String());
        }
    }
}
