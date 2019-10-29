using AliKuli.Extentions;
using AliKuli.UtilitiesNS.RandomNumberGeneratorNS;
using EnumLibrary.EnumNS;
using EnumLibrary.EnumNS.VerificationNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashEncashmentTrxNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequestHdrNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.BuySellDocNS;

namespace UowLibrary.SuperLayerNS
{


    public partial class SuperBiz
    {


        void getCustomerSalesmanAndOwnerSalesman(BuySellDoc bsd)
        {
            //We want to get the sales people so that they can share in the revenue
            //generated during all stages
            if (bsd.BuySellDocumentTypeEnum == BuySellDocumentTypeENUM.Purchase)
            {
                if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed)
                {
                    //make sure there a salesman has not been already selected
                    if (bsd.CustomerSalesmanId.IsNullOrWhiteSpace())
                    {
                        //No customer salesman exists
                        getAllCustomerSalesmen(bsd);

                    }


                    if (bsd.OwnerSalesmanId.IsNullOrWhiteSpace())
                    {
                        getAllOwnerSalesmen(bsd);
                    }
                }
            }
        }





        private List<BuySellDoc> get_BuySellDoc_Completed_Payments(CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum)
        {
            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type Unknown");

            if (cashTypeEnum == CashTypeENUM.NonRefundable)
            {
                //the buyselldoc are all refundable
                return null;
            }


            List<BuySellDoc> lstBuySellDocs = BuySellDocBiz.FindAll().Where(x =>
                x.BuySellDocStateEnum == BuySellDocStateENUM.Delivered).ToList();

            return lstBuySellDocs;
        }



