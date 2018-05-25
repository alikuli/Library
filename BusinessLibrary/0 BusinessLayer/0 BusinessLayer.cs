using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using InvoiceNS;
using MigraDocLibrary.IndexNS;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using UowLibrary.Abstract;
using UowLibrary.Interface;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {
        /// <summary>
        /// Remember to wire up _dal in the child CRUD class.
        /// 
        /// Example. In UowCountry it would be as follows
        ///         readonly ICountryDAL _icountryDAL;
        ///         public UowCountry(UserDAL userDAL, ICountryDAL icountryDAL, IMemoryMain memoryMain, IErrorSet errorSet)
        ///               : base(userDAL, memoryMain, errorSet)
        ///         {
        ///             _icountryDAL = icountryDAL;
        ///             _dal = CountryDAL;
        ///         
        ///         }
        ///         
        ///         protected CountryDAL CountryDAL
        ///          {
        ///             get
        ///             {
        ///                 if (_icountryDAL.IsNull())
        ///                 {
        ///                     ErrorsGlobal.Add("Country DAL not loaded.", MethodBase.GetCurrentMethod());
        ///                     throw new Exception(ErrorsGlobal.ToString());
        ///                 }
        ///                 return (CountryDAL)_icountryDAL;
        ///             }
        ///         }

        /// </summary>
        private IRepositry<TEntity> _dal;
        protected ApplicationDbContext _db;
        protected ConfigManagerHelper _configManager;
        protected UploadedFileBiz _uploadedFileBiz;
        public BusinessLayer(IRepositry<ApplicationUser> userDal, IMemoryMain memoryMain, IErrorSet errorSet, IRepositry<TEntity> dal, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(memoryMain, errorSet)
        {
            UserDal = userDal;
            UserDal.UserName = UserNameBiz;
            UserDal.UserId = UserIdBiz;

            _dal = dal;
            _dal.UserName = UserNameBiz;
            _dal.UserId = UserIdBiz;

            _db = db;
            _configManager = configManager;
            _uploadedFileBiz = uploadedFileBiz;

        }


        public IRepositry<ApplicationUser> UserDal { get; private set; }
        protected IRepositry<TEntity> Dal
        {
            get
            {
                if (_dal.IsNull())
                {
                    ErrorsGlobal.Add("No CountryDAL received.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }
                //This is where userName is passed to the Dal from Biz
                _dal.UserName = UserNameBiz;
                return _dal;
            }
        }

        //string logoAddress = @"..\Content\MyImages\Logo.jpg";

        public byte[] PrintInvoice()
        {
            PdfInvoiceData data = new PdfInvoiceData();

            InvoicPdfParameter parm = data.Load(System.Web.HttpContext.Current.Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION));
            //parm.Logo.Address = System.Web.HttpContext.Current.Server.MapPath(@"..\Content\MyImages\raddicco.jpg");

            var factory = new InvoiceFactory();
            byte[] pdf = factory.Build(parm);

            return pdf;


        }

        public byte[] PrintIndex(IndexListVM indexListVM)
        {
            //PdfInvoiceData data = new PdfInvoiceData();

            IndexPdfParameter parm = new IndexPdfParameter(indexListVM);
            //parm.Logo.Address = System.Web.HttpContext.Current.Server.MapPath(@"..\Content\MyImages\raddicco.jpg");

            var factory = new IndexFactory();
            byte[] pdf = factory.Build(parm);

            return pdf;


        }

        ////https://stackoverflow.com/questions/12553639/ef-codefirst-get-all-poco-types-for-dbcontext
        ////returns a list of all POCO class names
        //protected System.Collections.ObjectModel.ReadOnlyCollection<EntityType> ListOfAllClassEntyTypes()
        //{
        //    var lstOfClasses = Enum.GetNames(typeof(ClassesWithRightsENUM));

        //    var objectContext = ((IObjectContextAdapter)this).ObjectContext;

        //    var mdw = objectContext.MetadataWorkspace;

        //    var lstClassesNames = mdw.GetItems<EntityType>(DataSpace.OSpace);
        //    return lstClassesNames;
        //}

    }
}
