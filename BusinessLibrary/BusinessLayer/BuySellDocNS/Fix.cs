using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.MenuNS.MenuStateNS;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {
        private long GetNextDocNumber()
        {
            List<BuySellDoc> lst = FindAll().ToList();
            if (lst.IsNullOrEmpty())
                return 1;
            long maxExisting = lst.Max(x => x.DocumentNumber);
            return maxExisting + 1;

        }

        public override void Fix(ControllerCreateEditParameter parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("User is not logged in");
            BuySellDoc buySellDoc = parm.Entity as BuySellDoc;
            buySellDoc.IsNullThrowException("Unable to unbox payment trx");


            fixDocumentNumber(buySellDoc);
            fixName(parm);
            fixCustomer(buySellDoc);
            fixSeller(buySellDoc);

            fixCustomerSalesman(buySellDoc);
            fixOwnerSalesman(buySellDoc);
            fixDeliverymanSalesman(buySellDoc);

            fixAddresses(buySellDoc);
            fixVehicalType(buySellDoc);
            fixFreight(buySellDoc);
            fixDate(buySellDoc);
            fixFreightOfferTrxId(buySellDoc);
            fix_Update_BuySellDocStateModifierEnum_InBuySell(parm, buySellDoc);
            fix_MenuManager(buySellDoc, parm);
            base.Fix(parm);

        }

        private void fix_MenuManager(BuySellDoc buySellDoc, ControllerCreateEditParameter parm)
        {
            if(buySellDoc.MenuManager.IsNull())
            {
                buySellDoc.MenuManager = new MenuManager(null, null, null, MenuENUM.IndexDefault, BreadCrumbManager, null, UserId, parm.ReturnUrl, UserName);
            }
        }

        private void fix_Update_BuySellDocStateModifierEnum_InBuySell(ControllerCreateEditParameter parm, BuySellDoc buySellDoc)
        {
            //Only fix the value if it has no value. This is because sometimes it gets the value from Edit, and other times
            //from Specific Methods in the Controller like CancelRejectOrder.

            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Unknown)
                buySellDoc.BuySellDocStateModifierEnum = parm.BuySellDocStateModifierEnum;


        }

        private void fixFreightOfferTrxId(BuySellDoc buySellDoc)
        {
            if (buySellDoc.FreightOfferTrxAcceptedId.IsNullOrWhiteSpace())
                buySellDoc.FreightOfferTrxAcceptedId = null;
        }
        private void fixDate(BuySellDoc buySellDoc)
        {
            DateParameter dt = new DateParameter();
            dt.BeginDate = buySellDoc.PleasePickupOnDate_Start;
            dt.EndDate = buySellDoc.PleasePickupOnDate_End;
            if (dt.BeginDateAfterEndDate)
            {
                DateTime temp = buySellDoc.PleasePickupOnDate_Start;
                buySellDoc.PleasePickupOnDate_Start = buySellDoc.PleasePickupOnDate_End;
                buySellDoc.PleasePickupOnDate_End = temp;
            }

            if (dt.EndDate == DateTime.MinValue)
            {
                dt.EndDate = DateTime.MaxValue;
            }
        }

        private static void fixFreight(BuySellDoc buySellDoc)
        {
            if (buySellDoc.FreightCustomerBudget_String.IsNullOrWhiteSpace())
            {

            }
            else
            {
                decimal frt;
                bool success = decimal.TryParse(buySellDoc.FreightCustomerBudget_String, out frt);
                if (success)
                {
                    buySellDoc.FreightCustomerBudget = frt;
                }

            }
        }

        private void fixVehicalType(BuySellDoc buySellDoc)
        {
            if (buySellDoc.VehicalTypeRequestedId.IsNullOrWhiteSpace())
            {
                buySellDoc.VehicalTypeRequestedId = null;
            }

            if (buySellDoc.VehicalTypeOfferedId.IsNullOrWhiteSpace())
            {
                buySellDoc.VehicalTypeOfferedId = null;
            }
        }

        private void fixAddresses(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateEnum != EnumLibrary.EnumNS.BuySellDocStateENUM.RequestUnconfirmed)
                return;

            string addressFoundId = "";
            if (buySellDoc.AddressShipToId.IsNullOrWhiteSpace() && (buySellDoc.AddressShipToComplex.IsNull() || buySellDoc.AddressShipToComplex.IsWhiteSpaceOrNull()))
            {
                buySellDoc.AddressShipToId = null;
            }
            else
            {
                if (isNewAddress(buySellDoc, buySellDoc.AddressShipToComplex, out addressFoundId))
                {

                    prepareTheData(buySellDoc);
                    createAndAssignNew_ShipToAddress(buySellDoc);

                }
                else
                {
                    buySellDoc.AddressShipToId = addressFoundId;
                }
            }

            addressFoundId = "";
            if (buySellDoc.AddressBillToId.IsNullOrWhiteSpace() && (buySellDoc.AddressBillToComplex.IsNull() || buySellDoc.AddressBillToComplex.IsWhiteSpaceOrNull()))
            {
                //if the BillTo address null
                buySellDoc.AddressBillToId = null;

            }
            else
            {
                //BillTo Address is not null
                if (buySellDoc.AddressBillToComplex.IsNull() || buySellDoc.AddressBillToComplex.IsWhiteSpaceOrNull())
                {
                    //if the AddressComplex is null,that means the AddressBillToId has a value. We will
                    //leave that value as is

                }
                else
                {
                    //the AddressBillToComplex has a value
                    if (buySellDoc.AddressBillToComplex.Equals(buySellDoc.AddressShipToComplex))
                    {
                        //The billTo addressComplex is the same as the shipTo Address
                        buySellDoc.AddressBillToId = buySellDoc.AddressShipToId;
                    }
                    else
                    {
                        //the billToAddress is NOT the same as the shipTo Address
                        //check to see if the BillToAddress exists in the db.
                        if (isNewAddress(buySellDoc, buySellDoc.AddressBillToComplex, out addressFoundId))
                        {
                            prepareTheData(buySellDoc);
                            createAndAssignNew_BillToAddress(buySellDoc);
                        }
                        else
                        {
                            buySellDoc.AddressBillToId = addressFoundId;
                        }

                    }

                }
            }
        }

        private void createAndAssignNew_BillToAddress(BuySellDoc buySellDoc)
        {
            AddressMain addressToSave = AddressBiz.Factory() as AddressMain;
            addressToSave.LoadFor(buySellDoc.AddressBillToComplex);
            addressToSave.Name = addressToSave.MakeUniqueName();

            if (addressToSave.BuySellDocs.IsNull())
                addressToSave.BuySellDocs = new List<BuySellDoc>();

            addressToSave.BuySellDocs.Add(buySellDoc);

            addressToSave.PersonId = buySellDoc.Customer.PersonId;
            buySellDoc.Customer.Person.Addresses.Add(addressToSave);

            buySellDoc.AddressBillToId = addressToSave.Id;
            AddressBiz.Create(addressToSave);
        }

        private void createAndAssignNew_ShipToAddress(BuySellDoc buySellDoc)
        {
            AddressMain addressToSave = AddressBiz.Factory() as AddressMain;
            addressToSave.LoadFor(buySellDoc.AddressShipToComplex);
            addressToSave.Name = addressToSave.MakeUniqueName();

            if (addressToSave.BuySellDocs.IsNull())
                addressToSave.BuySellDocs = new List<BuySellDoc>();

            addressToSave.BuySellDocs.Add(buySellDoc);

            addressToSave.PersonId = buySellDoc.Customer.PersonId;
            buySellDoc.Customer.Person.Addresses.Add(addressToSave);

            buySellDoc.AddressShipToId = addressToSave.Id;
            AddressBiz.Create(addressToSave);
        }

        private void prepareTheData(BuySellDoc buySellDoc)
        {
            buySellDoc.CustomerId.IsNullOrWhiteSpaceThrowException();
            if (buySellDoc.Customer.Person.IsNull())
            {
                Customer customer = CustomerBiz.Find(buySellDoc.CustomerId);
                customer.IsNullThrowException();
                buySellDoc.Customer = customer;
            }

            buySellDoc.Customer.PersonId.IsNullOrWhiteSpaceThrowException();
            if (buySellDoc.Customer.Person.IsNull())
            {
                Person p = PersonBiz.Find(buySellDoc.Customer.PersonId);
                p.IsNullThrowException();
                buySellDoc.Customer.Person = p;
            }

            if (buySellDoc.Customer.Person.Addresses.IsNull())
            {
                buySellDoc.Customer.Person.Addresses = new List<AddressMain>();
            }
        }

        //private void fixDeliveryMan(BuySellDoc buySellDoc)
        //{
        //    if (buySellDoc.DeliverymanId.IsNullOrWhiteSpace())
        //    {
        //        buySellDoc.DeliverymanId = null;
        //    }
        //}

        private void fixSeller(BuySellDoc buySellDoc)
        {
            if (buySellDoc.OwnerId.IsNullOrWhiteSpace())
            {
                if (buySellDoc.Owner.IsNull())
                {
                    throw new Exception("No owner found!");
                }
                buySellDoc.OwnerId = buySellDoc.Owner.Id;
            }
            else
            {
                if (buySellDoc.Owner.IsNull())
                {
                    buySellDoc.Owner = OwnerBiz.Find(buySellDoc.OwnerId);
                    buySellDoc.Owner.IsNullThrowException("Owner not found");
                }
            }

        }

        private void fixBillTo(BuySellDoc buySellDoc)
        {
            if (buySellDoc.AddressBillToId.IsNullOrWhiteSpace())
            {
                buySellDoc.AddressBillToId = null;
            }
            else
            {
                buySellDoc.AddressBillTo = AddressBiz.Find(buySellDoc.AddressBillToId);
                buySellDoc.AddressBillTo.IsNullThrowException("Bill To Address not found");
            }
        }

        //private void fixAddressShipTo(BuySellDoc buySellDoc)
        //{
        //    if (buySellDoc.AddressShipToId.IsNullOrWhiteSpace())
        //    {
        //        buySellDoc.AddressShipToId = null;
        //    }
        //    else
        //    {
        //        buySellDoc.AddressShipTo = AddressBiz.Find(buySellDoc.AddressShipToId);
        //        buySellDoc.AddressShipTo.IsNullThrowException("Ship To Address not found");
        //    }
        //}
        private void fixInformTo(BuySellDoc buySellDoc)
        {
            buySellDoc.AddressShipTo = null;
            if (buySellDoc.AddressShipToId.IsNullOrWhiteSpace())
            {
                buySellDoc.AddressShipToId = null;
            }
            else
            {
                buySellDoc.AddressShipTo = AddressBiz.Find(buySellDoc.AddressShipToId);
                buySellDoc.AddressShipTo.IsNullThrowException("Ship To Address not found");
            }
        }

        private void fixCustomer(BuySellDoc buySellDoc)
        {
            //do we want to allow empty orders??
            if (buySellDoc.CustomerId.IsNullOrWhiteSpace())
            {
                buySellDoc.CustomerId = null;
            }
            else
            {
                buySellDoc.Customer = CustomerBiz.Find(buySellDoc.CustomerId);
                buySellDoc.Customer.IsNullThrowException("Customer not found!");
            }
        }

        private void fixCustomerSalesman(BuySellDoc buySellDoc)
        {
            //do we want to allow empty orders??
            if (buySellDoc.CustomerSalesmanId.IsNullOrWhiteSpace())
            {
                buySellDoc.CustomerSalesmanId = null;
            }
            else
            {
                buySellDoc.CustomerSalesman = SalesmanBiz.Find(buySellDoc.CustomerSalesmanId);
                buySellDoc.CustomerSalesman.IsNullThrowException("Salesman not found!");
            }
        }

        private void fixDeliverymanSalesman(BuySellDoc buySellDoc)
        {
            //do we want to allow empty orders??
            if (buySellDoc.DeliverymanSalesmanId.IsNullOrWhiteSpace())
            {
                buySellDoc.DeliverymanSalesmanId = null;
            }
            else
            {
                buySellDoc.DeliverymanSalesman = SalesmanBiz.Find(buySellDoc.DeliverymanSalesmanId);
                buySellDoc.DeliverymanSalesman.IsNullThrowException("Salesman not found!");
            }
        }

        private void fixOwnerSalesman(BuySellDoc buySellDoc)
        {
            //do we want to allow empty orders??
            if (buySellDoc.OwnerSalesmanId.IsNullOrWhiteSpace())
            {
                buySellDoc.OwnerSalesmanId = null;
            }
            else
            {
                buySellDoc.OwnerSalesman = SalesmanBiz.Find(buySellDoc.OwnerSalesmanId);
                buySellDoc.OwnerSalesman.IsNullThrowException("Salesman not found!");
            }
        }

        private static void fixName(ControllerCreateEditParameter parm)
        {
            if (parm.Entity.Name.IsNullOrWhiteSpace())
            {
                parm.Entity.Name = parm.Entity.MakeUniqueName();

            }
        }

        private void fixDocumentNumber(BuySellDoc buySellDoc)
        {
            if (buySellDoc.DocumentNumber == 0)
            {
                buySellDoc.DocumentNumber = GetNextDocNumber();
            }
        }

        /// <summary>
        /// This returns the new address Id, or null.
        /// </summary>
        /// <param name="buySellDoc"></param>
        /// <param name="addressComplex"></param>
        /// <returns></returns>
        private bool isNewAddress(BuySellDoc buySellDoc, AddressComplex addressComplex, out string addressFoundId)
        {
            addressFoundId = "";
            if (addressComplex.IsNull())
                return false;

            buySellDoc.IsNullThrowException();

            Customer customer = buySellDoc.Customer;
            customer.IsNullThrowException();

            Person person = customer.Person;
            person.IsNullThrowException();

            addressComplex.ErrorCheck();
            //if there is no address... there should be an error.
            if (!addressComplex.Error.IsNullOrWhiteSpace())
            {

                throw new Exception(addressComplex.Error);
            }

            //address is complete.
            //We need to make the unique name from this so that we can check it exists in the db
            AddressMain addressToSave = AddressBiz.Factory() as AddressMain;
            addressToSave.LoadFor(addressComplex);
            addressToSave.Name = addressToSave.MakeUniqueName();

            //locate this address in Person

            //get all the addresses for the ownerPerson
            List<AddressMain> allAddressForOwnerPerson = AddressBiz.FindAll().Where(x => x.PersonId == person.Id).ToList();

            if (allAddressForOwnerPerson.IsNull())
            {
                //no addresses found. Its empty...
                //so add the new address
                return true;

            }
            else
            {
                //addresses have been found
                //we do not update old addresses, we just add new ones, if they change.
                AddressMain addressFound = allAddressForOwnerPerson.FirstOrDefault(x => x.Name.ToLower() == addressToSave.Name.ToLower());

                if (addressFound.IsNull())
                {
                    //the address was not found.
                    //AddressComplex contains a new address
                    //Add it to the person's addresses

                    return true;
                }
                else
                {
                    addressFoundId = addressFound.Id;
                }
            }

            return false;

        }

        private string addNewAddress(BuySellDoc bsd, Person person, AddressMain addressToSave)
        {
            bsd.IsNullThrowException();



            if (addressToSave.BuySellDocs.IsNull())
                addressToSave.BuySellDocs = new List<BuySellDoc>();

            addressToSave.BuySellDocs.Add(bsd);
            addressToSave.PersonId = person.Id;

            person.Addresses.Add(addressToSave);
            return addressToSave.Id;
        }

    }
}
