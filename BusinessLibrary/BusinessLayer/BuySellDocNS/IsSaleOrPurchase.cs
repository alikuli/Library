using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using AliKuli.Extentions;
using System.Collections.Generic;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using EnumLibrary.EnumNS;
using UserModels;
using System;


namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {

        public BuySellDocumentTypeENUM IsSaleOrPurchase(BuySellDoc buySellDoc)
        {
            if (buySellDoc.CustomerId.IsNullOrWhiteSpace() && buySellDoc.OwnerId.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("Both Customer and Owner are empty.", "Event_CreateViewAndSetupSelectList");
                throw new Exception();

            }

            if (buySellDoc.CustomerId.IsNullOrWhiteSpace())
            {
                //this is a purchase order
                return BuySellDocumentTypeENUM.Sale;

            }
            if (buySellDoc.OwnerId.IsNullOrWhiteSpace())
            {
                //this is a sale.
                return BuySellDocumentTypeENUM.Purchase;


            }

            ApplicationUser ownerUser = OwnerBiz.GetUserForEntityrWhoIsNotAdminFor(buySellDoc.OwnerId);
            ownerUser.IsNullThrowException();

            if (UserId == ownerUser.Id)
            {
                //this is a purchase
                return BuySellDocumentTypeENUM.Sale;
            }



            //get the CustomerUser
            ApplicationUser customerUser = CustomerBiz.GetUserForEntityrWhoIsNotAdminFor(buySellDoc.CustomerId);
            customerUser.IsNullThrowException();
            if (UserId == customerUser.Id)
            {
                //this is a sale
                return BuySellDocumentTypeENUM.Purchase;
            }

            throw new Exception("Unknown type");
        }

    }
}
