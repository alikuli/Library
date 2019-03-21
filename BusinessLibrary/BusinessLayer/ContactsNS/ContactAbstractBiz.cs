using AliKuli.Extentions;
using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
//using ModelsClassLibrary.ModelsNS.PlacesNS.EmailAddressNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;


namespace UowLibrary.EmailAddressNS
{
    public abstract partial class ContactAbstractBiz<TEntity> :
        BusinessLayer<TEntity> where TEntity :
        PhoneEmailAddressAbstract, ICommonWithId
    {
        PersonBiz _personBiz;
        CountryBiz _countryBiz;
        public ContactAbstractBiz(IRepositry<TEntity> entityDal, BizParameters bizParameters, PersonBiz personBiz, CountryBiz countryBiz)
            : base(entityDal, bizParameters)
        {
            _personBiz = personBiz;
            _countryBiz = countryBiz;

        }

        public PersonBiz PersonBiz
        {
            get
            {
                _personBiz.IsNullThrowException();
                _personBiz.UserId = UserId;
                _personBiz.UserName = UserName;
                return _personBiz;
            }
        }

        public CountryBiz CountryBiz
        {
            get
            {
                _countryBiz.IsNullThrowException();
                _countryBiz.UserId = UserId;
                _countryBiz.UserName = UserName;
                return _countryBiz;
            }
        }
        public UserBiz UserBiz
        {
            get
            {
                return PersonBiz.UserBiz;
            }
        }


        public override void Fix(ControllerCreateEditParameter parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
            base.Fix(parm);
            TEntity entity = parm.Entity as TEntity;
            entity.IsNullThrowException("Unable to unbox email");

            if (entity.PersonId.IsNullOrWhiteSpace())
            {
                string personId = GetPersonIdForCurrentUser();
                entity.PersonId = personId;
            }

        }

        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {


            var lstEntities = await FindAllAsync();

            if (lstEntities.IsNullOrEmpty())
                return null;


            if (lstEntities.IsNullOrEmpty())
                return null;

            string personId = GetPersonIdForCurrentUser();


            var lstTEntity = lstEntities
                .Cast<TEntity>().ToList();

            //var lstTEntitySelected = lstTEntity
            //    .Where(x => x.People.Any(yield => yield.Id == personId)).ToList();

            if (lstTEntity.IsNullOrEmpty())
                return null;

            //get all addresses with this person's Id

            List<TEntity> tentityList = lstEntities.Where(x => x.PersonId == personId).ToList();


            //foreach (var item in lstTEntity)
            //{

            //    if (item.People.IsNullOrEmpty())
            //        continue;
            //    foreach (var pp in item.People)
            //    {
            //        if(pp.Id == personId)
            //            tentityList.Add(item);
            //    }
            //}


            var lstIcommonWithId = tentityList
                .Cast<ICommonWithId>().ToList();

            return lstIcommonWithId;


        }

        //protected IList<ICommonWithId> flatternFileForPerson(IList<ICommonWithId> lst)
        //{
        //    string personId = GetPersonIdForCurrentUser();

        //    List<PhoneEmailAddressAbstract> lstPhoneEmailAddressAbstract =
        //        lst.Cast<PhoneEmailAddressAbstract>().ToList();

        //    lstPhoneEmailAddressAbstract.IsNullOrEmptyThrowException("Unable to cast to PhoneEmail");

        //    //List<FlattenedModelForPerson> flatFile = new List<FlattenedModelForPerson>();
        //    //foreach (var phoneEmail in lstPhoneEmailAddressAbstract)
        //    //{
        //    //    if (phoneEmail.People.IsNullOrEmpty())
        //    //        continue;

        //    //    foreach (var p in phoneEmail.People)
        //    //    {
        //    //        Select this phoneEmail if Person is found in it.
        //    //        if (p.Id == person.Id)
        //    //        {
        //    //            FlattenedModelForPerson puFlat = new FlattenedModelForPerson();
        //    //            puFlat.Id = phoneEmail.Id;
        //    //            flatFile.Add(puFlat);
        //    //        }
        //    //    }
        //    //}

        //    //if (flatFile.IsNullOrEmpty())
        //    //    return null;

        //    //List<ICommonWithId> lstSelected = new List<ICommonWithId>();
        //    //foreach (var item in flatFile)
        //    //{
        //    //    ICommonWithId ic = lst.FirstOrDefault(x => x.Id == item.Id);
        //    //    ic.IsNullThrowException("Record not found. Programming error.");
        //    //    lstSelected.Add(ic);
        //    //}
        //    var lstIcommonwithId = (lst
        //        .Cast<TEntity>()
        //        .Where(x => x.People.Any(yield => yield.Id == personId)))
        //        .Cast<ICommonWithId>().ToList();

        //    return lstIcommonwithId;
        //}

        /// <summary>
        /// The domain data for this can be narowed so that the search takes place
        /// between bounds as sometimes is required. Eg. Same user cannot have a duplicate address, 
        /// but other users can have the same address with a different record.
        /// Sometimes addresses change... Phone numbers change...
        /// TODO During verification, we need to unverify the previous one?
        /// 
        /// </summary>
        public override IQueryable<TEntity> GetDataToCheckDuplicateName(TEntity entity)
        {

            var dataSet = FindAll().Where(x => x.PersonId == entity.Id);
            return dataSet;


        }

        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
            PhoneEmailAddressAbstract phoneEmailAddressAbstract = icommonWithId as PhoneEmailAddressAbstract;
            phoneEmailAddressAbstract.IsNullThrowException("Unable to unbox Phone Email Address Abstract");

            //this causes the MailerIcon to display
            //indexItem.VerificationStatus = phoneEmailAddressAbstract.VerificationStatusEnum;
            indexItem.VerificationIconResult = GetVerificationIconResult(indexItem.VerificationStatus);


        }
        public string GetPersonIdForCurrentUser()
        {
            UserId.IsNullOrWhiteSpaceThrowException("User not logged in.");
            return GetPersonIdFor(UserId);
        }

        //public string GetPersonIdForCurrentUser()
        //{
        //    UserId.IsNullOrWhiteSpaceThrowException("User not logged in.");
        //    Person person = UserBiz.GetPersonFor(UserId);
        //    person.IsNullThrowException("No person attached to this user");
        //    string personId = person.Id;
        //    return personId;
        //}


        public string GetPersonIdFor(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowException("No user.");
            Person person = UserBiz.GetPersonFor(userId);
            person.IsNullThrowException("No person attached to this user");
            string personId = person.Id;
            return personId;
        }

    }
}
