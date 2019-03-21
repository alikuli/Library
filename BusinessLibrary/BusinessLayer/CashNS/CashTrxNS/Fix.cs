using AliKuli.Extentions;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;


namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxBiz
    {
        public override void Fix(ControllerCreateEditParameter parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("User is not logged in");
            CashTrx paymentTrx = parm.Entity as CashTrx;
            paymentTrx.IsNullThrowException("Unable to unbox payment trx");


            if (paymentTrx.DocNumber == 0)
            {
                paymentTrx.DocNumber = GetNextDocNumber();
            }

            if (parm.Entity.Name.IsNullOrWhiteSpace())
            {
                parm.Entity.Name = UserName;
                parm.Entity.Name = parm.Entity.MakeUniqueName();

            }

            if (paymentTrx.PersonFromId.IsNullOrEmpty())
            {
                paymentTrx.PersonFromId = null;
            }

            paymentTrx.PersonToId.IsNullOrWhiteSpaceThrowException("You must declare who you are paying.");

            Person personTo = PersonBiz.Find(paymentTrx.PersonToId);
            personTo.IsNullThrowException("personTo");
            paymentTrx.PersonTo = personTo;

            base.Fix(parm);

        }
    }
}
