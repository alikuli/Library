using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.CashNS.CashEncashmentTrxNS;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.ParametersNS;
namespace UowLibrary.CashEncashmentTrxNS
{
    public partial class CashEncashmentTrxBiz : BusinessLayer<CashEncashmentTrx>
    {
        public CashEncashmentTrxBiz(IRepositry<CashEncashmentTrx> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }


        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Fix(parm);

            CashEncashmentTrx cashEncashmentTrx = CashEncashmentTrx.Unbox(parm.Entity);

            addDocumentNumber(cashEncashmentTrx);
            add_Name(cashEncashmentTrx);
            
        }


        //this must always be added after the document number and fixing IsApproved.
        //it uses these in the name
        private static void add_Name(CashEncashmentTrx cashEncashmentTrx)
        {
            if (cashEncashmentTrx.Name.IsNullOrWhiteSpace())
            {
                cashEncashmentTrx.Name = cashEncashmentTrx.MakeUniqueName();

            }
        }

        private void addDocumentNumber(CashEncashmentTrx cashEncashmentTrx)
        {
            if (cashEncashmentTrx.DocumentNo == 0)
            {
                List<CashEncashmentTrx> lst = FindAll().ToList();
                long docNo =1;
                if (lst.IsNullOrEmpty())
                { }
                else
                {
                    docNo = lst.Max(x => x.DocumentNo) + 1;
                }
                cashEncashmentTrx.DocumentNo = docNo;
            }
        }

    }
}
