using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);

            BuySellDoc buySellDoc = icommonWithId as BuySellDoc;
            buySellDoc.IsNullThrowException("Unable to unbox buySellDoc");

            //get current user's PersonId first...
            Person person = UserBiz.GetPersonFor(UserId);
        }
        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {
            //get user's customer and owner
            Customer customer = CustomerBiz.GetEntityFor(UserId);
            Owner owner = OwnerBiz.GetEntityFor(UserId);


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
