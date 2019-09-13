using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace ModelsClassLibrary.CashNS.PenaltyNS

{
    public partial class PenaltyTrxBiz: BusinessLayer<PenaltyTrx>
    {
        public PenaltyTrxBiz(IRepositry<PenaltyTrx> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            PenaltyTrx penaltyTrx = PenaltyTrx.Unbox(parm.Entity);
            fix_Name(penaltyTrx);
        }

        private void fix_Name(PenaltyTrx penaltyTrx)
        {
            penaltyTrx.Name = penaltyTrx.MakeUniqueName();
        }

    }
}
