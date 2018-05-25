namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21052018a : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UploadedFiles", "RelativeWebsitePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UploadedFiles", "RelativeWebsitePath", c => c.String());
        }
    }
}
