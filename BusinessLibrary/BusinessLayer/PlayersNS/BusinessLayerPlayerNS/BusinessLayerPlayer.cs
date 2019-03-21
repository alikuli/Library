using AliKuli.Extentions;
using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;
namespace UowLibrary.PlayersNS.PlayerAbstractCategoryNS
{

    ///Player names will be the username. This is ensured in fix.
    ///Players are tesxted for being black listed here. 
    ///Player blacklist and suspended is presisted in Users
    ///For a player, we need at least ONE address. 
    ///In the case of customers, we will make an exception because we will force the customer to enter address.
    ///During billing/invoicing we need to be very very strict.
    public abstract partial class BusinessLayerPlayer<TEntity> : BusinessLayer<TEntity> where TEntity : PlayerAbstract, ICommonWithId
    {
        AddressBiz _addressBiz;
        CashTrxBiz _cashTrxBiz;
        public BusinessLayerPlayer(IRepositry<TEntity> dal, BizParameters param, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(dal, param)
        {
            _addressBiz = addressBiz;
            _cashTrxBiz = cashTrxBiz;
        }

        public UserBiz UserBiz
        {
            get
            {
                return AddressBiz.UserBiz;
            }
        }
        public PersonBiz PersonBiz { get { return AddressBiz.PersonBiz; } }

        public AddressBiz AddressBiz
        {
            get
            {
                _addressBiz.IsNullThrowException();
                _addressBiz.UserId = UserId;
                _addressBiz.UserName = UserName;
                return _addressBiz;
            }
        }

        public CashTrxBiz CashTrxBiz
        {
            get
            {
                _cashTrxBiz.IsNullThrowException();
                _cashTrxBiz.UserId = UserId;
                _cashTrxBiz.UserName = UserName;
                return _cashTrxBiz;

            }
        }
        public AddressVerificationHdrBiz AddressVerificationHdrBiz { get { return AddressBiz.AddressVerificationHdrBiz; } }

        public AddressVerificationTrxBiz AddressVerificationTrxBiz { get { return AddressBiz.AddressVerificationHdrBiz.AddressVerificationTrxBiz; } }

        public CountryBiz CountryBiz { get { return AddressBiz.CountryBiz; } }

        public string PakistanId { get { return CountryBiz.PakistanId; } }

        public SelectList SelectListUser
        {
            get { return UserBiz.SelectList(); }
        }

        public SelectList SelectListBillAddressesFor(string userId)
        {
            return AddressBiz.SelectListBillAddressCurrentUser();
        }


        public SelectList SelectListShipAddressesFor(string userId)
        {
            return AddressBiz.SelectListShipAddressCurrentuser();
        }

        public SelectList SelectListWithout(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");

            Person person = PersonBiz.GetPersonForUserId(userId);
            person.IsNullThrowException("No person found for user.");


            var allItems = FindAll().Where(x => x.PersonId != person.Id);
            return SelectList_Engine(allItems);

        }



        public SelectList SelectListWithout(PlayerAbstract entity)
        {
            entity.IsNullThrowExceptionArgument("entity");
            Person person = null;
            if (person.IsNull())
            {
                if (entity.PersonId.IsNullOrWhiteSpaceThrowArgumentException("No person"))
                {
                    //error thrown
                }
                else
                {
                    person = PersonBiz.GetPersonForUserId(entity.PersonId);

                }
            }
            else
            {
                person = entity.Person;
            }

            person.IsNullThrowException("No person found for user.");


            var allItems = FindAll().Where(x => x.PersonId != person.Id);
            return SelectList_Engine(allItems);

        }

        public SelectList SelectListWithout(Person person)
        {
            person.IsNullThrowExceptionArgument("entity");
            var allItems = FindAll().Where(x => x.PersonId != person.Id);
            return SelectList_Engine(allItems);

        }


        public SelectList SelectListOnlyWith(PlayerAbstract entity)
        {
            entity.IsNullThrowExceptionArgument("entity");
            Person person = null;
            if (entity.Person.IsNull())
            {
                if (entity.PersonId.IsNullOrWhiteSpaceThrowArgumentException("No person"))
                {
                    //error thrown
                }
                else
                {
                    person = PersonBiz.GetPersonForUserId(entity.PersonId);

                }
            }
            else
            {
                person = entity.Person;
            }

            person.IsNullThrowException("No person found for user.");


            var allItems = FindAll().Where(x => x.PersonId == person.Id);
            return SelectList_Engine(allItems);

        }

        public SelectList SelectListOnlyWith(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");

            Person person = PersonBiz.GetPersonForUserId(userId);
            person.IsNullThrowException("No person found for user.");


            var allItems = FindAll().Where(x => x.PersonId == person.Id);
            return SelectList_Engine(allItems);

        }

        public override void Fix(ControllerCreateEditParameter parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in!");
            base.Fix(parm);

            PlayerAbstract playerAbstract = PlayerAbstract.UnboxPlayerAbstract(parm);
            if (playerAbstract.PersonId.IsNullOrWhiteSpace())
            {
                //we need to add a person Id
                Person person = UserBiz.GetPersonFor(UserId);
                person.IsNullThrowException("No person for this user");

                playerAbstract.PersonId = person.Id;
                playerAbstract.Person = person;
            }



            if (playerAbstract.Person.IsNull())
                playerAbstract.Person = PersonBiz.Find(playerAbstract.PersonId);

            playerAbstract.Person.IsNullThrowException("No Person");

            //the user name
            if (playerAbstract.Name.IsNullOrWhiteSpace())
                playerAbstract.Name = playerAbstract.Person.Name;

            if (playerAbstract.DefaultBillAddressId.IsNullOrWhiteSpace())
                playerAbstract.DefaultBillAddressId = null;

            if (playerAbstract.DefaultShipAddressId.IsNullOrWhiteSpace())
                playerAbstract.DefaultShipAddressId = null;


        }

        public override ICommonWithId Factory()
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");

            //add the default addresses if they are available
            PlayerAbstract playerAbstract = base.Factory() as PlayerAbstract;

            //get the person so we can get the deafult addresses.
            Person person = UserBiz.GetPersonFor(UserId);
            person.IsNullThrowException("User has no person");

            if (!person.DefaultBillAddressId.IsNullOrWhiteSpace())
            {
                playerAbstract.DefaultBillAddressId = person.DefaultBillAddressId;
                playerAbstract.DefaultShipAddressId = person.DefaultBillAddressId;

            }
            return playerAbstract as ICommonWithId;
        }



        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parameters)
        {
            UserId.IsNullOrWhiteSpaceThrowException();

            //now make sure only those are here which have the same person
            Person person = UserBiz.GetPersonFor(UserId);
            person.IsNullThrowException("There is no Person for this user");

            var lstEntities = await FindAllAsync();

            if (lstEntities.IsNullOrEmpty())
                return null;

            var lstEntitiesForPerson = lstEntities.Where(x => x.PersonId == person.Id).ToList();
            if (lstEntitiesForPerson.IsNullOrEmpty())
                return null;

            IList<ICommonWithId> lstEntitiesForPersonAsCommonWithId = lstEntitiesForPerson.Cast<ICommonWithId>().ToList();
            return lstEntitiesForPersonAsCommonWithId;

        }

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.Show.EditDeleteAndCreate = true;

