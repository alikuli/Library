namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _111020181 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AddressWithIds", new[] { "Country_Id" });
            DropColumn("dbo.AddressWithIds", "CountryId");
            RenameColumn(table: "dbo.AddressWithIds", name: "Country_Id", newName: "CountryId");
            AlterColumn("dbo.AddressWithIds", "CountryId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AddressWithIds", "CountryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AddressWithIds", new[] { "CountryId" });
            AlterColumn("dbo.AddressWithIds", "CountryId", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.AddressWithIds", name: "CountryId", newName: "Country_Id");
            AddColumn("dbo.AddressWithIds", "CountryId", c => c.Guid(nullable: false));
            CreateIndex("dbo.AddressWithIds", "Country_Id");
        }
    }
}
