using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddessWithIdNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.EmailAddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz : ContactAbstractBiz<AddressMain>
    {

        //UserBiz _userBiz;
        //PersonBiz _personBiz;
        CountryBiz _countryBiz;
        AddressVerificationTrxBiz _addressVerificationTrxBiz;
        AddressVerificationHdrBiz _addressVerificationHdrBiz;
        public AddressBiz(IRepositry<AddressMain> entityDal, BizParameters bizParameters, AddressVerificationHdrBiz addressVerificationHdrBiz, PersonBiz personBiz, CountryBiz countryBiz)
            : base(entityDal, bizParameters, personBiz, countryBiz)
        {
            //_personBiz = personBiz;
            _countryBiz = countryBiz;
            _addressVerificationTrxBiz = addressVerificationHdrBiz.AddressVerificationTrxBiz;
            _addressVerificationHdrBiz = addressVerificationHdrBiz;

        }

        //public PersonBiz PersonBiz 
        //{ 
        //    get 
        //    {
        //        _personBiz.IsNullThrowException();
        //        _personBiz.UserId = UserId;
        //        _personBiz.UserName = UserName;
        //        return _personBiz;
        //    } 
        //}

        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        _personBiz.UserBiz.IsNullThrowException();
        //        _personBiz.UserBiz.UserId = UserId;
        //        _personBiz.UserBiz.UserName = UserName;
        //        return _personBiz.UserBiz;
        //    }
        //}

        //public PersonBiz PersonBiz { get { return UserBiz.PersonBiz; } }

        //public CountryBiz CountryBiz
        //{
        //    get
        //    {

        //        return _countryBiz;
        //    }
        //}
        public SelectList CountrySelectList
        {
            get
            {
                return CountryBiz.SelectList();
            }
        }


        public AddressVerificationTrxBiz AddressVerificationTrxBiz
        {
            get
            {
                _addressVerificationTrxBiz.IsNullThrowException();
                _addressVerificationTrxBiz.UserId = UserId;
                _addressVerificationTrxBiz.UserName = UserName;
                return _addressVerificationTrxBiz;
            }
        }
        public AddressVerificationHdrBiz AddressVerificationHdrBiz
        {
            get
            {
                _addressVerificationHdrBiz.IsNullThrowException();
                _addressVerificationHdrBiz.UserId = UserId;
                _addressVerificationHdrBiz.UserName = UserName;
                return _addressVerificationHdrBiz;
            }
        }


        public AddressVerificationNumberVm CreateEnterVerificationNumberVm(string addressId)
        {
            addressId.IsNullOrWhiteSpaceThrowArgumentException("addressId");

            AddressMain address = Find(addressId);
            address.IsNullThrowException("Address not found.");

            AddressVerificationNumberVm vm = new AddressVerificationNumberVm();
            vm.AddressId = address.Id;
            vm.AddressName = address.FullName();

            return vm;
        }


        //this verifies the verification code and updates the AddressVerificationTrx and AddressWithId
        public void VerifiyAddressCode(AddressVerificationNumberVm avnVM, GlobalObject globalObject)
        {
            avnVM.IsNullThrowExceptionArgument("Address Verification not recieved");
            avnVM.VerificaitonNumber.IsNullOrWhiteSpaceThrowException("Verification is blank.");
            avnVM.AddressId.IsNullOrWhiteSpaceThrowException("Address Id not recieved");

            AddressMain address = Find(avnVM.AddressId);
            address.IsNullThrowException("Address not found");

            long lngVerificationNumber;
            bool success = long.TryParse(avnVM.VerificaitonNumber, out lngVerificationNumber);

            if (!success)
                throw new Exception("Unable to read the verification number");

            if (address.Verification.VerificationNumber != lngVerificationNumber)
                throw new Exception("Verification number does not match!");

            AddressVerificationTrx addressVerificationTrx = address
                .AddressVerificationTrxs
                .FirstOrDefault(x => x.Verification.VerificaionStatusEnum == EnumLibrary.EnumNS.VerificaionStatusENUM.Mailed);

            addressVerificationTrx.IsNullThrowException("Unable to locate Address Verification Trx");

            address.Verification.VerificaionStatusEnum = VerificaionStatusENUM.Verified;
            addressVerificationTrx.Verification.VerificaionStatusEnum = VerificaionStatusENUM.Verified;

            _addressVerificationTrxBiz.Update(addressVerificationTrx);


            ControllerCreateEditParameter param = new ControllerCreateEditParameter();
            param.Entity = address as ICommonWithId;
            param.GlobalObject = globalObject;


            UpdateAndSave(param);


        }



        [Authorize(Roles = "Administrator")]
        public async Task ResetAddressVerificationComplete(GlobalObject globalObject)
        {
            //delete all the AddressVerificationTrx
            await AddressVerificationTrxBiz.DeleteActuallyAllAndSaveAsync();
            //delete all the AddressVerificationHdr
            await AddressVerificationHdrBiz.DeleteActuallyAllAndSaveAsync();

            //Reset the Administrators addresses
            SetAllAddressesToNotVerified(globalObject);
            ErrorsGlobal.AddMessage("Addresses Verification Reset!");
        }


        public bool IsAddressInPakistan(string addressId)
        {
            addressId.IsNullOrWhiteSpaceThrowArgumentException("addressId");
            AddressMain addy = Find(addressId);
            addy.IsNullThrowException("Address not found.");
            //todo
            //addy.CountryId.IsNullOrWhiteSpaceThrowException("Country Id is empty in Address");

            //bool IsInPakistan = CountryBiz.IsAddressInPakistan(addy.CountryId);
            //return IsInPakistan;
            throw new NotImplementedException();

        }


    }
}