        /// <summary>
        /// These are the documents that will show in the cash statements
        /// </summary>
        /// <param name="cashTypeEnum"></param>
        /// <param name="cashStateEnum"></param>
        /// <param name="lstBuySellDocs"></param>
        /// <returns></returns>
        private List<BuySellDoc> get_BuySellDocs_Allocated_Payments(CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum)
        {


            List<BuySellDoc> lstBuySellDocs = new List<BuySellDoc>();

            if (cashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type Unknown");

            if (cashStateEnum == CashStateENUM.Available)
                return lstBuySellDocs;

            if (cashTypeEnum == CashTypeENUM.NonRefundable)
                return lstBuySellDocs;

            lstBuySellDocs = BuySellDocBiz.FindAll().Where(x =>
                x.BuySellDocStateEnum == BuySellDocStateENUM.RequestConfirmed ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.BeingPreparedForShipmentBySeller ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.CourierComingToPickUp ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.Enroute ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.Problem)
                .ToList();

            return lstBuySellDocs;
        }



        private List<BuySellDoc> get_BuySellDocs_Allocated_Receipt()
        {

            List<BuySellDoc> lstBuySellDocs = BuySellDocBiz.FindAll().Where(x =>
                x.BuySellDocStateEnum == BuySellDocStateENUM.BeingPreparedForShipmentBySeller ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.CourierComingToPickUp ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.PickedUp ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.Enroute ||
                x.BuySellDocStateEnum == BuySellDocStateENUM.Problem).ToList();
            return lstBuySellDocs;
        }

        public BuySellDoc GetDeliveryOrder(string buySellId)
        {
            buySellId.IsNullOrWhiteSpaceThrowArgumentException();
            BuySellDoc buySellDoc = BuySellDocBiz.FindAll().FirstOrDefault(x => x.Id == buySellId);
            return buySellDoc;
        }

        public string AddToSale(string userId, string productChildId, string poNumber, DateTime poDate)
        {
            userId.IsNullOrWhiteSpaceThrowException("No user");

            double totalThisItem = 0;

            //who is the owner:The product Child owner is the owner
            //get the productChild

            productChildId.IsNullOrWhiteSpaceThrowException("No Product");
            ProductChild productChild = ProductChildBiz.Find(productChildId);
            productChild.IsNullThrowException("productChild");



            //get the product child's owner
            Owner ownerProductChild = productChild.Owner;
            ownerProductChild.IsNullThrowException("productChildOwner");

            //get the owner
            //Get the select list for owner
            Person ownerPerson = ownerProductChild.Person;
            ownerPerson.IsNullThrowException("ownerPerson");
            System.Web.Mvc.SelectList ownerSelectList = OwnerBiz.SelectListOnlyWith(ownerProductChild);


            ////the user is the customer user;
            //get the customer
            Customer customerUser = CustomerBiz.GetPlayerFor(userId);

            //Get the select list for Customer
            //remove the owner from the list... owner cannot sell to self.
            System.Web.Mvc.SelectList customerSelectList = CustomerBiz.SelectListWithout(ownerPerson);
            System.Web.Mvc.SelectList selectListOwner = OwnerBiz.SelectListOnlyWith(ownerProductChild);
            System.Web.Mvc.SelectList selectListCustomer = CustomerBiz.SelectListWithout(ownerPerson);

            //this is the address in the customer
            AddressMain addressBillTo = customerUser.DefaultBillAddress;
            AddressMain addressShipTo = customerUser.DefaultShipAddress;
            AddressComplex addressShipFromComplex = productChild.ShipFromAddressComplex;

            string addressBillToId = "";
            string addressShipToId = "";

            if (!addressBillTo.IsNull())
            {
                addressBillToId = addressBillTo.Id;
            }

            if (!addressShipTo.IsNull())
            {
                addressShipToId = addressShipTo.Id;
            }


            //Get the select list for AddressInform
            System.Web.Mvc.SelectList selectListBillTo = AddressBiz.SelectListBillAddressCurrentUser();
            //Get the select list for AddressShipTo
            System.Web.Mvc.SelectList selectListShipTo = AddressBiz.SelectListShipAddressCurrentuser();



            //check to see if there is any open sale which belongs to the same buyer and seller
            BuySellDoc sale = BuySellDocBiz.GetOpenSaleWithSameCustomerAndSeller(customerUser.Id, ownerProductChild.Id, productChild);

            //create the itemList.
            List<BuySellItem> buySellItems = new List<BuySellItem>();
            string shopId = "";
            if (sale.IsNull())
            {
                //otherwise add a new sale
                sale = CreateSale(
                    productChild,
                    ownerProductChild,
                    1,
                    productChild.Sell.SellPrice,
                    customerUser,
                    selectListOwner,
                    selectListCustomer,
                    addressBillToId,
                    addressShipToId,
                    selectListBillTo,
                    selectListShipTo,
                    BuySellDocStateENUM.RequestUnconfirmed,
                    BuySellDocStateModifierENUM.Unknown,
                    DateTime.Now,
                    DateTime.Now,
                    shopId);

                totalThisItem++;

            }

            else
            {
                //dont really need this, but it is good for consistancy.
                sale.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Purchase;
                sale.BuySellDocStateEnum = BuySellDocStateENUM.RequestUnconfirmed;

                //now check to see if it is the same item... or is it a new item
                //get list of items for the sale
                List<BuySellItem> itemList = sale.BuySellItems.Where(x => x.MetaData.IsDeleted == false).ToList();

                if (itemList.IsNullOrEmpty())
                {
                    BuySellItem buySellItem = new BuySellItem(sale.Id, productChild.Id, 1, productChild.Sell.SellPrice, productChild.FullName());
                    sale.Add(buySellItem);
                }
                else
                {
                    //there are items in the list
                    BuySellItem itemFound = itemList.FirstOrDefault(x => x.ProductChildId == productChild.Id);
                    if (itemFound.IsNull())
                    {
                        BuySellItem buySellItem = new BuySellItem(sale.Id, productChild.Id, 1, productChild.Sell.SellPrice, productChild.FullName());
                        sale.Add(buySellItem);
                    }
                    else
                    {
                        totalThisItem = itemFound.Quantity.Order;
                        itemFound.Quantity.Order += 1;
                        itemFound.Quantity.Order_Original = itemFound.Quantity.Order;
                        //itemFound.Quantity.Ship += 1;
                    }
                }

                totalThisItem++;
                sale.AddressShipFromComplex = addressShipFromComplex;

                BuySellDocBiz.GetDefaultVehicalType(sale);
                sale.AddressShipFromId = productChild.ShipFromAddressId;

                sale.RequestUnconfirmed.SetToTodaysDate(UserName, UserId);

                ControllerCreateEditParameter parm = new ControllerCreateEditParameter();
                parm.Entity = sale as ICommonWithId;
                parm.GlobalObject = GetGlobalObject();

                BuySellDocBiz.Update(parm);

            }

            BuySellDocBiz.SaveChanges();
            string message = string.Format("Success. You ordered {0:N2} for {1:N2} (X{2:N2})", productChild.FullName(), productChild.Sell.SellPrice, totalThisItem);
            return message;

        }



        /// <summary>
        /// Use this to create buy sell orders programatically
        /// </summary>
        /// <param name="productChild"></param>
        /// <param name="ownerProductChild"></param>
        /// <param name="quantity"></param>
        /// <param name="customerUser"></param>
        /// <param name="selectListOwner"></param>
        /// <param name="selectListCustomer"></param>
        /// <param name="addressBillToId"></param>
        /// <param name="addressShipToId"></param>
        /// <param name="selectListBillTo"></param>
        /// <param name="selectListShipTo"></param>
        /// <param name="buySellDocStateEnum"></param>
        /// <returns></returns>


        public BuySellDoc CreateSale(
            ProductChild productChild,
            Owner ownerProductChild,
            int quantity,
            decimal salePrice,
            Customer customerUser,
            System.Web.Mvc.SelectList selectListOwner,
            System.Web.Mvc.SelectList selectListCustomer,
            string addressBillToId,
            string addressShipToId,
            System.Web.Mvc.SelectList selectListBillTo,
            System.Web.Mvc.SelectList selectListShipTo,
            BuySellDocStateENUM buySellDocStateEnum,
            BuySellDocStateModifierENUM buySellDocStateModifierEnum,
            DateTime pleasePickupOnDate_End,
            DateTime expectedDeliveryDate,
            string shopId)
        {
            BuySellDoc sale = BuySellDocBiz.Factory() as BuySellDoc;
            sale.ShopId = shopId;

            sale.Initialize(
                ownerProductChild.Id,
                customerUser.Id,
                addressBillToId,
                addressShipToId,
                selectListOwner,
                selectListCustomer,
                selectListBillTo,
                selectListShipTo);

            if (pleasePickupOnDate_End == DateTime.MaxValue || pleasePickupOnDate_End == DateTime.MinValue)
            {

            }
            else
            {
                sale.PleasePickupOnDate_End = pleasePickupOnDate_End;
            }

            if (expectedDeliveryDate == DateTime.MaxValue || expectedDeliveryDate == DateTime.MinValue)
            {

            }
            else
            {
                sale.ExpectedDeliveryDate = expectedDeliveryDate;
            }


            //dont really need this, but it is good for consistancy.
            sale.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Purchase;
            sale.BuySellDocStateModifierEnum = buySellDocStateModifierEnum;
            sale.BuySellDocStateEnum = buySellDocStateEnum;
            sale.ExpectedDeliveryDate = expectedDeliveryDate;
            BuySellItem buySellItem = new BuySellItem(sale.Id, productChild.Id, quantity, salePrice, productChild.FullName());
            sale.Add(buySellItem);



            BuySellDocBiz.GetDefaultVehicalType(sale);
            //add the owners address
            sale.AddressShipFromId = productChild.ShipFromAddressId;

            setStatusDate(sale);
            ControllerCreateEditParameter parm = new ControllerCreateEditParameter();
            parm.Entity = sale as ICommonWithId;
            parm.GlobalObject = GetGlobalObject();
            getCustomerSalesmanAndOwnerSalesman(sale);


            //add the payment amount.






            BuySellDocBiz.Create(parm);

            return sale;
        }

        private void setStatusDate(BuySellDoc sale)
        {
            switch (sale.BuySellDocStateEnum)
            {
                case BuySellDocStateENUM.RequestUnconfirmed:
                    sale.RequestUnconfirmed.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.RequestConfirmed:
                    sale.RequestConfirmed.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                    sale.BeingPreparedForShipmentBySeller.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.ReadyForPickup:
                    sale.ReadyForPickup.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                    sale.CourierAcceptedByBuyerAndSeller.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.CourierComingToPickUp:
                    sale.CourierComingToPickUp.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.PickedUp:
                    sale.PickedUp.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.Enroute:
                    sale.Enroute.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.Delivered:
                    sale.Delivered.SetToTodaysDate(UserName, UserId);
                    sale.Delivered.IsTrue = true;
                    break;
                case BuySellDocStateENUM.Rejected:
                    sale.Rejected.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.Problem:
                    sale.Problem.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.CashTransaction:
                    break;
                case BuySellDocStateENUM.CashEncashment:
                    //sale.CashEncashment.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.OptedOutOfSystem:
                    sale.RequestUnconfirmed.SetToTodaysDate(UserName, UserId);
                    break;
                case BuySellDocStateENUM.Unknown:
                case BuySellDocStateENUM.InProccess:
                case BuySellDocStateENUM.BackOrdered:
                case BuySellDocStateENUM.All:
                    break;
                default:
                    break;
            }
        }



        public void PrepareEncashmentForUser(string userId, string userName, CashEncashmentTrx cet)
        {

            userId.IsNullOrWhiteSpaceThrowArgumentException("You must be logged in");
            cet.PaymentMethodId.IsNullOrWhiteSpaceThrowException("No method selected");


            decimal currBalanceForUser = BalanceFor_User(userId, CashTypeENUM.Refundable);
            if (currBalanceForUser != cet.CurrentBalance_Refundable)
            {
                throw new Exception("Something is wrong. Previous balance does not match current balance.");
            }

            if (currBalanceForUser >= cet.Amount)
            {
                cet.IsApproved.MarkTrue(UserName, UserId);
                RandomNoGenerator randomGen = new RandomNoGenerator(3);
                cet.SecretNumber = randomGen.GetRandomNumber(1);

                PaymentMethod paymentMethod = PaymentMethodBiz.Find(cet.PaymentMethodId);
                paymentMethod.IsNullThrowException();

                cet.SystemMessageToApplicant = paymentMethod.DetailInfoToDisplayOnWebsite;

                Person userPerson = UserBiz.GetPersonFor(userId);
                userPerson.IsNullThrowException("Person not found");
                cet.PersonRequestingPaymentId = userPerson.Id;

                if (userPerson.CashEncashmentTrxs.IsNull())
                {
                    userPerson.CashEncashmentTrxs = new List<CashEncashmentTrx>();
                }
                userPerson.CashEncashmentTrxs.Add(cet);
                CashEncashmentTrxBiz.CreateAndSave(cet);

                ErrorsGlobal.AddMessage("Encashment certificate created and approved.");

            }
            else
            {

                throw new Exception("Something went wrong. You do not have the balance");
            }


        }



        public static void GetPenaltyFor(BuySellDoc buySellDoc, decimal saleAmountToBePenalized)
        {
            PenaltyController penaltyController = new PenaltyController();
            //throw new NotImplementedException();
        }

        public void CreateAnIWantToBeASalesmanEntry(string UserId)
        {
            //get the person

            if (CurrentUserParameter.IsSalesman)
                throw new Exception("You are already a salesman!");

            decimal costToBecomeASalesman = SuperBiz.GetCostToBecomeASalesman();

            ServiceRequestHdrBiz.CreateAnIWantToBeASalesmanEntry(
                CurrentUserParameter.PersonId,
                CurrentUserParameter.SystemPersonId,
                costToBecomeASalesman);

        }




        public List<ServiceRequestVM> GetListOfPeopleWantingJobs()
        {
            //just get those jobs that are withing the users expertise.
            List<ServiceRequestHdr> allServiceHeaders = new List<ServiceRequestHdr>();
            List<ServiceRequestHdr> salesmenRequestsOnly = new List<ServiceRequestHdr>();
            List<ServiceRequestHdr> mailerRequestsOnly = new List<ServiceRequestHdr>();
            List<ServiceRequestHdr> sellerRequestsOnly = new List<ServiceRequestHdr>();
            List<ServiceRequestHdr> customerRequestsOnly = new List<ServiceRequestHdr>();

            IQueryable<ServiceRequestHdr> allIq = ServiceRequestHdrBiz.FindAll().Where(x => x.ServiceRequestStatusEnum == ServiceRequestStatusENUM.Open);
            List<ServiceRequestHdr> allIq_DEBUG = allIq.ToList();
            sellerRequestsOnly = allIq.Where(x => x.RequestTypeEnum == ServiceRequestTypeENUM.BecomeSeller).ToList();
            customerRequestsOnly = allIq.Where(x => x.RequestTypeEnum == ServiceRequestTypeENUM.BecomeCustomer).ToList();

            if (CurrentUserParameter.IsSuperSalesman || CurrentUserParameter.IsAdmin)
            {
                salesmenRequestsOnly = allIq.Where(x => x.RequestTypeEnum == ServiceRequestTypeENUM.BecomeSalesman).ToList();

            }

            if (CurrentUserParameter.IsMailer || CurrentUserParameter.IsAdmin)
            {
                mailerRequestsOnly = allIq.Where(x => x.RequestTypeEnum == ServiceRequestTypeENUM.BecomeMailer).ToList();

            }

            //Concat

            if (!salesmenRequestsOnly.IsNullOrEmpty())
                allServiceHeaders = allServiceHeaders.Concat(salesmenRequestsOnly).ToList();

            if (!mailerRequestsOnly.IsNullOrEmpty())
                allServiceHeaders = allServiceHeaders.Concat(mailerRequestsOnly).ToList();

            if (!sellerRequestsOnly.IsNullOrEmpty())
                allServiceHeaders = allServiceHeaders.Concat(sellerRequestsOnly).ToList();

            if (!customerRequestsOnly.IsNullOrEmpty())
                allServiceHeaders = allServiceHeaders.Concat(customerRequestsOnly).ToList();

            if (allServiceHeaders.IsNullOrEmpty())
                return new List<ServiceRequestVM>();

            List<ServiceRequestVM> lstOfPplWantingJobs = new List<ServiceRequestVM>();

            foreach (ServiceRequestHdr srh in allServiceHeaders)
            {
                ServiceRequestVM srvm = new ServiceRequestVM(
                    srh.Id,
                    srh.MetaData.Created.Date_NotNull_Min,
                    srh.PersonFrom.FullName(),
                    srh.RequestTypeEnum);

                lstOfPplWantingJobs.Add(srvm);
            }
            return lstOfPplWantingJobs;
        }
    }
}
