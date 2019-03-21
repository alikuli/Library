using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
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

        public SelectList SelectListBillAddressCurrentUser()
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

        public SelectList SelectListShipAddressCurrentuser()
        {

            try
            {
                string pId = GetPersonIdForCurrentUser();
                return SelectListShipAddressFor(pId);

            }
            catch (System.Exception)
            {

            }

            return new SelectList(Enumerable.Empty<SelectListItem>());
        }
        public SelectList SelectListShipAddressFor(string userId)
        {

            try
            {
                userId.IsNullThrowExceptionArgument("userId");
                string pId = userId;

                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == pId && x.AddressType.IsShipAddress == true);
                SelectList allForUserId = Dal.SelectList_Engine(iqAllBillToForUserId);
                return allForUserId;

            }
            catch (System.Exception)
            {

            }

            return new SelectList(Enumerable.Empty<SelectListItem>());
        }


        public SelectList SelectListShipAddressFor(Person person)
        {

            try
            {
                person.IsNullThrowExceptionArgument("person");
                string pId = person.Id;

                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == pId && x.AddressType.IsShipAddress == true);
                SelectList allForUserId = Dal.SelectList_Engine(iqAllBillToForUserId);
                return allForUserId;

            }
            catch (System.Exception)
            {

            }

            return new SelectList(Enumerable.Empty<SelectListItem>());
        }

        
        public SelectList SelectListInformAddressCurrentUser()
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

        public SelectList SelectListInformAddressFor(string userId)
        {
            try
            {
                userId.IsNullThrowExceptionArgument("No user");
                string pId = userId;

                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == pId && x.AddressType.IsInformAddress == true);
                SelectList allForUserId = Dal.SelectList_Engine(iqAllBillToForUserId);
                return allForUserId;

            }
            catch (System.Exception)
            {

            }

            return new SelectList(Enumerable.Empty<SelectListItem>());
        }

        public SelectList SelectListInformAddressFor(Person person)
        {
            try
            {
                person.IsNullThrowException("person");

                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == person.Id && x.AddressType.IsInformAddress == true);
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
