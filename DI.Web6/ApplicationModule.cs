using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using DalNS;
using ErrorHandlerLibrary.ExceptionsNS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using Ninject.Web.Common;
using UowLibrary;
using UowLibrary.CounterNS;
using UowLibrary.FileDocNS;
using UowLibrary.Interface;
using UowLibrary.MenuNS;
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

            Bind<UomLengthBiz>().ToSelf();
            Bind<UomQuantityBiz>().ToSelf();
            Bind<UomVolumeBiz>().ToSelf();
            Bind<UomWeightBiz>().ToSelf();

            Bind<UploadedFileBiz>().ToSelf();

            Bind<UserBiz>().ToSelf();
        }

        public void LoadDALs()
        {
            //Bind<IUserDAL>().To<UserDAL>().InRequestScope(); 
            //Bind<ICountryDAL>().To<CountryDAL>().InRequestScope();


            //https://stackoverflow.com/questions/4370515/ninject-bind-generic-repository
            Bind(typeof(IRepositry<>)).To(typeof(Repositry<>));
            Bind<UserDAL>().ToSelf();
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
