namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _111020184 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AddressWithIds", "GeoPosition_Latitude", c => c.String(maxLength: 100));
            AlterColumn("dbo.AddressWithIds", "GeoPosition_Longitude", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AddressWithIds", "GeoPosition_Longitude", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.AddressWithIds", "GeoPosition_Latitude", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
