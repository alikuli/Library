using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModels;
namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.Heading.Column = "Cashs";
            indexListVM.Show.EditDeleteAndCreate = true;

        }



        //public BuySellDocStatementENUM IsSaleOrPurchase(BuySellDoc buySellDoc)
        //{
        //    if (buySellDoc.CustomerId.IsNullOrWhiteSpace() && buySellDoc.OwnerId.IsNullOrWhiteSpace())
        //    {
        //        ErrorsGlobal.Add("Both Customer and Owner are empty.", "Event_CreateViewAndSetupSelectList");
        //        throw new Exception();

        //    }

        //    if (buySellDoc.CustomerId.IsNullOrWhiteSpace())
        //    {
        //        //this is a purchase order
        //        return BuySellDocStatementENUM.SaleOrderStatement;

        //    }
        //    if (buySellDoc.OwnerId.IsNullOrWhiteSpace())
        //    {
        //        //this is a sale.
        //        return BuySellDocStatementENUM.PurchaseOrderStatement;


        //    }

        //    ApplicationUser ownerUser = OwnerBiz.GetUserForEntityrWhoIsNotAdminFor(buySellDoc.OwnerId);
        //    ownerUser.IsNullThrowException();

        //    if (UserId == ownerUser.Id)
        //    {
        //        //this is a purchase
        //        return BuySellDocStatementENUM.SaleOrderStatement;
        //    }



        //    //get the CustomerUser
        //    ApplicationUser customerUser = CustomerBiz.GetUserForEntityrWhoIsNotAdminFor(buySellDoc.CustomerId);
        //    customerUser.IsNullThrowException();
        //    if (UserId == customerUser.Id)
        //    {
        //        //this is a sale
        //        return BuySellDocStatementENUM.PurchaseOrderStatement;
        //    }

        //    throw new Exception("Unknown type");
        //}


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);

            BuySellDoc buySellDoc = icommonWithId as BuySellDoc;
            buySellDoc.IsNullThrowException("Unable to unbox buySellDoc");

            //we need to know if this is a sale or a purchase order
            ////get current user's PersonId first...
            BuySellDocumentTypeENUM buySellDocumentTypeEnum = IsSaleOrPurchase(buySellDoc);
            buySellDoc.BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
            indexItem.Name = buySellDoc.FullName();
            //Person person = UserBiz.GetPersonFor(UserId);
        }
        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {
            //get user's customer and owner
            Customer customer = CustomerBiz.GetPlayerFor(UserId);
            Owner owner = OwnerBiz.GetPlayerFor(UserId);


            //now, get all the payment trx for this person
            List<BuySellDoc> allTrx = await FindAllAsync();
            List<BuySellDoc> filteredTrx;

            //now reduce this list so the user is customer or seller
            if (customer.IsNull())
            {
                if (owner.IsNull())
                {
                    //filteredTrx = allTrx.Where(x => x.CustomerId == customerId).AsQueryable();
                    return null;

                }
                else
                {
                    filteredTrx = allTrx.Where(x => x.OwnerId == owner.Id).ToList();

                }
            }
            else
            {
                if (owner.IsNull())
                {
                    filteredTrx = allTrx.Where(x => x.CustomerId == customer.Id).ToList();

                }
                else
                {
                    filteredTrx = allTrx.Where(x => x.CustomerId == customer.Id || x.OwnerId == owner.Id).ToList();

                }

            }

            //cast and return the list
            var lstIcommonwithId = allTrx.Cast<ICommonWithId>().ToList();
            return lstIcommonwithId;



        }



    }
}
