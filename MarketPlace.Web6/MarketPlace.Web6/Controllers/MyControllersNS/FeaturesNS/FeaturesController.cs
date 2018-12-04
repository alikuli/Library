using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using System;
using System.Reflection;
using UowLibrary.FileDocNS;
using AliKuli.Extentions;
using UowLibrary;
using ModelsClassLibrary.ModelsNS.SharedNS;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using ErrorHandlerLibrary;
using ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS;
using UowLibrary.FeaturesNS;
using UowLibrary.PageViewNS;

namespace MarketPlace.Web6.Controllers
{
    public partial class FeaturesController : EntityAbstractController<Feature>
    {

        //private FeatureBiz _biz;
        //private UserBiz _userBiz;
        public FeaturesController(FeatureBiz biz, AbstractControllerParameters param)
            : base(biz, param) 
        {
            _biz = biz;
            //_userBiz = userBiz;
        }

        public FeatureBiz FeatureBiz
        {
            get 
            {
                _biz.IsNullThrowException();
                return _biz;
            }
        }

        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        _userBiz.IsNullThrowException();
        //        return _userBiz;
        //    }
        //}






    }
}