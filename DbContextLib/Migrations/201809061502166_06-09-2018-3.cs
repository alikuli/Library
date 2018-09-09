namespace DbContextLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _060920183 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PageViews", "HttpMethod", c => c.String(maxLength: 128));
            AddColumn("dbo.PageViews", "UserHostName", c => c.String(maxLength: 128));
            AddColumn("dbo.PageViews", "UserHostAddress", c => c.String(maxLength: 128));
            AddColumn("dbo.PageViews", "urlRefererrerHost", c => c.String(maxLength: 128));
            AddColumn("dbo.PageViews", "UserInfo", c => c.String(maxLength: 128));
            AddColumn("dbo.PageViews", "UserAgent", c => c.String(maxLength: 128));
            AddColumn("dbo.PageViews", "UserLanguages", c => c.String(maxLength: 128));
            AddColumn("dbo.PageViews", "BrowserType", c => c.String(maxLength: 128));
            AddColumn("dbo.PageViews", "IsCrawler", c => c.Boolean(nullable: false));
            AddColumn("dbo.PageViews", "IsMobileDevice", c => c.Boolean(nullable: false));
            AddColumn("dbo.PageViews", "IsClientWin16Based", c => c.Boolean(nullable: false));
            AddColumn("dbo.PageViews", "IsClientWin32Based", c => c.Boolean(nullable: false));
            AddColumn("dbo.PageViews", "IsAjaxRequest", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Counters", "Name", c => c.String());
            AlterColumn("dbo.UploadedFiles", "Name", c => c.String());
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.States", "Name", c => c.String());
            AlterColumn("dbo.FileDocs", "Name", c => c.String());
            AlterColumn("dbo.GlobalComments", "Name", c => c.String());
            AlterColumn("dbo.MenuPath1", "Name", c => c.String());
            AlterColumn("dbo.Features", "Name", c => c.String());
            AlterColumn("dbo.MenuPath2", "Name", c => c.String());
            AlterColumn("dbo.LikeUnlikes", "Name", c => c.String());
            AlterColumn("dbo.MenuPath3", "Name", c => c.String());
            AlterColumn("dbo.MenuPathMains", "Name", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.ProductChilds", "Name", c => c.String());
            AlterColumn("dbo.ProductIdentifiers", "Name", c => c.String());
            AlterColumn("dbo.UomLengths", "Name", c => c.String());
            AlterColumn("dbo.UomQties", "Name", c => c.String());
            AlterColumn("dbo.UomVolumes", "Name", c => c.String());
            AlterColumn("dbo.UomWeights", "Name", c => c.String());
            AlterColumn("dbo.Rights", "Name", c => c.String());
            AlterColumn("dbo.CustomerCategories", "Name", c => c.String());
            AlterColumn("dbo.DiscountPrecedences", "Name", c => c.String());
            AlterColumn("dbo.Languages", "Name", c => c.String());
            AlterColumn("dbo.OwnerCategories", "Name", c => c.String());
            AlterColumn("dbo.PageViews", "ActionName", c => c.String(maxLength: 128));
            AlterColumn("dbo.PageViews", "ControllerName", c => c.String(maxLength: 128));
            AlterColumn("dbo.PageViews", "Name", c => c.String());
            AlterColumn("dbo.PaymentMethods", "Name", c => c.String());
            AlterColumn("dbo.PaymentTerms", "Name", c => c.String());
            AlterColumn("dbo.PaymentTypes", "Name", c => c.String());
            AlterColumn("dbo.VisitorLogs", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VisitorLogs", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.PaymentTypes", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.PaymentTerms", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.PaymentMethods", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.PageViews", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.PageViews", "ControllerName", c => c.String());
            AlterColumn("dbo.PageViews", "ActionName", c => c.String());
            AlterColumn("dbo.OwnerCategories", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Languages", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.DiscountPrecedences", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.CustomerCategories", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Rights", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.UomWeights", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.UomVolumes", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.UomQties", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.UomLengths", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.ProductIdentifiers", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.ProductChilds", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Products", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.MenuPathMains", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.MenuPath3", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.LikeUnlikes", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.MenuPath2", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Features", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.MenuPath1", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.GlobalComments", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.FileDocs", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.States", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Countries", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.UploadedFiles", "Name", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Counters", "Name", c => c.String(maxLength: 2000));
            DropColumn("dbo.PageViews", "IsAjaxRequest");
            DropColumn("dbo.PageViews", "IsClientWin32Based");
            DropColumn("dbo.PageViews", "IsClientWin16Based");
            DropColumn("dbo.PageViews", "IsMobileDevice");
            DropColumn("dbo.PageViews", "IsCrawler");
            DropColumn("dbo.PageViews", "BrowserType");
            DropColumn("dbo.PageViews", "UserLanguages");
            DropColumn("dbo.PageViews", "UserAgent");
            DropColumn("dbo.PageViews", "UserInfo");
            DropColumn("dbo.PageViews", "urlRefererrerHost");
            DropColumn("dbo.PageViews", "UserHostAddress");
            DropColumn("dbo.PageViews", "UserHostName");
            DropColumn("dbo.PageViews", "HttpMethod");
        }
    }
}