            //if (parameters.Entity.IsNull())
            //    return;

            //PlayerAbstract playerAbstract = parameters.Entity as PlayerAbstract;
            //playerAbstract.IsNullThrowException("Unable to unbox player Abstract");

            //this is where the user's money amounts are added.
            //if (!UserId.IsNullOrWhiteSpace())
            //{
            //    //user is logged in...
            //    //Get the User's Money balances
            //    playerAbstract.PersonId.IsNullOrWhiteSpaceThrowException("playerAbstract.PersonId");
            //    indexListVM.MenuManager.UserMoneyAccount = CashTrxBiz.UserMoneyAccountForPerson(playerAbstract.PersonId);
            //}

        }


        public TEntity GetEntityFor(string userId)
        {
            Person person = GetPersonForUserId(userId);
            //get the current entity for this pseron
            TEntity entity = FindAll().FirstOrDefault(x => x.PersonId == person.Id);
            entity.IsNullThrowExceptionArgument("Not found entity");
            return entity;
        }
        public string GetPersonIdForCurrentUser()
        {
            Person person = GetPersonForCurrentUser();
            string personId = person.Id;
            return personId;
        }

        public Person GetPersonForCurrentUser()
        {
            UserId.IsNullOrWhiteSpaceThrowException("User not logged in.");
            Person person = GetPersonForUserId(UserId);
            person.IsNullThrowException("No person attached to this user");
            return person;
        }

        public Person GetPersonForUserId(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowException("User not logged in.");
            Person person = UserBiz.GetPersonFor(userId);
            person.IsNullThrowException("No person attached to this user");
            return person;
        }

        public void CreatePlayerForCurrentUser()
        {
            Person person = GetPersonForCurrentUser();
            person.IsNullThrowException("No person found for user.");
            ICommonWithId entity = Factory();
            PlayerAbstract playerAbstract = entity as PlayerAbstract;
            playerAbstract.IsNullThrowException("This is not a player!");

            playerAbstract.PersonId = person.Id;
            playerAbstract.Person = person;

            CreateEntity(playerAbstract as TEntity);



        }




    }



}
