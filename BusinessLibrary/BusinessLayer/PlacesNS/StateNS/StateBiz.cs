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
        //readonly CountryBiz _countryBiz;
        public StateBiz(IRepositry<State> dal, BizParameters bizParameters)
            : base(dal, bizParameters)
        {
            //_countryBiz = countryBiz;


        }

        //protected CountryBiz CountryBiz
        //{
        //    get
        //    {
        //        if (_countryBiz.IsNull())
        //        {
        //            ErrorsGlobal.Add("Country DAL not loaded.", MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());
        //        }
        //        _countryBiz.UserId = UserId;
        //        _countryBiz.UserName = UserName;

        //        return _countryBiz;
        //    }
        //}




    }
}
