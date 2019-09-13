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

        public SelectList SelectListForUserId(string userId)
        {
            try
            {

                //throw new NotImplementedException(); 
                string pId = getPersonId(userId);
                pId.IsNullOrWhiteSpaceThrowException();
                return SelectListForPersonId(pId);

            }
            catch (System.Exception)
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());

            }

        }
        public SelectList SelectListForPersonId(string personId)
        {
            try
            {


                //get all address with this personId
                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == personId);
                SelectList allForUserId = SelectList_Engine(iqAllBillToForUserId);
                return allForUserId;

            }
            catch (System.Exception)
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());

            }

        }
        public SelectList SelectListForAddressId(string addressId)
        {
            try
            {
                addressId.IsNullThrowException();
                AddressMain addy = Find(addressId);
                addy.IsNullThrowException();
                addy.PersonId.IsNullOrWhiteSpaceThrowException();

                return SelectListForPersonId(addy.PersonId);

            }
            catch (System.Exception)
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());

            }

        }



        public SelectList SelectListBillAddressCurrentUser()
        {
            try
            {
                UserId.IsNullOrWhiteSpaceThrowException("Not logged in");
                return SelectListBillAddressForUser(UserId);
                //throw new NotImplementedException(); 
                //string pId = GetPersonIdForCurrentUser();

                ////get all address with this personId
                //IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == pId && x.AddressType.IsBillAddress == true);
                //SelectList allForUserId = Dal.SelectList_Engine(iqAllBillToForUserId);
                //return allForUserId;

            }
            catch (System.Exception)
            {

            }

            return new SelectList(Enumerable.Empty<SelectListItem>());
        }
        public SelectList SelectListBillAddressForUser(string userId)
        {
            try
            {
                //throw new NotImplementedException(); 
                string pId = GetPersonIdFor(userId);

                //get all address with this personId
                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == pId && x.AddressType.IsBillAddress == true);
                SelectList allForUserId = SelectList_Engine(iqAllBillToForUserId);
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
                return SelectListShipAddressForPerson(pId);

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
                //get the personId
                string pId = getPersonId(userId);
                return SelectListShipAddressForPerson(pId);
            }
            catch (System.Exception)
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());

            }

        }

        public SelectList SelectListShipAddressForPerson(string personId)
        {

            try
            {
                personId.IsNullThrowExceptionArgument();
                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == personId && x.AddressType.IsShipAddress == true);
                SelectList allForUserId = SelectList_Engine(iqAllBillToForUserId);
                return allForUserId;
            }
            catch (System.Exception)
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());

            }

        }
        private string getPersonId(string userId)
        {
            Person person = PersonBiz.GetPersonForUserId(userId);
            person.IsNullThrowException();

            string pId = person.Id;
            return pId;
        }


        public SelectList SelectListShipAddressFor(Person person)
        {

            try
            {
                person.IsNullThrowExceptionArgument("person");
                string pId = person.Id;

                IQueryable<AddressMain> iqAllBillToForUserId = FindAll().Where(x => x.PersonId == pId && x.AddressType.IsShipAddress == true);
                SelectList allForUserId = SelectList_Engine(iqAllBillToForUserId);
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
                SelectList allForUserId = SelectList_Engine(iqAllBillToForUserId);
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
                SelectList allForUserId = SelectList_Engine(iqAllBillToForUserId);
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
                SelectList allForUserId = SelectList_Engine(iqAllBillToForUserId);
                return allForUserId;

            }
            catch (System.Exception)
            {

            }

            return new SelectList(Enumerable.Empty<SelectListItem>());
        }
    }
}
