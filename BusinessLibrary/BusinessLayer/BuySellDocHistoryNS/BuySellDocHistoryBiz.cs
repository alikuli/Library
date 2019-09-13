using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.VehicalTypeNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using UowLibrary.AddressNS;
using UowLibrary.FreightOffersTrxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.DeliverymanNS;
using UowLibrary.PlayersNS.MessageNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.SalesmanNS;
using UowLibrary.PlayersNS.VehicalTypeNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocHistoryBiz
            : BusinessLayer<BuySellDocHistory>
    {
        public BuySellDocHistoryBiz(IRepositry<BuySellDocHistory> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }


        public override string SelectListCacheKey
        {
            get { return "SelectListBuySellDocHistoryCache"; }
        }


        public override void Fix(ControllerCreateEditParameter parm)
        {
            BuySellDocHistory buySellDocHistory = BuySellDocHistory.UnBox(parm.Entity);
            if (buySellDocHistory.Name.IsNullOrWhiteSpace())
                buySellDocHistory.Name = buySellDocHistory.MakeUniqueName();
        }




    }


}


