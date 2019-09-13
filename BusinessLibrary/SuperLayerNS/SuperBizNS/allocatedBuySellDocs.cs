using AliKuli.Extentions;
using AliKuli.UtilitiesNS.RandomNumberGeneratorNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashEncashmentTrxNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.BuySellDocNS;

namespace UowLibrary.SuperLayerNS
{


    public partial class SuperBiz
    {


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


            //check if user has enough balance to purchase the product.
            //if(!HasAvailableBalance_User(userId,CashTypeENUM.Refundable,productChild.Sell.SellPrice))
            //    throw new Exception("You do not have the available balance to purchase this.");
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
            //customerUser.Person.IsNullThrowException("No person for customer");
            //Person customerUserPerson = customerUser.Person;
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
            if (sale.IsNull())
            {
                //otherwise add a new sale
                sale = BuySellDocBiz.Factory() as BuySellDoc;

                sale.Initialize(
                    ownerProductChild.Id,
                    customerUser.Id,
                    addressBillToId,
                    addressShipToId,
                    poNumber,
                    poDate,
                    selectListOwner,
                    selectListCustomer,
                    selectListBillTo,
                    selectListShipTo);

                //dont really need this, but it is good for consistancy.
                sale.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Purchase;
                sale.BuySellDocStateEnum = BuySellDocStateENUM.RequestUnconfirmed;

                BuySellItem buySellItem = new BuySellItem(sale.Id, productChild.Id, 1, productChild.Sell.SellPrice, productChild.FullName());
                sale.Add(buySellItem);

                BuySellDocBiz.GetDefaultVehicalType(sale);
                //add the owners address
                sale.AddressShipFromId = productChild.ShipFromAddressId;
                BuySellDocBiz.Create(sale);
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

                BuySellDocBiz.Update(sale);

            }

            BuySellDocBiz.SaveChanges();
            string message = string.Format("Success. You ordered {0:N2} for {1:N2} (X{2:N2})", productChild.FullName(), productChild.Sell.SellPrice, totalThisItem);
            return message;

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
    }
}
