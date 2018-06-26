using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary.MenuNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using System.Linq;

namespace UowLibrary.ProductNS
{
    public partial class MenuPathMainBiz : BusinessLayer<MenuPathMain>
    {
        MenuPath1Biz _menupath1Biz;
        MenuPath2Biz _menupath2Biz;
        MenuPath3Biz _menupath3Biz;
        public MenuPathMainBiz(IRepositry<ApplicationUser> userDal, MenuPath1Biz menupath1Biz, MenuPath2Biz menupath2Biz, MenuPath3Biz menupath3Biz, IRepositry<MenuPathMain> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
            _menupath1Biz = menupath1Biz;
            _menupath2Biz = menupath2Biz;
            _menupath3Biz = menupath3Biz;
        }

        public MenuPathMain FindByMenuPath1Id(string menuPath1Id)
        {
            MenuPathMain mp1 = FindAll().FirstOrDefault(x => x.MenuPath1Id == menuPath1Id);
            return mp1;
        }

    }
}
