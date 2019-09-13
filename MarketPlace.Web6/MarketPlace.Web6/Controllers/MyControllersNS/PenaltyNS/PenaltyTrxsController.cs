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
    public class PenaltyTrxsController : EntityAbstractController<PenaltyTrx>
    {

        PenaltyTrxBiz _PenaltyTrxsBiz;
        SuperBiz _superBiz;


        public PenaltyTrxsController(AbstractControllerParameters param, SuperBiz superBiz)
            : base(superBiz.PenaltyTrxBiz, param)
        {
            _PenaltyTrxsBiz = superBiz.PenaltyTrxBiz;
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
        PenaltyTrxBiz PenaltyTrxBiz
        {
            get
            {
                return _PenaltyTrxsBiz;
            }
        }



    }
}