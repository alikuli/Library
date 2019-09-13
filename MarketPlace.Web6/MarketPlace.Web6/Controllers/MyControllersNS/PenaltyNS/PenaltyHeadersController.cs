using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.SuperLayerNS;


namespace MarketPlace.Web6.Controllers
{
    public class PenaltyHeadersController : EntityAbstractController<PenaltyHeader>
    {

        PenaltyHeaderBiz _PenaltyHeadersBiz;
        SuperBiz _superBiz;


        public PenaltyHeadersController(AbstractControllerParameters param, SuperBiz superBiz)
            : base(superBiz.PenaltyHeaderBiz, param)
        {
            _PenaltyHeadersBiz = superBiz.PenaltyHeaderBiz;
            _superBiz = superBiz;
        }

        SuperBiz SuperBiz
        {
            get
            {
                _superBiz.UserId = UserId;
                _superBiz.UserName = UserName;
                return _superBiz;
            }
        }
        PenaltyHeaderBiz PenaltyHeaderBiz
        {
            get
            {
                return _PenaltyHeadersBiz;
            }
        }



    }
}