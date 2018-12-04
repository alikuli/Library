using ModelsClassLibrary.ModelsNS.AddressNS;
using System.Linq;
using System.Web.Mvc;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {
        public override string SelectListCacheKey
        {
            get { return "AddresssSelectListData"; }
        }

        public SelectList SelectListBillAddress()
        {
            try
            {
                //throw new NotImplementedException(); 
                string pId = GetPersonIdForCurrentUser();
                
                //get all address with this personId
                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == pId && x.AddressType.IsBillAddress == true);
                SelectList allForUserId = Dal.SelectList_Engine(iqAllBillToForUserId);
                return allForUserId;

            }
            catch (System.Exception)
            {

            }

            return new SelectList(Enumerable.Empty<SelectListItem>());
        }

        public SelectList SelectListShipAddress()
        {

            try
            {
                string pId = GetPersonIdForCurrentUser();

                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == pId && x.AddressType.IsShipAddress == true);
                SelectList allForUserId = Dal.SelectList_Engine(iqAllBillToForUserId);
                return allForUserId;

            }
            catch (System.Exception)
            {

            }

            return new SelectList(Enumerable.Empty<SelectListItem>());
        }

        public SelectList SelectListInformAddress()
        {
            try
            {
                string pId = GetPersonIdForCurrentUser();

                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == pId && x.AddressType.IsInformAddress == true);
                SelectList allForUserId = Dal.SelectList_Engine(iqAllBillToForUserId);
                return allForUserId;

            }
            catch (System.Exception)
            {

            }

            return new SelectList(Enumerable.Empty<SelectListItem>());
        }

    }
}
