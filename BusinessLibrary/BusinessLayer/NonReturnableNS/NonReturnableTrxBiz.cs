using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.NonReturnableNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.NonReturnableNS
{
    public partial class NonReturnableTrxBiz : BusinessLayer<NonReturnableTrx>, UowLibrary.BusinessLayer.NonReturnableNS.INonReturnableTrxBiz
    {
        public NonReturnableTrxBiz(IRepositry<NonReturnableTrx> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }
    }
}
