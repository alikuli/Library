using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using InvoiceNS;
using MigraDocLibrary.IndexNS;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;
using UowLibrary.Abstract;
using UowLibrary.Interface;
using UowLibrary.LikeUnlikeNS;
using UowLibrary.PageViewNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
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
        private IRepositry<TEntity> _dal;
        //BizParameters _param;

        //private ConfigManagerHelper _configManager;
        //protected ApplicationDbContext _db;
        //private UploadedFileBiz _uploadedFileBiz;
        //private BreadCrumbManager _breadCrumbManager;
        //protected RightBiz _rightBiz;
        //protected UserBiz _userBiz;

        public BusinessLayer( IRepositry<TEntity> dal, BizParameters param)
            : base(param)
        {
            _dal = dal;
            //_param = param;

            //_uploadedFileBiz = param.UploadedFileBiz;

        }

        public BusinessLayer(IRepositry<TEntity> dal, UploadedFileBiz uploadedFileBiz, IMemoryMain memoryMain, PageViewBiz pageViewBiz, IErrorSet errorSet, ConfigManagerHelper configManagerHelper, BreadCrumbManager breadCrumbManager)
            : base(uploadedFileBiz, memoryMain, pageViewBiz, errorSet, configManagerHelper, breadCrumbManager)
        {
            _dal = dal;
            //_param = param;

            //_uploadedFileBiz = param.UploadedFileBiz;

        }


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
                _dal.UserName = UserName?? "";
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

        //public UploadedFileBiz UploadedFileBiz
        //{
        //    get
        //    {
        //        return _uploadedFileBiz;
        //    }
        //}


















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
