using AliKuli.Extentions;
using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;
using UserModels;
using System.Data.Entity;


namespace UowLibrary.PlayersNS.PlayerAbstractCategoryNS
{

    ///Player names will be the username. This is ensured in fix.
    ///Players are texted for being black listed here. 
    ///Player blacklist and suspended is presisted in Users
    ///For a player, we need at least ONE address. 
    ///In the case of customers, we will make an exception because we will force the customer to enter address.
    ///During billing/invoicing we need to be very very strict.
    ///Admin accounts will not be able to participate in buying and selling
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
            //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in!");
            base.Fix(parm);

            PlayerAbstract playerAbstract = PlayerAbstract.UnboxPlayerAbstract(parm);

            if(playerAbstract.Name.IsNullOrWhiteSpace())
            {
                if(playerAbstract.Person.IsNull())
                {
                    if(playerAbstract.PersonId.IsNull())
                    {
                        throw new Exception("No person");
                    }
                    else
                    {
                        Person person = PersonBiz.Find(playerAbstract.PersonId);
                        person.IsNullThrowException();
                        playerAbstract.Person = person;
                    }

                }
                playerAbstract.Name = playerAbstract.Person.Name;
            }


            //if (!playerAbstract.PersonId.IsNullOrWhiteSpace())
            //{
            //    //we need to add a person Id
            //    Person person = PersonBiz.Find(playerAbstract.PersonId);
            //    person.IsNullThrowException();

            //    playerAbstract.PersonId = person.Id;
            //    playerAbstract.Person = person;
            //}
            //else
            //{
            //    throw new Exception("No person found");
            //}



            //if (playerAbstract.Person.IsNull())
            //    playerAbstract.Person = PersonBiz.Find(playerAbstract.PersonId);

            //playerAbstract.Person.IsNullThrowException("No Person");

            //the user name


            //if (playerAbstract.Name.IsNullOrWhiteSpace())
            //    playerAbstract.Name = playerAbstract.Person.Name;

            if (playerAbstract.DefaultBillAddressId.IsNullOrWhiteSpace())
                playerAbstract.DefaultBillAddressId = null;

