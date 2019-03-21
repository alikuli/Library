using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using AliKuli.Extentions;
using System.Collections.Generic;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Linq;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {
        private long GetNextDocNumber()
        {
            List<BuySellDoc> lst = FindAll().ToList();
            if (lst.IsNullOrEmpty())
                return 1;
            long maxExisting = lst.Max(x => x.DocumentNumber);
            return maxExisting + 1;

        }

        public override void Fix(ControllerCreateEditParameter parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("User is not logged in");
            BuySellDoc buySellDoc = parm.Entity as BuySellDoc;
            buySellDoc.IsNullThrowException("Unable to unbox payment trx");


            fixDocumentNumber(buySellDoc);
            fixName(parm);
            fixCustomer(buySellDoc);
            fixSeller(buySellDoc);
            fixAddressShipTo(buySellDoc);
            fixInformTo(buySellDoc);

            base.Fix(parm);

        }

        private void fixSeller(BuySellDoc buySellDoc)
        {
            if (buySellDoc.OwnerId.IsNullOrWhiteSpace())
            {
                if(buySellDoc.Owner.IsNull())
                {
                    throw new Exception("No owner found!");
                }
                buySellDoc.OwnerId = buySellDoc.Owner.Id;
            }
            else
            {
                if (buySellDoc.Owner.IsNull())
                {
                    buySellDoc.Owner = OwnerBiz.Find(buySellDoc.OwnerId);
                    buySellDoc.Owner.IsNullThrowException("Owner not found");
                }
            }
            
        }

        private void fixInformTo(BuySellDoc buySellDoc)
        {
            if (buySellDoc.AddressInformToId.IsNullOrWhiteSpace())
            {
                buySellDoc.AddressInformToId = null;
            }
            else
            {
                buySellDoc.AddressInformTo = AddressBiz.Find(buySellDoc.AddressInformToId);
                buySellDoc.AddressInformTo.IsNullThrowException("Inform To Address not found");
            }
        }

        private void fixAddressShipTo(BuySellDoc buySellDoc)
        {
            if (buySellDoc.AddressShipToId.IsNullOrWhiteSpace())
            {
                buySellDoc.AddressShipToId = null;
            }
            else
            {
                buySellDoc.AddressShipTo = AddressBiz.Find(buySellDoc.AddressShipToId);
                buySellDoc.AddressShipTo.IsNullThrowException("Ship To Address not found");
            }
        }

        private void fixCustomer(BuySellDoc buySellDoc)
        {
            //do we want to allow empty orders??
            if (buySellDoc.CustomerId.IsNullOrWhiteSpace())
            {
                buySellDoc.CustomerId = null;
            }
            else
            {
                buySellDoc.Customer = CustomerBiz.Find(buySellDoc.CustomerId);
                buySellDoc.Customer.IsNullThrowException("Customer not found!");
            }
        }

        private static void fixName(ControllerCreateEditParameter parm)
        {
            if (parm.Entity.Name.IsNullOrWhiteSpace())
            {
                parm.Entity.Name = parm.Entity.MakeUniqueName();

            }
        }

        private void fixDocumentNumber(BuySellDoc buySellDoc)
        {
            if (buySellDoc.DocumentNumber == 0)
            {
                buySellDoc.DocumentNumber = GetNextDocNumber();
            }
        }
    }
}
