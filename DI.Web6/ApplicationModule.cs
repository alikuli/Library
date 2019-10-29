using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using Ninject.Modules;
using Ninject.Web.Common;
using UowLibrary;
using UowLibrary.AddressNS;
using UowLibrary.BusinessLayer.ProductNS.ShopNS;
//using UowLibrary.BusinessLayer.NonReturnableNS;
using UowLibrary.BusinessLayer.ServiceRequestNS.ServiceRequestHdrNS;
//using UowLibrary.BusinessLayer.ServiceRequestNS.ServiceRequestTrxNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.CashEncashmentTrxNS;
using UowLibrary.CashTtxNS;
using UowLibrary.CounterNS;
using UowLibrary.EmailAddressNS;
using UowLibrary.FeatureNS.MenuFeatureNS;
//using UowLibrary.FeaturesNS;
//using UowLibrary.FeaturesNS;
using UowLibrary.FileDocNS;
using UowLibrary.FreightOffersTrxNS;
using UowLibrary.GlobalCommentsNS;
using UowLibrary.Interface;
using UowLibrary.LikeUnlikeNS;
using UowLibrary.MailerNS;
using UowLibrary.MenuNS;
//using UowLibrary.NonReturnableNS;
using UowLibrary.PageViewNS;
using UowLibrary.ParametersNS;
using UowLibrary.PaymentTypeNS;
using UowLibrary.PhoneNS;
using UowLibrary.PlayersNS.BankCategoryNS;
using UowLibrary.PlayersNS.BankNS;
//using UowLibrary.PhoneNS;
using UowLibrary.PlayersNS.CashierCategoryNS;
using UowLibrary.PlayersNS.CashierNS;
using UowLibrary.PlayersNS.CustomerCategoryNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.DeliverymanCategoryNS;
using UowLibrary.PlayersNS.DeliverymanNS;
using UowLibrary.PlayersNS.MailerCategoryNS;
using UowLibrary.PlayersNS.MessageNS;
using UowLibrary.PlayersNS.MessageToPeopleListNS;
using UowLibrary.PlayersNS.OwnerCategoryNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonCategoryNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.ProductApproverCategoryNS;
using UowLibrary.PlayersNS.ProductApproverNS;
using UowLibrary.PlayersNS.SalesmanCategoryNS;
using UowLibrary.PlayersNS.SalesmanNS;
using UowLibrary.PlayersNS.ServiceRequestHdrNS;
//using UowLibrary.PlayersNS.ServiceRequestTrxNS;
using UowLibrary.PlayersNS.VehicalTypeNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;
using UowLibrary.StateNS;
using UowLibrary.SuperLayerNS.AccountsNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
namespace DependancyResolver
{
    public class ApplicationModule : NinjectModule
    {

        //public override void OnLoad()
        //{
        //    Bind<ApplicationDbContext>().ToSelf();
        //    Bind<IUserStoreGuid<User>>().To<UserStoreGuid<User>>().WithConstructorArgument("context", this.Kernel.Get<ApplicationDbContext>());
        //    Bind<UserManagerGuid<User>>().ToSelf();
        //}
        public override void Load()
        {


            LoadUserStuff();
            LoadMisc();
            LoadUow();
            //LoadDALs();
            LoadBusinessLayer();

        }

