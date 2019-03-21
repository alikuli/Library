using System.Web.Mvc;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Linq;

namespace UowLibrary.PlayersNS.CustomerNS
{
    public partial class CustomerBiz
    {

        public override string SelectListCacheKey
        {
            get { return "CustomerSelectList"; }
        }

        public SelectList SelectListCustomerCategory
        {
            get { return CustomerCategoryBiz.SelectList(); }
        }

        //public SelectList SelectListUser {
        //    get { return UserBiz.SelectList(); }
        //}

        //public SelectList SelectListWithout(string userId)
        //{
        //    userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");

        //    Person person = PersonBiz.GetPersonForUserId(userId);
        //    person.IsNullThrowException("No person found for user.");

            
        //    var allItems = FindAll().Where(x => x.PersonId != person.Id);
        //    return SelectList_Engine(allItems);

        //}

    }
}