            if (playerAbstract.DefaultShipAddressId.IsNullOrWhiteSpace())
                playerAbstract.DefaultShipAddressId = null;


        }

        public override ICommonWithId Factory()
        {
            //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");

            //add the default addresses if they are available
            PlayerAbstract playerAbstract = base.Factory() as PlayerAbstract;

            ////get the person so we can get the deafult addresses.
            //Person person = UserBiz.GetPersonFor(UserId);
            //person.IsNullThrowException("User has no person");

            //if (!person.DefaultBillAddressId.IsNullOrWhiteSpace())
            //{
            //    playerAbstract.DefaultBillAddressId = person.DefaultBillAddressId;
            //    playerAbstract.DefaultShipAddressId = person.DefaultBillAddressId;

            //}
            return playerAbstract as ICommonWithId;
        }



        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parameters)
        {
            UserId.IsNullOrWhiteSpaceThrowException();

            //now make sure only those are here which have the same person
            Person person = UserBiz.GetPersonFor(UserId);
            person.IsNullThrowException("There is no Person for this user");



            List<TEntity> lst = new List<TEntity>();


            if(UserBiz.IsAdmin(UserId))
            {
                lst = await FindAllAsync();
                parameters.IsUserAdmin = true;
            }
            else
            {
                lst = await FindAll().Where(x => x.PersonId == person.Id).ToListAsync();
                parameters.IsUserAdmin = false;
            }


            if (lst.IsNullOrEmpty())
                return null;
            
            IList<ICommonWithId> lstEntitiesForPersonAsCommonWithId = lst.Cast<ICommonWithId>().ToList();
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


        public virtual TEntity GetPlayerFor(string userId)
        {
            if (userId.IsNullOrWhiteSpace())
                return null;

            Person person = GetPersonForUserId(userId);

            //get the current entity for this pseron
            TEntity entity = FindAll().FirstOrDefault(x => x.PersonId == person.Id);
            //entity.IsNullThrowExceptionArgument("Not found entity");
            return entity;
        }

        public TEntity GetPlayerForPersonId(string personId)
        {
            if (personId.IsNullOrWhiteSpace())
                return null;

            TEntity player = FindAll().FirstOrDefault(x => x.PersonId == personId);
            return player;
        }

        public Person GetPersonForPlayer(string playerId)
        {
            playerId.IsNullOrWhiteSpaceThrowArgumentException();
            TEntity entity = FindAll().FirstOrDefault(x => x.Id == playerId);
            if (entity.IsNull())
                return null;

            Person person = entity.Person;
            Detach(entity);
            return person;
        }
        public string GetPersonIdForPlayer(string playerId)
        {
            playerId.IsNullOrWhiteSpaceThrowArgumentException();
            TEntity entity = FindAll().FirstOrDefault(x => x.Id == playerId);
            entity.IsNullThrowException();

            entity.PersonId.IsNullOrWhiteSpaceThrowException();
            return entity.PersonId;
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

        public ApplicationUser GetUser(string userId)
        {
            ApplicationUser user = UserBiz.Find(userId);
            return user;
        }

        /// <summary>
        /// This automatically creates a person if one does not exist.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Person GetPersonForUserId(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowException("User not logged in.");
            //find the person
            Person userPerson = UserBiz.GetPersonFor(userId);
            ApplicationUser user = GetUser(userId);
            user.IsNullThrowException();

            if (userPerson.IsNull())
            {
                //the userPerson is coming back as null. This can also happen if the user
                //and the person have become disconnected due to user being deleted.
                //so check to see if the person exists. If the person exists then attach him to the
                //user.
                Person personWithAdminName = PersonBiz.FindByName(user.UserName);

                //create a person if one does not exist.
                if (personWithAdminName.IsNull())
                {
                    //create person

                    user.IsNullThrowException();

                    userPerson = PersonBiz.Factory() as Person;
                    userPerson.Name = user.Name;

                    if (userPerson.Users.IsNull())
                        userPerson.Users = new List<ApplicationUser>();

                    userPerson.Users.Add(user);
                    PersonBiz.Create(userPerson);

                }
                else
                {
                    personWithAdminName.Users.Add(user);
                    PersonBiz.Update(personWithAdminName);


                }
                SaveChanges();
            }


            return userPerson;
        }


        public ApplicationUser GetUserForEntityrWhoIsNotAdminFor(string entityId)
        {
            //get the entity
            TEntity entity = Find(entityId);

            entity.IsNullThrowException("Entity not found");

            PlayerAbstract playerAbstract = entity as PlayerAbstract;
            playerAbstract.IsNullThrowException("playerAbstract");

            string personId = entity.PersonId;
            personId.IsNullOrWhiteSpaceThrowArgumentException("personId");
            Person person = PersonBiz.Find(personId);
            person.IsNullThrowException("No person found");

            //Now get the user. Here we can get multiple users if person is being impersonated by admin.
            //we have to deal with that.

            List<ApplicationUser> usersForPerson = UserBiz.FindAll().Where(x => x.PersonId == personId).ToList();
            usersForPerson.IsNullOrEmptyThrowException("Person not found");

            //if it is the admininistrators own account, there will be only one person and the item will get thru
            if (usersForPerson.Count() == 1)
                return usersForPerson[0];


            //Getting rid of administrators

            //if more than 1 person has been found then the admin is impersonating this person
            //at this time.
            //get rid of all administrators.

            //find all administrators
            List<ApplicationUser> allAdminList = UserBiz.GetAllAdmin();

            //create a duplicate list of usersForPerson we will use for itteration
            List<ApplicationUser> duplicate_usersForPerson = new List<ApplicationUser>(); ;
            foreach (ApplicationUser user in usersForPerson)
            {
                duplicate_usersForPerson.Add(user);
            }

            //now itterate thru duplicate_usersForPerson and remove admins from usersForPerson 
            foreach (var user in duplicate_usersForPerson)
            {
                if (allAdminList.Contains(user))
                {
                    usersForPerson.Remove(user);
                }
            }

            //all administrators are now gone from usersForPerson
            usersForPerson.IsNullOrEmptyThrowException("Person not found");
            if (usersForPerson.Count() > 1)
            {
                throw new Exception("There is more than one person for the user. This is a data error.");
            }
            return usersForPerson[0];


        }
        //public void CreatePlayerForCurrentUser()
        //{
        //    Person person = GetPersonForCurrentUser();
        //    person.IsNullThrowException("No person found for user.");
        //    ICommonWithId entity = Factory();
        //    PlayerAbstract playerAbstract = entity as PlayerAbstract;
        //    playerAbstract.IsNullThrowException("This is not a player!");

        //    playerAbstract.PersonId = person.Id;
        //    playerAbstract.Person = person;

        //    CreateEntity(playerAbstract as TEntity);



        //}

        public CashBalance GetCashBalancePerson(string personId)
        {
            CashBalance cashBal = new CashBalance();

            return cashBal;
        }




    }



}
