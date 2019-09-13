using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using System.Linq;
using UowLibrary.ParametersNS;

namespace UowLibrary.FreightOffersTrxNS
{
    /// <summary>
    /// The index of the buySellItem is used for Pickup
    /// </summary>
    public partial class FreightOfferTrxBiz
            : BusinessLayer<FreightOfferTrx>
    {

        public FreightOfferTrxBiz(IRepositry<FreightOfferTrx> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }



        public override string SelectListCacheKey
        {
            get { return "SelectListCacheFreightOfferTrx"; }
        }



        public override IQueryable<FreightOfferTrx> GetDataToCheckDuplicateName(FreightOfferTrx entity)
        {
            return base.GetDataToCheckDuplicateName(entity).Where(x => x.BuySellDocId == entity.BuySellDocId && x.DeliverymanId == entity.DeliverymanId);
        }


        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {

            FreightOfferTrx freightOfferTrx = parm.Entity as FreightOfferTrx;
            freightOfferTrx.IsNullThrowException();


            freightOfferTrx.BuySellDocId.IsNullOrWhiteSpaceThrowArgumentException();
            freightOfferTrx.DeliverymanId.IsNullOrWhiteSpaceThrowException();


            freightOfferTrx.Name = freightOfferTrx.MakeUniqueName();
            
            //fix the name
            //freightOfferTrx.Name = buy


            base.Fix(parm);

        }

        public override void Event_ModifyIndexList(ModelsClassLibrary.ViewModels.IndexListVM indexListVM, ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.Show.EditDeleteAndCreate = true;

        }



    }


}


