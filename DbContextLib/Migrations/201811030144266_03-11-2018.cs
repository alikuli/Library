namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03112018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressVerificationHdrs", "CostOfmailing", c => c.Double(nullable: false));
            AddColumn("dbo.AddressVerificationHdrs", "BudgetedCost", c => c.Double(nullable: false));
            AddColumn("dbo.AddressVerificationHdrs", "TotalQtyLettersMailed", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AddressVerificationHdrs", "TotalQtyLettersMailed");
            DropColumn("dbo.AddressVerificationHdrs", "BudgetedCost");
            DropColumn("dbo.AddressVerificationHdrs", "CostOfmailing");
        }
    }
}
