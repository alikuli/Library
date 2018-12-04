using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.DeliverymanCategoryNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
using AliKuli.Extentions;

namespace UowLibrary.PlayersNS.DeliverymanNS
{
    public partial class DeliverymanBiz : BusinessLayerPlayer<Deliveryman>
    {


        //readonly UserBiz _userBiz;
        readonly DeliverymanCategoryBiz _deliverymanCategoryBiz;

        public DeliverymanBiz(IRepositry<Deliveryman> entityDal, BizParameters bizParameters, DeliverymanCategoryBiz deliverymanCategoryBiz, AddressBiz addressBiz)
            : base(entityDal, bizParameters,  addressBiz)
        {

            //_userBiz = userBiz;
            _deliverymanCategoryBiz = deliverymanCategoryBiz;
        }

        public DeliverymanCategoryBiz deliverymanCategoryBiz 
        { 
            get 
            {
                _deliverymanCategoryBiz.IsNullThrowException("_deliverymanCategoryBiz");
                _deliverymanCategoryBiz.UserId = UserId;
                _deliverymanCategoryBiz.UserName = UserName;
                return _deliverymanCategoryBiz; 
            } 
        }
        //public AddressBiz AddressBiz { get { return _userBiz.AddressBiz; } }
        //public AddressVerificationHdrBiz AddressVerificationHdrBiz
        //{
        //    get
        //    {
        //        return _userBiz.AddressBiz.AddressVerificationHdrBiz;
        //    }
        //}

        //public AddressVerificationTrxBiz AddressVerificationTrxBiz
        //{
        //    get
        //    {
        //        return _userBiz.AddressBiz.AddressVerificationHdrBiz.AddressVerificationTrxBiz;
        //    }
        //}


        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        return _userBiz;
        //    }
        //}
        //public CountryBiz CountryBiz
        //{
        //    get
        //    {
        //        return UserBiz.CountryBiz;
        //    }
        //}

        //public string PakistanId
        //{
        //    get
        //    {
        //        return CountryBiz.PakistanId;
        //    }
        //}
    }
}
