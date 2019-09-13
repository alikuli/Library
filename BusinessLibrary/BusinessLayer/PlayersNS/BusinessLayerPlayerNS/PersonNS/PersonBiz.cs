using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Linq;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonCategoryNS;
using UserModels;

namespace UowLibrary.PlayersNS.PersonNS
{
    public partial class PersonBiz : BusinessLayer<Person>
    {
        readonly UserBiz _userBiz;
        readonly PersonCategoryBiz _personCategoryBiz;
        //readonly AddressBiz _addressBiz;
        //readonly AddressBiz _addressBiz;
        public PersonBiz(IRepositry<Person> entityDal, BizParameters bizParameters, PersonCategoryBiz personCategoryBiz, UserBiz userBiz)
            : base(entityDal, bizParameters)
        {

            _userBiz = userBiz;
            //_addressBiz = addressBiz;
            _personCategoryBiz = personCategoryBiz;
        }

        public UserBiz UserBiz { get { return _userBiz; } }


        public PersonCategoryBiz PersonCategoryBiz { get { return _personCategoryBiz; } }
        //public AddressBiz AddressBiz { get { return _addressBiz; } }
        //public AddressVerificationHdrBiz AddressVerificationHdrBiz
        //{
        //    get
        //    {
        //        return AddressBiz.AddressVerificationHdrBiz;
        //    }
        //}

        //public AddressVerificationTrxBiz AddressVerificationTrxBiz
        //{
        //    get
        //    {
        //        return AddressBiz.AddressVerificationHdrBiz.AddressVerificationTrxBiz;
        //    }
        //}


        //public CountryBiz CountryBiz
        //{
        //    get
        //    {
        //        return AddressBiz.CountryBiz;
        //    }
        //}

        //public string PakistanId
        //{
        //    get
        //    {
        //        return CountryBiz.PakistanId;
        //    }
        //}

        public Person GetPersonForUserId(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowArgumentException("No user!");
            ApplicationUser user = UserBiz.FindAll().FirstOrDefault(x => x.Id == userId);
            user.IsNullThrowException("User");
            Person person = Find(user.PersonId);
            return person;
        }
        public string GetPersonIdForUserId(string userId)
        {
            Person person = GetPersonForUserId(userId);
            return person.Id;
        }

        public ApplicationUser GetUserFor(string personId)
        {
            Person person = Find(personId);
            person.IsNullThrowException();
            //we are using the fact that they have the same name
            ApplicationUser user = UserBiz.FindByUserName_UserManager(person.Name);
            user.IsNullThrowException();
            //every person must have a user.
            return user;
        }
    }
}
