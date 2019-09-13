using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.DeliverymanCategoryNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
using AliKuli.Extentions;
using UowLibrary.CashTtxNS;
using InterfacesLibrary.SharedNS;
using System.Configuration;

namespace UowLibrary.PlayersNS.DeliverymanNS
{
    public partial class DeliverymanBiz : BusinessLayerPlayer<Deliveryman>
    {


        //readonly UserBiz _userBiz;
        readonly DeliverymanCategoryBiz _deliverymanCategoryBiz;

        public DeliverymanBiz(IRepositry<Deliveryman> entityDal, BizParameters bizParameters, DeliverymanCategoryBiz deliverymanCategoryBiz, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(entityDal, bizParameters, addressBiz, cashTrxBiz)
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



        public override ICommonWithId Factory()
        {

            Deliveryman deliveryMan = base.Factory() as Deliveryman;
        
    
            //add default values
            double costOfDeliveryPct;
            string costOfDeliveryPctStr = ConfigurationManager.AppSettings["Deliveryman.CostOfDeliveryPct"];
            bool success = double.TryParse(costOfDeliveryPctStr , out costOfDeliveryPct);
            if(success)
            {
                deliveryMan.CostOfDeliveryPct = costOfDeliveryPct;

            }



            decimal minimumDeliveryCost;
            string minimumDeliveryCostStr = ConfigurationManager.AppSettings["Deliveryman.MinimumDeliveryCost"];
            success = decimal.TryParse(minimumDeliveryCostStr, out minimumDeliveryCost);
            if (success)
            {
                deliveryMan.MinimumDeliveryCost = minimumDeliveryCost;

            }

            double maxWeightInKg;
            string maxWeightInKgStr = ConfigurationManager.AppSettings["Deliveryman.MaxWeightInKg"];
            success = double.TryParse(minimumDeliveryCostStr, out maxWeightInKg);
            if (success)
            {
                deliveryMan.MaxWeightInKg = maxWeightInKg;

            }

            return deliveryMan as ICommonWithId;

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
