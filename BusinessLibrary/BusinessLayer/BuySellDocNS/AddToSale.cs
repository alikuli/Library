using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.PeopleMessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
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
        public void AcceptCourier(string frtOfferId, BuySellDocumentTypeENUM buySellDocumentTypeEnum, decimal currBalance)
        {
            FreightOfferTrx freightOfferAcceptedTrx = getFreightOffer(frtOfferId);
            freightOfferAcceptedTrx.IsNullThrowException();

            BuySellDoc buyselldoc = getBuySellDoc(freightOfferAcceptedTrx);
            buyselldoc.IsNullThrowException();

            exceptionSellerAndDeliveryManIsTheSame(freightOfferAcceptedTrx, buyselldoc);
            exceptionIfCurrBalanceIsNotEnought(freightOfferAcceptedTrx.OfferAmount, currBalance);

            buyselldoc.BuySellDocumentTypeEnum = buySellDocumentTypeEnum;

            acceptDeliveryMan(freightOfferAcceptedTrx, buyselldoc);


        }

        private void exceptionIfCurrBalanceIsNotEnought(decimal freightOfferAmount, decimal currBalance)
        {
            if (freightOfferAmount > currBalance)
            {
                string err = string.Format("Freight offer is {0:N2} and your current amount is {1:N2}. You are short of {2:N2}. Please load your account with REFUNDABLE MONEY.",
                   freightOfferAmount,
                   currBalance,
                   freightOfferAmount - currBalance);
                throw new Exception(err);
            }
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
            buyselldoc.DeliveryCode_Customer = GetRandomCode();
            buyselldoc.PickupCode_Deliveryman = GetRandomCode();

            buyselldoc.FreightOfferTrxAcceptedId = freightOfferAcceptedTrx.Id;
            //buyselldoc.AcceptRejectOrEmpty = ConstantsLibrary.BuySellConstants.ACCEPT;
            buyselldoc.BuySellDocStateModifierEnum = BuySellDocStateModifierENUM.Accept;

            Update(buyselldoc);
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
