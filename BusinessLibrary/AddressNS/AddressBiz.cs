using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddessWithIdNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using System;
using System.Linq;
using System.Web.Mvc;
using UowLibrary.ParametersNS;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz : BusinessLayer<AddressWithId>
    {

        //UserBiz _userBiz;
        CountryBiz _countryBiz;
        AddressVerificationTrxBiz _addressVerificationTrxBiz;
        AddressVerificationHdrBiz _addressVerificationHdrBiz;
        public AddressBiz(IRepositry<AddressWithId> entityDal, BizParameters bizParameters, CountryBiz countryBiz, AddressVerificationHdrBiz addressVerificationHdrBiz)
            : base(entityDal, bizParameters)
        {
            //_userBiz = userBiz;
            _countryBiz = countryBiz;
            _addressVerificationTrxBiz = addressVerificationHdrBiz.AddressVerificationTrxBiz;
            _addressVerificationHdrBiz = addressVerificationHdrBiz;
        }

        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        return _userBiz;
        //    }
        //}

        public CountryBiz CountryBiz
        {
            get
            {
                return _countryBiz;
            }
        }


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
                return _addressVerificationTrxBiz;
            }
        }
        public AddressVerificationHdrBiz AddressVerificationHdrBiz
        {
            get
            {
                return _addressVerificationHdrBiz;
            }
        }

        public AddressVerificationNumberVm CreateEnterVerificationNumberVm(string addressId)
        {
            addressId.IsNullOrWhiteSpaceThrowArgumentException("addressId");

            AddressWithId address = Find(addressId);
            address.IsNullThrowException("Address not found.");

            AddressVerificationNumberVm vm = new AddressVerificationNumberVm();
            vm.AddressId = address.Id;
            vm.AddressName = address.FullName();

            return vm;
        }


        //this verifies the verification code and updates the AddressVerificationTrx and AddressWithId
        public void VerifiyAddressCode(AddressVerificationNumberVm avnVM)
        {
            avnVM.IsNullThrowExceptionArgument("Address Verification not recieved");
            avnVM.VerificaitonNumber.IsNullOrWhiteSpaceThrowException("Verification is blank.");
            avnVM.AddressId.IsNullOrWhiteSpaceThrowException("Address Id not recieved");

            AddressWithId address = Find(avnVM.AddressId);
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
            UpdateAndSave(address);


        }
    }
}
