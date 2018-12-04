using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlacesNS.PhoneNS;
using UowLibrary.ParametersNS;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UowLibrary.PlayersNS.PersonNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using InterfacesLibrary.SharedNS;
using System.Linq;
using UowLibrary.EmailAddressNS;
using System;

namespace UowLibrary.PhoneNS
{
    public partial class PhoneBiz : ContactAbstractBiz<Phone>
    {
        public PhoneBiz(IRepositry<Phone> entityDal, BizParameters bizParameters, PersonBiz personBiz, CountryBiz countryBiz)
            : base(entityDal, bizParameters, personBiz, countryBiz)
        {
        }

        public override string SelectListCacheKey
        {
            get { return "PhonesSelectListData"; }

        }

        public override void Fix(ControllerCreateEditParameter parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in. Please log in");
            base.Fix(parm);
            Phone phone = parm.Entity as Phone;

            if (phone.CountryId.IsNullOrWhiteSpace())
                phone.CountryId = null;

            //if(phone.PersonId.IsNullOrEmpty())
            //{
            //    Person person = UserBiz.GetPersonFor(UserId);
            //    person.IsNullThrowException();

            //    phone.PersonId = person.Id;
            //    phone.Person = person;
            //} 

        }

        //public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        //{
        //    base.Event_ModifyIndexList(indexListVM, parameters);
        //    indexListVM.Show.EditDeleteAndCreate = true;

        //}


        //public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        //{

        //    //throw new Exception();
        //    string personId = GetPersonIdForCurrentUser();

        //    var lst = (await base.GetListForIndexAsync(parms));

        //    if (lst.IsNullOrEmpty())
        //        return null;

        //    var lstIcommonwithId = (lst
        //        .Cast<Phone>()
        //        .Where(x => x.People.Any( y => y.Id == personId)))
        //        .Cast<ICommonWithId>().ToList();

        //    return lstIcommonwithId;
        //}

    }
}
