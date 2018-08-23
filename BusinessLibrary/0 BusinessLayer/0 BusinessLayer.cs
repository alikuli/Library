using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
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
using UowLibrary.MyWorkClassesNS;
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
        protected ConfigManagerHelper _configManager;

        //protected ApplicationDbContext _db;
        protected UploadedFileBiz _uploadedFileBiz;
        protected BreadCrumbManager _breadCrumbManager;
        //protected RightBiz _rightBiz;
        //protected UserBiz _userBiz;

        public BusinessLayer(MyWorkClasses myWorkClasses, IRepositry<TEntity> dal, UploadedFileBiz uploadedFileBiz, BreadCrumbManager breadCrumbManager)
            : base(myWorkClasses)
        {
            _dal = dal;
            _uploadedFileBiz = uploadedFileBiz;
            _breadCrumbManager = breadCrumbManager;

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
