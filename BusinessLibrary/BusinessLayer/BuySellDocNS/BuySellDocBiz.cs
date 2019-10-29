using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.VehicalTypeNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.AddressNS;
using UowLibrary.BusinessLayer.ProductNS.ShopNS;
using UowLibrary.FreightOffersTrxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.DeliverymanNS;
using UowLibrary.PlayersNS.MessageNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.SalesmanCategoryNS;
using UowLibrary.PlayersNS.SalesmanNS;
using UowLibrary.PlayersNS.VehicalTypeNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
            : BusinessLayer<BuySellDoc>
    {
        OwnerBiz _ownerBiz;
        CustomerBiz _customerBiz;
        ShopBiz _shopBiz;
        BuySellDocBiz _buySellBiz;
        BuySellItemBiz _buySellItemBiz;
        DeliverymanBiz _deliverymanBiz;
        FreightOfferTrxBiz _freightOfferTrxBiz;
        VehicalTypeBiz _vehicalTypeBiz;
        MessageBiz _messageBiz;
        PeopleMessageBiz _peopleMessageBiz;
        SalesmanBiz _salesmanBiz;
        BuySellDocHistoryBiz _buySellDocHistoryBiz;
        public BuySellDocBiz(IRepositry<BuySellDoc> entityDal, BuySellItemBiz buySellItemBiz, BizParameters bizParameters, OwnerBiz ownerBiz, CustomerBiz customerBiz, ShopBiz shopBiz, DeliverymanBiz deliverymanBiz, FreightOfferTrxBiz freightOfferTrxBiz, VehicalTypeBiz vehicalTypeBiz, MessageBiz messageBiz, PeopleMessageBiz peopleMessageBiz, SalesmanBiz salesmanBiz, BuySellDocHistoryBiz buySellDocHistoryBiz )
            : base(entityDal, bizParameters)
        {
            _ownerBiz = ownerBiz;
            _customerBiz = customerBiz;
            _shopBiz = shopBiz;
            _buySellBiz = entityDal as BuySellDocBiz;
            _buySellItemBiz = buySellItemBiz;
            _freightOfferTrxBiz = freightOfferTrxBiz;
            _deliverymanBiz = deliverymanBiz;
            _vehicalTypeBiz = vehicalTypeBiz;
            _messageBiz = messageBiz;
            _peopleMessageBiz = peopleMessageBiz;
            _salesmanBiz = salesmanBiz;
            _buySellDocHistoryBiz = buySellDocHistoryBiz;
        }



        public BuySellDocHistoryBiz BuySellDocHistoryBiz
        {
            get
            {
                _buySellDocHistoryBiz.UserId = UserId;
                _buySellDocHistoryBiz.UserName = UserName;
                return _buySellDocHistoryBiz;
            }
        }

        public SalesmanCategoryBiz SalesmanCategoryBiz
        {
            get
            {
                return SalesmanBiz.SalesmanCategoryBiz;
            }
        }
        public SalesmanBiz SalesmanBiz
        {
            get
            {
                _salesmanBiz.UserId = UserId;
                _salesmanBiz.UserName = UserName;
                return _salesmanBiz;
            }
        }

        public PeopleMessageBiz PeopleMessageBiz
        {
            get
            {
                _peopleMessageBiz.IsNullThrowException("peopleMessageBiz");
                _peopleMessageBiz.UserId = UserId;
                _peopleMessageBiz.UserName = UserName;
                return _peopleMessageBiz;
            }
        }

        public MessageBiz MessageBiz
        {
            get
            {
                _messageBiz.IsNullThrowException("messageBiz");
                _messageBiz.UserId = UserId;
                _messageBiz.UserName = UserName;
                return _messageBiz;
            }
        }

        public VehicalTypeBiz VehicalTypeBiz
        {
            get
            {
                _vehicalTypeBiz.IsNullThrowException("vehicalTypeBiz");
                _vehicalTypeBiz.UserId = UserId;
                _vehicalTypeBiz.UserName = UserName;
                return _vehicalTypeBiz;

            }
        }
        public FreightOfferTrxBiz FreightOfferTrxBiz
        {
            get
            {
                _freightOfferTrxBiz.IsNullThrowException("freightOfferTrxBiz");
                _freightOfferTrxBiz.UserId = UserId;
                _freightOfferTrxBiz.UserName = UserName;
                return _freightOfferTrxBiz;

            }
        }
        public DeliverymanBiz DeliverymanBiz
        {
            get
            {
                _deliverymanBiz.IsNullThrowException("deliverymanBiz");
                _deliverymanBiz.UserId = UserId;
                _deliverymanBiz.UserName = UserName;
                return _deliverymanBiz;

            }
        }
        public BuySellItemBiz BuySellItemBiz
        {
            get
            {
                _buySellItemBiz.IsNullThrowException("_buySellItemBiz");
                _buySellItemBiz.UserId = UserId;
                _buySellItemBiz.UserName = UserName;
                return _buySellItemBiz;
            }

        }
        public ProductBiz ProductBiz
        {
            get
            {

                return ShopBiz.ProductBiz;
            }
        }

        public ShopBiz ShopBiz
        {
            get
            {
                _shopBiz.IsNullThrowException("_shopBiz");
                _shopBiz.UserId = UserId;
                _shopBiz.UserName = UserName;
                return _shopBiz;
            }
        }

        public ProductChildBiz ProductChildBiz
        {
            get
            {
                return ProductBiz.ProductChildBiz;
            }
        }
        public AddressBiz AddressBiz
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




        public void GetDefaultVehicalType(BuySellDoc buySellDoc)
        {
            if (buySellDoc.VehicalTypeRequestedId.IsNullOrWhiteSpace())
            {
                buySellDoc.VehicalTypeRequested = VehicalTypeBiz.FindByName("Any Vehical Type");
                buySellDoc.VehicalTypeRequested.IsNullThrowException();
                buySellDoc.VehicalTypeRequestedId = buySellDoc.VehicalTypeRequested.Id; ;
            }
        }




        #region Move to BuySellBiz
        public void RejectOrder_Code(string id, BuySellDocumentTypeENUM buySellDocumentTypeEnum, GlobalObject globalObject)
        {
            id.IsNullOrWhiteSpaceThrowException();
            //returnUrl.IsNullOrWhiteSpaceThrowException();

            BuySellDoc buySellDoc = Find(id);
            buySellDoc.IsNullThrowException();
            buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.Rejected;

            ControllerCreateEditParameter param = new ControllerCreateEditParameter();
            param.Entity = buySellDoc as ICommonWithId;
            param.GlobalObject = globalObject;

            UpdateAndSave(param);

        }
        public static void CreateHeadingForCreateForm(ControllerIndexParams parm)
        {
            BuySellDoc buySellDoc = UnboxBuySell(parm);
            buySellDoc.HeadingForCreateForm = parm.BuySellDocumentTypeEnum.ToString().ToTitleSentance();
        }

        public static void Load_DocumentType_Into_BuySell(ControllerIndexParams parm)
        {
            BuySellDoc buySellDoc = UnboxBuySell(parm);
            buySellDoc.BuySellDocumentTypeEnum = parm.BuySellDocumentTypeEnum;
        }


        public static void Make_Heading_For_Create_Form(ControllerIndexParams parm)
        {
            BuySellDoc buySellDoc = UnboxBuySell(parm);
            buySellDoc.HeadingForCreateForm = parm.BuySellDocumentTypeEnum.ToString().ToTitleSentance();
        }

        private static void load_Freight_Request_Into_String_Field(BuySellDoc buySellDoc)
        {
            buySellDoc.FreightCustomerBudget_String = buySellDoc.FreightCustomerBudget.ToString("N2");
        }

        private void load_Deliverymen_SelectList_Into_BuySellDoc(BuySellDoc buySellDoc)
        {
            buySellDoc.SelectListDeliveryman = DeliverymanBiz.SelectList();
        }

        public static BuySellDoc UnboxBuySell(ControllerIndexParams parm)
        {
            BuySellDoc buySellDoc = parm.Entity as BuySellDoc;
            buySellDoc.IsNullThrowException("Unable to unbox buySellDoc");
            return buySellDoc;
        }


        public static void Load_DocStateAndType_Into_Items(BuySellDoc buySellDoc)
        {
            //add the document type and document type to items. these are not presisted

            if (buySellDoc.BuySellItems.Any(x => x.MetaData.IsDeleted == false))
            {
                List<BuySellItem> itemLst = buySellDoc.BuySellItems.Where(x => x.MetaData.IsDeleted == false).ToList();
                foreach (BuySellItem item in itemLst)
                {
                    item.BuySellDocStateEnum = buySellDoc.BuySellDocStateEnum;
                    item.BuySellDocumentTypeEnum = buySellDoc.BuySellDocumentTypeEnum;
                }
            }
        }


        public static void AddDefaultValuesToPleasePickupDate(BuySellDoc buySellDoc)
        {

            if (buySellDoc.PleasePickupOnDate_Start == DateTime.MinValue)
                buySellDoc.PleasePickupOnDate_Start = DateTime.Now;

            if (buySellDoc.PleasePickupOnDate_End == DateTime.MinValue)
                buySellDoc.PleasePickupOnDate_End = DateTime.Now.AddDays(15);

        }


        private Deliveryman loadCurrentUsersDeliverymanIdIntoBuySellDoc(BuySellDoc buySellDoc)
        {
            //get the deliveryman Id for the current user.
            Deliveryman deliveryman = DeliverymanBiz.GetPlayerFor(UserId);
            deliveryman.IsNullThrowException();

            //This is used in View OrderList to determine if the current user is the deliveryman
            //who is to accept the courier order request.
            buySellDoc.CurrentUser_DeliverymanId = deliveryman.Id;
            return deliveryman;
        }


        public static void FixFreightOfferAndCustomerBudeget(BuySellDoc buySellDoc)
        {
            buySellDoc.FreightOffer = string.Format("{0:N0}", buySellDoc.Freight_Accepted_Refundable);
            buySellDoc.FreightCustomerBudget_String = string.Format("{0:N0}", buySellDoc.FreightCustomerBudget);
        }

        public static void LoadPickupDates(BuySellDoc buySellDoc)
        {
            if (buySellDoc.PleasePickupOnDate_Start == DateTime.MaxValue || buySellDoc.PleasePickupOnDate_Start == DateTime.MinValue || buySellDoc.PleasePickupOnDate_Start == null)
            {
                buySellDoc.PleasePickupOnDate_Start = DateTime.Now.AddHours(3);
                buySellDoc.PleasePickupOnDate_End = buySellDoc.PleasePickupOnDate_Start.AddDays(7);
            }
            buySellDoc.OfferedPickupOnDate = buySellDoc.PleasePickupOnDate_Start;
        }

        public void Update_VehicalType_Offered(BuySellDoc buySellDoc)
        {
            if (buySellDoc.VehicalTypeOfferedId.IsNullOrWhiteSpace())
            {
                buySellDoc.VehicalTypeOfferedId = buySellDoc.VehicalTypeRequestedId;

            }
        }

        public static void Update_Delyman_Defaults_With_FreightOffer_From_CustomersRequestOffer(BuySellDoc buySellDoc)
        {
            if (buySellDoc.FreightOffer == "0" || buySellDoc.FreightOffer.IsNullOrWhiteSpace())
            {
                buySellDoc.FreightOffer = buySellDoc.FreightCustomerBudget.ToString("N2");
            }
        }


        public void FillAddressShipFromComplex(BuySellDoc buySellDoc)
        {
            if (buySellDoc.AddressShipFrom.IsNull())
            {
                if (buySellDoc.AddressShipFromId.IsNullOrWhiteSpace())
                {
                    buySellDoc.AddressShipFromComplex = new AddressComplex();
                    return;
                }
                buySellDoc.AddressShipFrom = AddressBiz.Find(buySellDoc.AddressShipFromId);
                buySellDoc.AddressShipFrom.IsNullThrowException();

            }
            buySellDoc.AddressShipFromComplex = buySellDoc.AddressShipFrom.ToAddressComplex();
        }

        public void FillAddressShipToComplex(BuySellDoc buySellDoc)
        {
            if (buySellDoc.AddressShipTo.IsNull())
            {
                if (buySellDoc.AddressShipToId.IsNullOrWhiteSpace())
                {
                    buySellDoc.AddressShipToComplex = new AddressComplex();
                    return;
                }
                buySellDoc.AddressShipTo = AddressBiz.Find(buySellDoc.AddressShipToId);
                buySellDoc.AddressShipTo.IsNullThrowException();
            }
            buySellDoc.AddressShipToComplex = buySellDoc.AddressShipTo.ToAddressComplex();

        }
        public void fillAddressBillToComplex(BuySellDoc buySellDoc)
        {
            if (buySellDoc.AddressBillTo.IsNull())
            {
                if (buySellDoc.AddressBillToId.IsNullOrWhiteSpace())
                {
                    buySellDoc.AddressBillToComplex = new AddressComplex();
                    return;
                }
                buySellDoc.AddressBillTo = AddressBiz.Find(buySellDoc.AddressBillToId);
                buySellDoc.AddressBillTo.IsNullThrowException();
            }

            buySellDoc.AddressBillToComplex = buySellDoc.AddressBillTo.ToAddressComplex();
        }

        public void Load_Current_User_As_Delyman_Id_Into_BuySellDoc_And_UpdateVarsWithEarlierOffer(BuySellDoc buySellDoc)
        {
            //check to see if the user has bid on this before. If yes, then find it and add the values.
            Deliveryman deliveryman = loadCurrentUsersDeliverymanIdIntoBuySellDoc(buySellDoc);

            FreightOfferTrx earlierOfferByUser = FreightOfferTrxBiz.FindAll().FirstOrDefault(x => x.BuySellDocId == buySellDoc.Id && x.DeliverymanId == deliveryman.Id);

            if (earlierOfferByUser.IsNull())
            {
            }
            else
            {
                //an earlier bid has been found
                buySellDoc.UpdateFreightBidVars(earlierOfferByUser);
            }
        }


        public void load_ShipAddress_SelectList_Into_BuySellDoc(BuySellDoc buySellDoc)
        {
            buySellDoc.Customer.IsNullThrowException();
            buySellDoc.SelectListAddressBillTo = AddressBiz.SelectListShipAddressForPerson(buySellDoc.Customer.PersonId);
        }
        public void load_VehicalTypeOffered_SelectList_Into_BuySellDoc(BuySellDoc buySellDoc)
        {
            buySellDoc.SelectListVehicalTypeOffered = VehicalTypeBiz.SelectList();
        }

        public void load_VehicalTypeRequested_SelectList_Into_BuySellDoc(BuySellDoc buySellDoc)
        {
            buySellDoc.SelectListVehicalTypeRequested = VehicalTypeBiz.SelectList();
        }


        public void LoadSelectListsFor_GET(BuySellDoc buySellDoc)
        {
            buySellDoc.SelectListCustomer = CustomerBiz.SelectList();
            buySellDoc.SelectListOwner = OwnerBiz.SelectList();
            buySellDoc.SelectListAddressInformTo = AddressBiz.SelectListInformAddressFor(UserId);
            buySellDoc.SelectListAddressShipTo = AddressBiz.SelectListShipAddressFor(UserId);
            buySellDoc.SelectListVehicalTypeAccepted = VehicalTypeBiz.SelectList();

            load_VehicalTypeRequested_SelectList_Into_BuySellDoc(buySellDoc);
            load_VehicalTypeOffered_SelectList_Into_BuySellDoc(buySellDoc);
            load_Deliverymen_SelectList_Into_BuySellDoc(buySellDoc);
            load_Freight_Request_Into_String_Field(buySellDoc);
            load_ShipAddress_SelectList_Into_BuySellDoc(buySellDoc);
            load_Salesmen_SelectList_Into_BuySellDoc(buySellDoc);

        }

        private void load_Salesmen_SelectList_Into_BuySellDoc(BuySellDoc buySellDoc)
        {
            buySellDoc.SelectListCustomerSalesman = SalesmanBiz.SelectList(); ;
            buySellDoc.SelectListOwnerSalesman = SalesmanBiz.SelectList(); ;
            buySellDoc.SelectListDeliverymanSalesman = SalesmanBiz.SelectList(); ;
        }


        public void AddDefaultValuesToDelivrymanOfferVariables(BuySellDoc buySellDoc)
        {
            DateParameter dp = new DateParameter();
            dp.BeginDate = buySellDoc.PleasePickupOnDate_Start;
            dp.EndDate = buySellDoc.PleasePickupOnDate_End;

            if (IsWithinDate(buySellDoc))
            {
                if (buySellDoc.OfferedPickupOnDate == DateTime.MinValue || buySellDoc.OfferedPickupOnDate == DateTime.MaxValue)
                {
                    buySellDoc.OfferedPickupOnDate = DateTime.Now;
                }

                if (buySellDoc.FreightOffer.IsNullOrWhiteSpace())
                {
                    buySellDoc.FreightOffer = buySellDoc.FreightCustomerBudget_String;
                }
                else
                {
                    if (buySellDoc.FreightOfferDecimal == 0)
                    {
                        buySellDoc.FreightOffer = buySellDoc.FreightCustomerBudget_String;
                    }
                }

            }
        }

        public bool IsWithinDate(BuySellDoc buySellDoc)
        {

            DateParameter dp = new DateParameter();
            dp.BeginDate = buySellDoc.PleasePickupOnDate_Start;
            dp.EndDate = buySellDoc.PleasePickupOnDate_End;
            if (dp.IsDateWithinBeginAndEndDatesInclusive(DateTime.Now))
            {
                return true;
            }

            return false;
        }

        private string not_Within_Shipping_Date_Exception_Or_Message_String(BuySellDoc buySellDoc)
        {
            string err = "Error. Document is not known";
            if (!IsWithinDate(buySellDoc))
            {
                DateParameter dp = new DateParameter();
                dp.BeginDate = buySellDoc.PleasePickupOnDate_Start;
                dp.EndDate = buySellDoc.PleasePickupOnDate_End;

                string beginDate = dp.BeginDate.ToShortDateString();
                string endDate = dp.EndDate.ToShortDateString();
                string todaysDate = DateTime.Now.ToShortDateString();
                string customerPhoneNumber = getCustomerPhoneNumber(buySellDoc);
                string customerName = buySellDoc.Customer.FullName();

                switch (buySellDoc.BuySellDocumentTypeEnum)
                {
                    case BuySellDocumentTypeENUM.Unknown:
                        break;

                    case BuySellDocumentTypeENUM.Purchase:
                        err = string.Format("The shipping window was between {0} and {1}. Today is {2} which is not within the window. {3}, please amend the shipping dates.",
                            beginDate,
                            endDate,
                            todaysDate,
                            customerName);
                        break;

                    case BuySellDocumentTypeENUM.Sale:
                    case BuySellDocumentTypeENUM.Delivery:
                    default:
                        err = string.Format("The shipping window was between {0} and {1}. Today is {2} which is not within the window. Customer to amend the shipping dates. Customer phone number is {3}",
                            beginDate,
                            endDate,
                            todaysDate,
                            customerPhoneNumber);
                        break;
                }

            }
            return err;
        }

        private void not_Within_Shipping_Window_Throw_Exception(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                return;

            if (IsWithinDate(buySellDoc))
            {
                return;
            }
            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Unknown)
                throw new Exception(not_Within_Shipping_Date_Exception_Or_Message_String(buySellDoc));
        }



        private void not_Within_Shipping_Window_Throw_Message(BuySellDoc buySellDoc)
        {
            if (buySellDoc.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
            {
                if (IsWithinDate(buySellDoc))
                {
                    return;
                }
                ErrorsGlobal.AddMessage(not_Within_Shipping_Date_Exception_Or_Message_String(buySellDoc));
            }
        }


        private static string getCustomerPhoneNumber(BuySellDoc buySellDoc)
        {
            string customerPhoneNumber = "No Phone";
            if (buySellDoc.Customer.DefaultBillAddress.IsNull())
            { }
            else
            {
                if (buySellDoc.Customer.DefaultBillAddress.Phone.IsNullOrEmpty())
                { }
                else
                {
                    customerPhoneNumber = buySellDoc.Customer.DefaultBillAddress.Phone;
                }
            }
            return customerPhoneNumber;
        }





        public void AddDefaultVehicalType(BuySellDoc buySellDoc)
        {
            if (buySellDoc.VehicalTypeRequestedId.IsNullOrWhiteSpace())
            {
                VehicalType vt = VehicalTypeBiz.FindByName("Any Vehical Type");
                vt.IsNullThrowException();
                buySellDoc.VehicalTypeRequestedId = vt.Id;
            }
        }


        public void AddPickupAddress(BuySellDoc buySellDoc)
        {
            //add the PickUp From Address to the Complex
            buySellDoc.AddressShipFromId.IsNullOrWhiteSpaceThrowException("AddressShipFromId");
            AddressMain addressShipFrom = AddressBiz.Find(buySellDoc.AddressShipFromId);
            addressShipFrom.IsNullThrowException("addressShipFrom");

            buySellDoc.AddressShipFromString = addressShipFrom.AddressWithoutContacts();
            buySellDoc.AddressShipFromComplex = addressShipFrom.ToAddressComplex();
        }



        //public BuySellStatementModel GetOrdersList(BuySellDocumentTypeENUM buySellDocumentTypeEnum, BuySellDocStateENUM buySellDocStateEnum, bool isAdmin)
        //{
        //    UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
        //    DateTime fromDate = DateTime.Now.AddMonths(-3);
        //    DateTime toDate = DateTime.Now;
        //    BuySellStatementModel buySellStatementModel = GetBuySellStatementModel(UserId, fromDate, toDate, isAdmin, buySellDocumentTypeEnum, buySellDocStateEnum);
        //    return buySellStatementModel;
        //}




        #endregion












    }


}


