using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;
using ModelsClassLibrary.ModelsNS.Logs.VisitorsLogNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS.CounterNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.RightsNS;
using System.Data.Entity;
using UserModels;
using ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;

namespace ApplicationDbContextNS
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        #region IDbSet
        //public virtual IDbSet<AddressWithId> Addresses { get; set; }
        //public IDbSet<AddressCategory> AddressCategories { get; set; }
        //public virtual IDbSet<City> Cities { get; set; }
        public virtual IDbSet<GlobalComment> GlobalComments { get; set; }
        public virtual IDbSet<Country> Countries { get; set; }
        public virtual IDbSet<Counter> Counters { get; set; }
        //public IDbSet<Customer> Customers { get; set; }
        public virtual IDbSet<CustomerCategory> CustomerCategories { get; set; }
        //public IDbSet<DeliveryMethod> DeliveryMethods { get; set; }
        //public IDbSet<Discount> Discounts { get; set; }
        public virtual IDbSet<DiscountPrecedence> DiscountPrecedences { get; set; }
        ////public IDbSet<EmailAddress> EmailAddresses { get; set; }
        public virtual IDbSet<Feature> Features { get; set; }
        
        public virtual IDbSet<FileDoc> FileDocs { get; set; }
        public virtual IDbSet<OldFileData> OldFileDatas { get; set; }
        //public IDbSet<FileCategory> FileCategories { get; set; }

        //public IDbSet<GeoLocation> GeoLocations { get; set; }
        //public IDbSet<GeoLocationNS.GeoLocHomeWorker> GeoLocHomeWorkers { get; set; }
        //public IDbSet<GeoLocation.GeoLocation> GeoLocForWorks { get; set; }
        //public IDbSet<GlAccount> GlAccounts { get; set; }
        //public IDbSet<GlTrx> GlTrxs { get; set; }
        //public IDbSet<Invoice> Invoices { get; set; }
        public virtual IDbSet<Language> Languages { get; set; }
        //public IDbSet<Owner> Owners { get; set; }
        //public virtual IDbSet<UploadedFile> Medias { get; set; }
        //public IDbSet<MediaNS.Movie> Movies { get; set; }
        public virtual IDbSet<PaymentMethod> PaymentMethods { get; set; }
        //public IDbSet<Payment> Payments { get; set; }
        public IDbSet<PaymentType> PaymentTypes { get; set; }
        //public IDbSet<PaymentAppliedInvoice> PaymentAppliedInvoices { get; set; }
        //public IDbSet<PaymentAppliedSalesOrder> PaymentAppliedSalesOrders { get; set; }

        public virtual IDbSet<PaymentTerm> PaymentTerms { get; set; }
        public virtual IDbSet<PageView> PageViews { get; set; }
        


        public IDbSet<MenuPath1> MenuPath1s { get; set; }


        public IDbSet<MenuPath2> MenuPath2s { get; set; }

        public IDbSet<MenuPath3> MenuPath3s { get; set; }

        public IDbSet<MenuPathMain> ProductCategoryMains { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<ProductChild> ProductChildren { get; set; }

        //public IDbSet<ProductIdentifierType> ProductIdentifierTypes { get; set; }

        public IDbSet<ProductIdentifier> ProductIdentifiers { get; set; }
        public IDbSet<Right> Rights { get; set; }
        public IDbSet<OwnerCategory> OwnerCategories { get; set; }

        public IDbSet<State> States { get; set; }

        //public IDbSet<Salesman> Salesmen { get; set; }
        //public IDbSet<SalesmanCategory> SalesmanCategories { get; set; }
        ////public IDbSet<Documents.Sale.SalesOrder> SalesOrders { get; set; }
        //public IDbSet<ScratchCard> ScratchCards { get; set; }

        //public IDbSet<ScratchCardTrx> ScratchCardTrxs { get; set; }

        //public IDbSet<SalesOrder> SalesOrders { get; set; }
        //public IDbSet<SalesOrderTrx> SalesorderTrxes { get; set; }
        //public IDbSet<Salepoint> Salepoints { get; set; }
        //public virtual IDbSet<Setup> SetUps { get; set; }
        //public IDbSet<SetupNew> SetupNews { get; set; }
        public virtual IDbSet<UploadedFile> UploadedFiles { get; set; }
        public virtual IDbSet<VisitorLog> VisitorLogs { get; set; }
        public virtual IDbSet<UomLength> UomLengths { get; set; }
        public virtual IDbSet<UomQty> UomQtys { get; set; }
        public virtual IDbSet<UomVolume> UomVolumes { get; set; }
        public virtual IDbSet<UomWeight> UomWeights { get; set; }

        //public IDbSet<Warehouse> Warehouses { get; set; }
        //public IDbSet<VendorCategory> VendorCategories { get; set; }
        //public IDbSet<Vendor> Vendors { get; set; }
        //public IDbSet<Worker> Workers { get; set; }
        //public IDbSet<ApplicationUser> Users { get; set; }
        public IDbSet<IdentityUserRole> UserRoles { get; set; }
        #endregion
    }
}