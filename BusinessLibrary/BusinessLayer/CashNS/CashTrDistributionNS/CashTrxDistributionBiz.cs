using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrDistributionNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;
using AliKuli.Extentions;

namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxDistributionBiz
            : BusinessLayer<CashTrxDistribution>
    {
        public CashTrxDistributionBiz(IRepositry<CashTrxDistribution> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }


        public override string SelectListCacheKey
        {
            get { return "SelectListCacheKeyCashTrxDistribution"; }
        }
        public override void Update(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Update(parm);
        }


        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            CashTrxDistribution ctd = parm.Entity as CashTrxDistribution;
            ctd.IsNullThrowException();

            //this means this cash amount has not been applied.
            //this will be the usual case for cash received from the admin.
            if(ctd.BuySellDocId.IsNullOrWhiteSpace())
            {
                ctd.BuySellDoc = null;
            }
            
        }




    }
}
