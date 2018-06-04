namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _020520182 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductChilds", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductChilds", "SerialNumber");
        }
    }
}
