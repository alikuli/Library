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
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.StateNS
{
    public partial class StateBiz : BusinessLayer<State>
    {
        readonly IRepositry<Country> _iCountryDal;
        public StateBiz(IRepositry<ApplicationUser> userDal, IRepositry<Country> iCountryDal, IRepositry<State> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
            _iCountryDal = iCountryDal;


        }

        protected Repositry<Country> CountryDal
        {
            get
            {
                if (_iCountryDal.IsNull())
                {
                    ErrorsGlobal.Add("Country DAL not loaded.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }
                return (Repositry<Country>)_iCountryDal;
            }
        }




    }
}
