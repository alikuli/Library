namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22052018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UploadedFiles", "RelativeWebsitePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UploadedFiles", "RelativeWebsitePath");
        }
    }
}
