namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15518 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AddressComplex_Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_Address2", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_HouseNo", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_Road", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_WebAddress", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_Zip", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_Phone", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_Attention", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_TownName", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_CityName", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_StateName", c => c.String(maxLength: 2000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_CountryName", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "AddressComplex_CountryName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "AddressComplex_StateName", c => c.String(maxLength: 1000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_CityName", c => c.String(maxLength: 1000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_TownName", c => c.String(maxLength: 1000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_Attention", c => c.String(maxLength: 1000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_Phone", c => c.String(maxLength: 100));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_Zip", c => c.String(maxLength: 1000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_WebAddress", c => c.String(maxLength: 1000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_Road", c => c.String(maxLength: 1000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_HouseNo", c => c.String(maxLength: 1000));
            AlterColumn("dbo.AspNetUsers", "AddressComplex_Address2", c => c.String(maxLength: 1000));
            DropColumn("dbo.AspNetUsers", "AddressComplex_Name");
        }
    }
}
