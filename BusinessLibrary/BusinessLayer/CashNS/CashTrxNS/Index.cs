using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashsNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UowLibrary.PlayersNS.PersonNS;

namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxBiz
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.Heading.Column = "Cashs";

        }


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);

            CashTrx cashTrx = icommonWithId as CashTrx;
            cashTrx.IsNullThrowException("Unable to unbox cashTrx");

            //get current user's PersonId first...
            Person person = UserBiz.GetPersonFor(UserId);
            if (person.IsNull())
            {
                indexItem.AllowDelete = false;
                indexItem.AllowEdit = false;
            }
            else
                if (person.Id == cashTrx.PersonToId)
                {
                    indexItem.AllowDelete = true;
                    indexItem.AllowEdit = false;

                }
                else
                {
                    indexItem.AllowDelete = false;
                    indexItem.AllowEdit = false;

                }
        }
        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {


            //first get a list of all the people
            List<Person> peopleList = await PersonBiz.FindAllAsync();
            Person person = GetPersonForUser(UserId, peopleList);
            person.IsNullThrowException("Person not found!");

            //now, get all the payment trx for this person
            List<CashTrx> allTrx = await FindAllAsync();
            var allTrxForPerson = allTrx.Where(x => x.PersonFromId == person.Id || x.PersonToId == person.Id).ToList();
            //cast and return the list
            var lstIcommonwithId = allTrxForPerson.Cast<ICommonWithId>().ToList();
            return lstIcommonwithId;



        }



    }
}
