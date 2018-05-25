using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using DalNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;
using UowLibrary.StateNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary
{
    public partial class CountryBiz : BusinessLayer<Country>
    {
        readonly StateBiz _stateBiz;

        public CountryBiz(IRepositry<ApplicationUser> userDal, StateBiz stateBiz,IRepositry<Country> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
            _stateBiz = stateBiz;

        }

        StateBiz StateBiz { get { return _stateBiz; } }


        

    }
}