        private void LoadBusinessLayer()
        {
            Bind(typeof(IBusinessLayer<>)).To(typeof(BusinessLayer<>));

            Bind<CounterBiz>().ToSelf();

            Bind<FileDocBiz>().ToSelf();

            Bind<MenuBiz>().ToSelf();

            Bind<ProductBiz>().ToSelf();
            Bind<ProductChildBiz>().ToSelf();
            Bind<ProductIdentifierBiz>().ToSelf();

            Bind<StateBiz>().ToSelf();
            Bind<UserBiz>().ToSelf();

            Bind<UomLengthBiz>().ToSelf();
            Bind<UomQuantityBiz>().ToSelf();
            Bind<UomVolumeBiz>().ToSelf();
            Bind<UomWeightBiz>().ToSelf();

            Bind<UploadedFileBiz>().ToSelf();

            Bind<GlobalCommentBiz>().ToSelf();
            Bind<LikeUnlikeBiz>().ToSelf();
            Bind<AbstractControllerParameters>().ToSelf();
            Bind<BizParameters>().ToSelf();
            Bind<PageViewBiz>().ToSelf();

            Bind<MenuFeature>().ToSelf();
            Bind<ProductFeature>().ToSelf();
            Bind<ProductChildFeature>().ToSelf();

            Bind<AddressBiz>().ToSelf()
                ;
            Bind<MailerBiz>().ToSelf();
            Bind<MailerCategoryBiz>().ToSelf();

            Bind<CustomerCategoryBiz>().ToSelf();
            Bind<CustomerBiz>().ToSelf();

            Bind<OwnerCategoryBiz>().ToSelf();
            Bind<OwnerBiz>().ToSelf();

            Bind<CashierCategoryBiz>().ToSelf();
            Bind<CashierBiz>().ToSelf();

            Bind<BankCategoryBiz>().ToSelf();
            Bind<BankBiz>().ToSelf();

            Bind<SalesmanBiz>().ToSelf();
            Bind<SalesmanCategoryBiz>().ToSelf();

            Bind<PersonCategoryBiz>().ToSelf();
            Bind<PersonBiz>().ToSelf();


            Bind<DeliverymanBiz>().ToSelf();
            Bind<DeliverymanCategoryBiz>().ToSelf();

            Bind<EmailAddressBiz>().ToSelf();
            Bind<PhoneBiz>().ToSelf();

            Bind<CashTrxBiz>().ToSelf();
            Bind<BuySellDoc>().ToSelf();
            Bind<AccountsBizSuper>().ToSelf();
            //Bind<MenuPath1FeatureBiz>().ToSelf();
            Bind<MenuFeatureBiz>().ToSelf();

            Bind<ProductApproverBiz>().ToSelf();
            Bind<ProductApproverCategoryBiz>().ToSelf();
            Bind<MessageBiz>().ToSelf();
            Bind<MessageToPeopleListBiz>().ToSelf();
            Bind<BuySellItemBiz>().ToSelf();
            Bind<FreightOfferTrxBiz>().ToSelf();
            Bind<VehicalTypeBiz>().ToSelf();
            Bind<CountryBizTest>().ToSelf();
            Bind<CashEncashmentTrxBiz>().ToSelf();
            Bind<PaymentTypeBiz>().ToSelf();
            Bind<BuySellDocHistoryBiz>().ToSelf();
            Bind<PenaltyHeaderBiz>().ToSelf();
            Bind<PenaltyTrxBiz>().ToSelf();
            Bind<ShopBiz>().ToSelf();
            Bind<IServiceRequestHdrBiz>().To<ServiceRequestHdrBiz>();
            //https://stackoverflow.com/questions/4370515/ninject-bind-generic-repository
            Bind(typeof(IRepositry<>)).To(typeof(Repositry<>));
            //Bind<UserDAL>().ToSelf();
        }

        public void LoadMisc()
        {
            Bind<IMemoryMain>().To<MemoryMain>().InRequestScope();
            Bind<IErrorSet>().To<ErrorSet>().InRequestScope();
            Bind<ConfigManagerHelper>().ToSelf().InRequestScope();
            Bind<BreadCrumbManager>().ToSelf().InRequestScope();
            //Bind<ISetUpValues>().To<SetUpValues>().InRequestScope();
        }
        public void LoadUow()
        {
            //Bind<Home_UOW>().ToSelf().InRequestScope(); ;
            //Bind<AccountUow>().ToSelf().InRequestScope();
            //Bind<ManageUow>().ToSelf().InRequestScope();
            //Bind<UsersUOW>().ToSelf().InRequestScope();
            //Bind<InitializeUOW>().ToSelf();
        }

        public void LoadUserStuff()
        {
            //Bind<DbContext>().To<ApplicationDbContext>().InRequestScope();
            Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>().WithConstructorArgument("store", Kernel.GetService(typeof(ApplicationDbContext)));



        }
    }
}
