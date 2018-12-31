using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySell;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
            : BusinessLayer<BuySellDoc>
    {
        OwnerBiz _ownerBiz;
        CustomerBiz _customerBiz;
        public BuySellDocBiz(IRepositry<BuySellDoc> entityDal, BizParameters bizParameters, OwnerBiz ownerBiz, CustomerBiz customerBiz)
            : base(entityDal, bizParameters)
        {
            _ownerBiz = ownerBiz;
            _customerBiz = customerBiz;
        }

        AddressBiz AddressBiz
        {
            get
            {
                return OwnerBiz.AddressBiz;
            }
        }

        public CustomerBiz CustomerBiz
        {
            get
            {
                _customerBiz.IsNullThrowException("_customerBiz");
                _customerBiz.UserId = UserId;
                _customerBiz.UserName = UserName;
                return _customerBiz;
            }
        }

        public OwnerBiz OwnerBiz
        {
            get
            {
                _ownerBiz.IsNullThrowException("_ownerBiz");
                _ownerBiz.UserId = UserId;
                _ownerBiz.UserName = UserName;
                return _ownerBiz;
            }
        }
        public PersonBiz PersonBiz
        {
            get
            {

                return OwnerBiz.PersonBiz;

            }
        }

        public UserBiz UserBiz
        {
            get
            {
                return PersonBiz.UserBiz;
            }

        }
        public long GetNextDocNumber()
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


            if (buySellDoc.DocumentNumber == 0)
            {
                buySellDoc.DocumentNumber = GetNextDocNumber();
            }

            if (parm.Entity.Name.IsNullOrWhiteSpace())
            {
                parm.Entity.Name = parm.Entity.MakeUniqueName();

            }

            if (buySellDoc.OwnerId.IsNullOrEmpty())
            {
                Person person = PersonBiz.GetPersonForUserId(UserId);
                person.IsNullThrowException("PersonFrom");
                //now get the Owner
                Owner owner = OwnerBiz.GetOwnerForUser(UserId);
                owner.IsNullThrowException("No owner for this Person!");
                buySellDoc.OwnerId = owner.Id; ;
            }

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


            if (buySellDoc.AddressShipToId.IsNullOrWhiteSpace())
            {
                buySellDoc.AddressShipToId = null;
            }
            else
            {
                buySellDoc.AddressShipTo = AddressBiz.Find(buySellDoc.AddressShipToId);
                buySellDoc.AddressShipTo.IsNullThrowException("Ship To Address not found");
            }

            if (buySellDoc.AddressInformToId.IsNullOrWhiteSpace())
            {
                buySellDoc.AddressInformToId = null;
            }
            else
            {
                buySellDoc.AddressInformTo = AddressBiz.Find(buySellDoc.AddressInformToId);
                buySellDoc.AddressInformTo.IsNullThrowException("Inform To Address not found");
            }
            base.Fix(parm);

        }




    }
}
