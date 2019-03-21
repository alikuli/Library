using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using AliKuli.Extentions;
using System.Collections.Generic;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using System;


namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {


        public override void ErrorCheck(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.ErrorCheck(parm);
            BuySellDoc buySellDoc = parm.Entity as BuySellDoc;
            buySellDoc.IsNullThrowException("Could not unbox BuySellDoc");
            saleToSelfNotAllowed(buySellDoc);
        }

        private void saleToSelfNotAllowed(BuySellDoc buySellDoc)
        {

            string personCustomerId = buySellDoc.Customer.PersonId;
            string personSellerId = buySellDoc.Owner.PersonId;
            if(personCustomerId == personSellerId)
            {
                ErrorsGlobal.Add(string.Format("This product belongs to you! You cannot sell to you self!"), "saleToSelfNotAllowed");
                throw new Exception(ErrorsGlobal.ToString());
            }
        }
    }
}
