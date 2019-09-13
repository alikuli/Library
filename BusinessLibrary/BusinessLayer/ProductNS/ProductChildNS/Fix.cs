using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UowLibrary.AddressNS;
namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz
    {
        /// <summary>
        /// Every Owner can add only One product of a specific name.
        /// We need to load the product in this at a higher level because we are unable to access ProductBiz here
        /// </summary>
        /// <param name="parm"></param>
        public override void Fix(ControllerCreateEditParameter parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");

            ProductChild pc = parm.Entity as ProductChild;
            pc.IsNullThrowException("Unable to unbox Product Child");

            //Product comes from the controller.
            pc.ProductId.IsNullOrWhiteSpaceThrowException("There is no parent Product");
            pc.Product.IsNullThrowException("Product Has not been loaded!");


            if (pc.Name.IsNullOrWhiteSpace())
                pc.Name = pc.Product.Name;

            //we need to load the owner in because the name is used to create
            //a directory for the uploads.
            //first get the Owners Id
            Owner owner = OwnerBiz.GetOwnerForUser(UserId);
            owner.IsNullThrowException("Owner not found!");
            pc.OwnerId = owner.Id;
            pc.Owner = owner;

            ////I dont want to add the features. I will add them temporarily when it is being presented,
            ////addProductFeatures(pc);
            FixProductChildFeatures(pc);

            base.Fix(parm);

            //check the address. If the address is a new address, make it past of the database for the ProductChild Owner
            //check if the address exists

            AddShippingAddressToOwnerPersonIfItIsNew(pc);
            //AddPhoneNumberToOwnerPersonIfItsNew(pc);
        }

        private void AddPhoneNumberToOwnerPersonIfItsNew(ProductChild pc)
        {
            Owner productChildOwner = pc.Owner;
            productChildOwner.IsNullThrowException();
            Person ownerPerson = productChildOwner.Person;
            ownerPerson.IsNullThrowException();

            //check if a phone number has been entered
            pc.ShipFromAddressComplex.Phone.IsNullOrWhiteSpace();
        }

        private void AddShippingAddressToOwnerPersonIfItIsNew(ProductChild pc)
        {
            Owner productChildOwner = pc.Owner;
            productChildOwner.IsNullThrowException();
            Person ownerPerson = productChildOwner.Person;
            ownerPerson.IsNullThrowException();
            AddressBiz addressBiz = OwnerBiz.AddressBiz;

            //get the address from the productChild
            AddressComplex shipFromAddressComplexInProductChild = pc.ShipFromAddressComplex; 
            shipFromAddressComplexInProductChild.ErrorCheck();
            
            //if there is no address... there should be an error.
            if(!shipFromAddressComplexInProductChild.Error.IsNullOrWhiteSpace())
            {
                throw new Exception("You need to add the address of where the product is sitting");
            }
            
            //address is complete.
            //We need to make the unique name from this so that we can check it exists in the db
            AddressMain addressToSave = addressBiz.Factory() as AddressMain;
            addressToSave.LoadFor(shipFromAddressComplexInProductChild);
            addressToSave.Name = addressToSave.MakeUniqueName();
            //locate this address in Person
            
            //get all the addresses for the ownerPerson
            List<AddressMain> allAddressForOwnerPerson = addressBiz.FindAll().Where(x => x.PersonId == ownerPerson.Id).ToList();

            if(allAddressForOwnerPerson.IsNull())
            {
                //no addresses found. Its empty...
                //so add the new address
                addNewAddress(pc, ownerPerson, addressBiz, addressToSave);

            }
            {
                //addresses have been found
                //we do not update old addresses, we just add new ones, if they change.
                AddressMain addressFound = allAddressForOwnerPerson.FirstOrDefault(x => x.Name.ToLower() == addressToSave.Name.ToLower());
                
                if(addressFound.IsNull())
                {
                    //the address was not found.
                    //AddressComplex contains a new address
                    //Add it to the person's addresses

                    addNewAddress(pc, ownerPerson, addressBiz, addressToSave);
                }
                //else
                //{
                //    updateAddress(pc, addressBiz, addressToSave);
                //}
            }
            
            //otherwise do nothing
        }

        private void addNewAddress(ICommonWithId entity, Person person, AddressBiz addressBiz, AddressMain addressToSave)
        {
            ProductChild pc = entity as ProductChild;
            pc.IsNullThrowException();

            pc.ShipFromAddressId = addressToSave.Id;

            if (addressToSave.ProductChilds.IsNull())
                addressToSave.ProductChilds = new List<ProductChild>();

            addressToSave.ProductChilds.Add(pc);

            person.Addresses.Add(addressToSave);
            addressToSave.PersonId = person.Id;
            addressBiz.Create(addressToSave);
        }


        private void updateAddress(ProductChild pc, AddressBiz addressBiz, AddressMain addressToSave)
        {
            pc.ShipFromAddressId = addressToSave.Id;
            addressBiz.Update(addressToSave);
        }

        private static void addNewAddress(ProductChild pc, Person ownerPerson, AddressBiz addressBiz, AddressMain addressToSave)
        {
            pc.ShipFromAddressId = addressToSave.Id;

            if (addressToSave.ProductChilds.IsNull())
                addressToSave.ProductChilds = new List<ProductChild>();

            addressToSave.ProductChilds.Add(pc);

            ownerPerson.Addresses.Add(addressToSave);
            addressToSave.PersonId = ownerPerson.Id;
            addressBiz.Create(addressToSave);
        }


        public override IQueryable<ProductChild> GetDataToCheckDuplicateName(ProductChild productChild)
        {
            var data = base.GetDataToCheckDuplicateName(productChild);

            string ownerId = productChild.OwnerId;
            ownerId.IsNullOrWhiteSpaceThrowException("OwnerId");

            var dataForOwner = data.Where(x => x.OwnerId == ownerId &&
                x.IdentificationNumber == productChild.IdentificationNumber &&
                x.SerialNumber == productChild.SerialNumber);

            return dataForOwner;
        }




        public void LogPersonsVisit(string UserId, ProductChild productChild)
        {
            UserId.IsNullOrWhiteSpaceThrowArgumentException("UserId");
            Person person = UserBiz.GetPersonFor(UserId);
            person.IsNullThrowException("Person for user not found");

            //if (productChild.VisitorPeople.IsNull())
            //    productChild.VisitorPeople = new List<Person>();


            //if (person.VisitedProductChildren.IsNull())
            //    person.VisitedProductChildren = new List<ProductChild>();

            //productChild.VisitorPeople.Add(person);
            //person.VisitedProductChildren.Add(productChild);
            SaveChanges();

        }



        public override void ErrorCheck(ControllerCreateEditParameter parm)
        {

            base.ErrorCheck(parm);

            ProductChild productChild = parm.Entity as ProductChild;
            
            //address should exist.
            productChild.ShipFromAddressComplex.ErrorCheck();

            if (!productChild.ShipFromAddressComplex.Error.IsNullOrWhiteSpace())
                throw new Exception(productChild.ShipFromAddressComplex.Error);
        }


        //public override IQueryable<TEntity> GetDataToCheckDuplicateName(TEntity entity)
        //{
        //    IQueryable<TEntity> dataSet = FindAll().Where(x => x.Id != entity.Id);
        //    return dataSet;
        //}

        
    }


}
