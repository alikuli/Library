using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.StateNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary
{
    public partial class CountryBiz : BusinessLayer<Country>
    {
        readonly StateBiz _stateBiz;

        public CountryBiz(StateBiz stateBiz, IRepositry<Country> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)

        {
            _stateBiz = stateBiz;

        }

        protected StateBiz StateBiz { get { return _stateBiz; } }


        

    }
}
