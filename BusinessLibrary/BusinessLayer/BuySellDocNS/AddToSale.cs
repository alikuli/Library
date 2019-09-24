using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using System;
using System.Linq;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {



        public BuySellDoc GetOpenSaleWithSameCustomerAndSeller(string customerId, string ownerProductChildId, ProductChild productChild)
        {
            customerId.IsNullThrowExceptionArgument("customerId");
            ownerProductChildId.IsNullThrowExceptionArgument("ownerProductChildId");
            productChild.IsNullThrowExceptionArgument("productChild");

            BuySellDoc buysSellDoc = FindAll().FirstOrDefault(x =>
                x.CustomerId == customerId &&
                x.OwnerId == ownerProductChildId &&
                x.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed);

            //make sure the product addresses are also the same.
            if (buysSellDoc.IsNull())
                return buysSellDoc;

            if (productChild.ShipFromAddress.IsNull())
            {
                productChild.ShipFromAddressId.IsNullOrWhiteSpaceThrowException();

                productChild.ShipFromAddress = AddressBiz.Find(productChild.ShipFromAddressId);
                productChild.ShipFromAddress.IsNullThrowException();
            }
            productChild.ShipFromAddressComplex = productChild.ShipFromAddress.ToAddressComplex();

            if (buysSellDoc.AddressShipFromComplex.Equals(productChild.ShipFromAddressComplex))
                return buysSellDoc;

            return null;


        }

        #region AcceptCourier
        public void AcceptCourier(FreightOfferTrx frtTrx, BuySellDocumentTypeENUM buySellDocumentTypeEnum, decimal currBalance, decimal insuranceRequired)
        {
            if (insuranceRequired < 0)
                throw new Exception("Insurance cannot be less than zero!");

            BuySellDoc bsd = getBuySellDoc(frtTrx);
            bsd.IsNullThrowException();
            exceptionSellerAndDeliveryManIsTheSame(frtTrx, bsd);

            bsd.BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
            
            //this should all happen when courier accepts.
            //bsd.DeliveryCode_Customer = GetRandomCode();
            //bsd.PickupCode_Deliveryman = GetRandomCode();
            //bsd.FreightOfferTrxAcceptedId = frtTrx.Id;
            
            bsd.BuySellDocStateModifierEnum = BuySellDocStateModifierENUM.Accept;
            bsd.InsuranceRequired = insuranceRequired;
            decimal maxDeliverymanLiability = maxPossibleLiabilityToDeliverParcel(bsd, frtTrx);

            if (frtTrx.MaxPossibleLiabilityToDeliverParcel() > currBalance)
            {
                frtTrx.IsNullThrowException();
                decimal insuranceAmountRqrdToPass = currBalance - frtTrx.OfferAmount;
                string err = string.Format("The deliveryman '{0}' is unable to satisfy financial guarantee requirements by Rs{1:N0}. If you would still like to use him, you have an insurance amount of {2:N0} which can be reduced to {4:N0} which will allow you to proceed and accept the deliveryman. This is risky. If something goes wrong during delivery, then the maximum amount you will be able to recover from the deliveryman will be {3:N0}. Alternatively, you can request '{0}' to increase his guarantee amount with the Company. Proceed cautiously.",
                   frtTrx.Deliveryman.FullName(),
                   maxDeliverymanLiability - currBalance,
                   bsd.InsuranceRequired,
                   currBalance,
                   insuranceAmountRqrdToPass);
                throw new Exception(err);
            }
            frtTrx.OfferAcceptedByOwner.SetToTodaysDate(UserName,UserId);
            FreightOfferTrxBiz.Update(frtTrx);
            Update(bsd);

        }

        decimal maxPossibleLiabilityToDeliverParcel(BuySellDoc bsd, FreightOfferTrx frtTrx)
        {
            bsd.IsNullThrowException();
            decimal ttlLiability = frtTrx.OfferAmount + bsd.InsuranceRequired;
            return ttlLiability;
        }


        private void exceptionIfCurrBalanceIsNotEnought(decimal freightOfferAmount, decimal currBalance)
        {
        }

        private BuySellDoc getBuySellDoc(FreightOfferTrx freightOfferAcceptedTrx)
        {
            freightOfferAcceptedTrx.BuySellDocId.IsNullOrWhiteSpaceThrowException();
            BuySellDoc buyselldoc = Find(freightOfferAcceptedTrx.BuySellDocId);
            return buyselldoc;
        }

        private FreightOfferTrx getFreightOffer(string frtOfferId)
        {
            frtOfferId.IsNullOrWhiteSpaceThrowArgumentException();
            FreightOfferTrx freightOfferAcceptedTrx = FreightOfferTrxBiz.Find(frtOfferId);
            return freightOfferAcceptedTrx;
        }

        private void acceptDeliveryMan(FreightOfferTrx freightOfferAcceptedTrx, BuySellDoc buyselldoc)
        {
        }

        private void updateTheAcceptedFreightOffer(FreightOfferTrx freightOfferAcceptedTrx)
        {
            //freightOfferAcceptedTrx.IsOfferAccepted = true;

            FreightOfferTrxBiz.Update(freightOfferAcceptedTrx);
        }


        /// <summary>
        /// This throws an exception if the deliveryman and the vendor are the same.
        /// </summary>
        /// <param name="freightOfferAcceptedTrx"></param>
        /// <param name="buyselldoc"></param>
        private void exceptionSellerAndDeliveryManIsTheSame(FreightOfferTrx freightOfferAcceptedTrx, BuySellDoc buyselldoc)
        {
            //do not allow if the DeliveryPerson and the VendorPerson and currentUser are the same
            UserId.IsNullOrWhiteSpaceThrowException();

            Person deliveryPerson = DeliverymanBiz.GetPersonForPlayer(freightOfferAcceptedTrx.DeliverymanId);
            Person sellerPerson = OwnerBiz.GetPersonForPlayer(buyselldoc.OwnerId);
            Person userPerson = UserBiz.GetPersonFor(UserId);

            deliveryPerson.IsNullThrowException();
            sellerPerson.IsNullThrowException();
            deliveryPerson.Id.IsNullOrWhiteSpaceThrowException();
            sellerPerson.Id.IsNullOrWhiteSpaceThrowException();
            userPerson.IsNullThrowException();

            if (deliveryPerson.Id == sellerPerson.Id && userPerson.Id == sellerPerson.Id && buyselldoc.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup)
            {
                throw new Exception("You are the seller. You cannot bid for this. If you want to deliver the item, ask the customer to accept you.");
            }
        }

        #endregion

        //public void CancelRejectOrder(RejectCancelDeleteInbetweenClass rcdbc)
        //{

        //    rcdbc.BuySellDocId.IsNullOrWhiteSpaceThrowArgumentException();

        //    BuySellDoc buySellDoc = Find(rcdbc.BuySellDocId);
        //    buySellDoc.BuySellDocStateModifierEnum = rcdbc.BuySellDocStateModifierEnum;
        //    buySellDoc.BuySellDocumentTypeEnum = rcdbc.BuySellDocumentTypeEnum;


        //    //create subject
        //    rcdbc.Subject = rcdbc.ToString();

        //    if (rcdbc.Comment.IsNullOrWhiteSpace())
        //    { }
        //    else
        //    {
        //        //add the message
        //        //current user is sending the message. 
        //        //if the document type is purchase, then it is the customer
        //        //if document type is sale, then it is the owner sending the message.
        //        //we need to find the persons for each.
        //        createMessage(rcdbc, buySellDoc);
        //    }

        //    createPenaltyTrx(buySellDoc);
        //    UpdateAndSave(buySellDoc);

        //}

        //private void createPenaltyTrx(BuySellDoc buySellDoc)
        //{
        //    if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Cancel)
        //    {
        //        IPenaltyClass penelty = PenaltyController.GetPenalty(buySellDoc);
        //        if (penelty.IsNull())
        //            return;
        //        //create a cash transaction

        //    }

        //}

        //private void createMessage(RejectCancelDeleteInbetweenClass rcdbc, BuySellDoc buySellDoc)
        //{
        //    string fromPersonId = "";
        //    string toPersonId = "";
        //    Person fromPerson = null;
        //    List<string> listOfToPeople = new List<string>();

        //    string buySellDocId = buySellDoc.Id;
        //    Person ownerPerson = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);
        //    Person customerPerson = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);

        //    ownerPerson.IsNullThrowException();
        //    customerPerson.IsNullThrowException();
        //    switch (buySellDoc.BuySellDocumentTypeEnum)
        //    {
        //        case BuySellDocumentTypeENUM.Sale:
        //            fromPersonId = ownerPerson.Id;
        //            fromPerson = ownerPerson;
        //            toPersonId = customerPerson.Id;
        //            listOfToPeople.Add(toPersonId);
        //            break;

        //        case BuySellDocumentTypeENUM.Purchase:
        //            toPersonId = ownerPerson.Id;
        //            fromPersonId = customerPerson.Id;
        //            fromPerson = customerPerson;
        //            listOfToPeople.Add(toPersonId);

        //            break;
        //        case BuySellDocumentTypeENUM.Delivery:
        //            Person deliveryPerson = DeliverymanBiz.GetPersonForPlayer(buySellDoc.DeliverymanId);
        //            deliveryPerson.IsNullThrowException();

        //            fromPersonId = deliveryPerson.Id;
        //            fromPerson = deliveryPerson;
        //            listOfToPeople.Add(ownerPerson.Id);
        //            listOfToPeople.Add(customerPerson.Id);
        //            break;

        //        case BuySellDocumentTypeENUM.Unknown:
        //        default:
        //            throw new Exception("Unknown document type");
        //    }

        //    Message message = new Message(
        //        fromPersonId,
        //        fromPerson,
        //        listOfToPeople,
        //        rcdbc.Subject,
        //        rcdbc.Comment,
        //        MessageENUM.Free,
        //        null);

        //    if (buySellDoc.Messages.IsNull())
        //        buySellDoc.Messages = new List<Message>();

        //    buySellDoc.Messages.Add(message);
        //    message.BuySellDocId = buySellDoc.Id;

        //    message.ListOfToPeopleId.IsNullOrEmptyThrowException();
        //    foreach (var pplId in message.ListOfToPeopleId)
        //    {
        //        Person person = PersonBiz.Find(pplId);
        //        person.IsNullThrowException();

        //        PeopleMessage pplMsg = new PeopleMessage();
        //        pplMsg.IsNullThrowException();
        //        pplMsg.PersonId = pplId;
        //        pplMsg.Person = person;
        //        pplMsg.MessageId = message.Id;
        //        PeopleMessageBiz.Create(pplMsg);

        //    }
        //    MessageBiz.Create(message);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buySellDocId"></param>
        /// <param name="buySellDocumentTypeEnum"></param>
        public void CancelDeliveryManAndSave_GET(string buySellDocId, BuySellDocumentTypeENUM buySellDocumentTypeEnum)
        {
            //clear the buySell header 
            buySellDocId.IsNullOrWhiteSpaceThrowException();

            BuySellDoc buySellDoc = Find(buySellDocId);
            buySellDoc.IsNullThrowException();

            buySellDoc.BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
            buySellDoc.BuySellDocStateModifierEnum = BuySellDocStateModifierENUM.Cancel;

            Update(buySellDoc);
            SaveChanges();
            //mark the order as false. It should already be false
        }

    }
}
