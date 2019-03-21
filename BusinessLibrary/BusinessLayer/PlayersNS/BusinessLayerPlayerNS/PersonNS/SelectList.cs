using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Linq;
using System.Web.Mvc;

namespace UowLibrary.PlayersNS.PersonNS
{
    public partial class PersonBiz
    {

        public override string SelectListCacheKey
        {
            get { return "PersonSelectList"; }
        }



        public SelectList SelectListPersonCategory
        {
            get { return PersonCategoryBiz.SelectList(); }
        }



        //public SelectList SelectListUser
        //{
        //    get { return UserBiz.SelectList(); }
        //}



        //public SelectList SelectListBillAddressesFor(string userId)
        //{
        //    //return AddressBiz.SelectListBillAddressFor(userId);
        //}


        //public SelectList SelectListShipAddressesFor(string userId)
        //{
        //    return AddressBiz.SelectListShipAddressFor(userId);
        //}

        public SelectList SelectListSonOfWifeOf()
        {
            return EnumExtention.ToSelectListSorted<SonOfWifeOfDotOfENUM>(SonOfWifeOfDotOfENUM.Unknown);
        }

        public SelectList SelectListSex()
        {
            return EnumExtention.ToSelectListSorted<SexENUM>(SexENUM.Unknown);
        }

        public SelectList SelectListWithoutPersonFor(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");

            Person person = GetPersonForUserId(userId);
            person.IsNullThrowException("No person found for user.");


            var allItems = FindAll().Where(x => x.Id != person.Id);
            return SelectList_Engine(allItems);

        }

    }
}
