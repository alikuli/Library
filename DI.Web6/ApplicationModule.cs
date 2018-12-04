using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using Ninject.Modules;
using Ninject.Web.Common;
using UowLibrary;
using UowLibrary.AddressNS;
using UowLibrary.CounterNS;
using UowLibrary.EmailAddressNS;
//using UowLibrary.FeaturesNS;
using UowLibrary.FileDocNS;
using UowLibrary.GlobalCommentsNS;
using UowLibrary.Interface;
using UowLibrary.LikeUnlikeNS;
using UowLibrary.MailerNS;
using UowLibrary.MenuNS;
using UowLibrary.PageViewNS;
using UowLibrary.ParametersNS;
//using UowLibrary.PhoneNS;
using UowLibrary.PlayersNS.CashierCategoryNS;
using UowLibrary.PlayersNS.CashierNS;
using UowLibrary.PlayersNS.CustomerCategoryNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.DeliverymanCategoryNS;
using UowLibrary.PlayersNS.DeliverymanNS;
using UowLibrary.PlayersNS.MailerCategoryNS;
using UowLibrary.PlayersNS.OwnerCategoryNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonCategoryNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.SalesmanCategoryNS;
using UowLibrary.PlayersNS.SalesmanNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;
using UowLibrary.StateNS;
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
            LoadDALs();
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

            Bind<SalesmanBiz>().ToSelf();
            Bind<SalesmanCategoryBiz>().ToSelf();

            Bind<PersonCategoryBiz>().ToSelf();
            Bind<PersonBiz>().ToSelf();


            Bind<DeliverymanBiz>().ToSelf();
            Bind<DeliverymanCategoryBiz>().ToSelf();

            Bind<EmailAddressBiz>().ToSelf();
            Bind<UowLibrary.PhoneNS.PhoneBiz>().ToSelf();


            //Bind<UserBiz>().ToSelf();
            //Bind<FeatureBiz>().ToSelf();
            //Bind<MenuPath1FeatureBiz>().ToSelf();
            //Bind<MenuPath2FeatureBiz>().ToSelf();
            //Bind<RightBiz>().ToSelf();
            //Bind<MenuPath3FeatureBiz>().ToSelf();
        }

        public void LoadDALs()
        {
            //Bind<IUserDAL>().To<UserDAL>().InRequestScope(); 
            //Bind<ICountryDAL>().To<CountryDAL>().InRequestScope();


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
