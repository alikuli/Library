namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13618 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuPath1", "MenuPath1Enum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuPath1", "MenuPath1Enum");
        }
    }
}
