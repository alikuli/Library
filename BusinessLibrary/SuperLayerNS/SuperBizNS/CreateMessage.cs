
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.PeopleMessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;

namespace UowLibrary.SuperLayerNS
{
    public partial class SuperBiz
    {
        private void createMessageFor(RejectCancelDeleteInbetweenClass rcdbc, BuySellDoc buySellDoc)
        {
            string fromPersonId = "";
            string toPersonId = "";
            Person fromPerson = null;
            List<string> listOfToPeople = new List<string>();

            string buySellDocId = buySellDoc.Id;
            Person ownerPerson = OwnerBiz.GetPersonForPlayer(buySellDoc.OwnerId);
            Person customerPerson = CustomerBiz.GetPersonForPlayer(buySellDoc.CustomerId);

            ownerPerson.IsNullThrowException();
            customerPerson.IsNullThrowException();
            switch (buySellDoc.BuySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Sale:
                    fromPersonId = ownerPerson.Id;
                    fromPerson = ownerPerson;
                    toPersonId = customerPerson.Id;
                    listOfToPeople.Add(toPersonId);
                    break;

                case BuySellDocumentTypeENUM.Purchase:
                    toPersonId = ownerPerson.Id;
                    fromPersonId = customerPerson.Id;
                    fromPerson = customerPerson;
                    listOfToPeople.Add(toPersonId);

                    break;
                case BuySellDocumentTypeENUM.Delivery:
                    Person deliveryPerson = DeliverymanBiz.GetPersonForPlayer(buySellDoc.DeliverymanId);
                    deliveryPerson.IsNullThrowException();

                    fromPersonId = deliveryPerson.Id;
                    fromPerson = deliveryPerson;
                    listOfToPeople.Add(ownerPerson.Id);
                    listOfToPeople.Add(customerPerson.Id);
                    break;

                case BuySellDocumentTypeENUM.Unknown:
                default:
                    throw new Exception("Unknown document type");
            }

            Message message = new Message(
                fromPersonId,
                fromPerson,
                listOfToPeople,
                rcdbc.Subject,
                rcdbc.Comment,
                MessageENUM.Free,
                null);

            if (buySellDoc.Messages.IsNull())
                buySellDoc.Messages = new List<Message>();

            buySellDoc.Messages.Add(message);
            message.BuySellDocId = buySellDoc.Id;

            message.ListOfToPeopleId.IsNullOrEmptyThrowException();
            foreach (var pplId in message.ListOfToPeopleId)
            {
                Person person = PersonBiz.Find(pplId);
                person.IsNullThrowException();

                PeopleMessage pplMsg = new PeopleMessage();
                pplMsg.IsNullThrowException();
                pplMsg.PersonId = pplId;
                pplMsg.Person = person;
                pplMsg.MessageId = message.Id;
                PeopleMessageBiz.Create(pplMsg);

            }
            MessageBiz.Create(message);
        }

        private void createMessageFor(string fromPersonId, string toPersonId, string subject, string body)
        {
            fromPersonId.IsNullOrWhiteSpaceThrowArgumentException();
            toPersonId.IsNullOrWhiteSpaceThrowArgumentException();
            subject.IsNullOrWhiteSpaceThrowArgumentException();
            body.IsNullOrWhiteSpaceThrowArgumentException();

            Person fromPerson = PersonBiz.Find(fromPersonId);
            fromPerson.IsNullThrowException();

            Message message = new Message(fromPersonId, fromPerson, toPersonId, subject, body, MessageENUM.Free, null);
            MessageBiz.Create(message);
        }





    }
}
