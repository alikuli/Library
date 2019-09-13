using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.CashNS.CashEncashmentTrxNS;
using System.Web.Mvc;
using UowLibrary.CashEncashmentTrxNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    public class CashEncashmentTrxsController : EntityAbstractController<CashEncashmentTrx>
    {

        CashEncashmentTrxBiz _CashEncashmentTrxsBiz;

        public CashEncashmentTrxsController(CashEncashmentTrxBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _CashEncashmentTrxsBiz = biz;
        }




    }
}
