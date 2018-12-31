using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.StateNS
{
    public partial class StateBiz : BusinessLayer<State>
    {
        readonly IRepositry<Country> _iCountryDal;
        public StateBiz(IRepositry<Country> countryDal, IRepositry<State> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {
            _iCountryDal = countryDal;


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
