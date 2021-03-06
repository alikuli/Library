﻿using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PeopleMessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace ModelsClassLibrary.ModelsNS.MessageNS
{
    /// <summary>
    /// The message system works so that it sends messages between people.
    /// </summary>
    public class Message : CommonWithId
    {
        public Message()
        {
            Name = MakeUniqueName();
            MessageEnum = MessageENUM.Unknown;
        }

        public Message(string fromPersonId, Person fromPerson, string toPersonId, string subject, string body, MessageENUM messageEnum, ICollection<ProductChild> productChildrenBeingAdvertised)
            : this()
        {
            toPersonId.IsNullThrowExceptionArgument("toPerson");
            List<string> toPeopleId = new List<string>();
            toPeopleId.Add(toPersonId);
            initialize(fromPersonId, fromPerson, toPeopleId, subject, body, messageEnum, productChildrenBeingAdvertised);

        }

        public Message(string fromPersonId, Person fromPerson, List<string> listOfToPeopleId, string subject, string body, MessageENUM messageEnum, ICollection<ProductChild> productChildrenBeingAdvertised)
            : this()
        {
            initialize(fromPersonId, fromPerson, listOfToPeopleId, subject, body, messageEnum, productChildrenBeingAdvertised);

        }

        private void initialize(string fromPersonId, Person fromPerson, List<string> listOfToPeopleId, string subject, string body, MessageENUM messageEnum, ICollection<ProductChild> productChildrenBeingAdvertised)
        {
            FromPersonId = fromPersonId;
            Subject = subject;
            Body = body;
            MessageEnum = messageEnum;
            ListOfToPeopleId = listOfToPeopleId;
            FromPerson = fromPerson;
        }


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Message;
        }

        [NotMapped]
        public SelectList SelectListPersonFrom { get; set; }

        public List<string> ListOfToPeopleId { get; set; }

        #region Navigation

        public string MenuPath1Id { get; set; }
        public virtual MenuPath1 MenuPath1 { get; set; }


        public string MenuPath2Id { get; set; }
        public virtual MenuPath2 MenuPath2 { get; set; }

        public string MenuPath3Id { get; set; }
        public virtual MenuPath3 MenuPath3 { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string ProductChildId { get; set; }
        public virtual ProductChild ProductChild { get; set; }

        [Display(Name = "Document")]
        public string BuySellDocId { get; set; }
        public virtual BuySellDoc BuySellDoc { get; set; }


        public virtual List<PeopleMessage> PeopleMessages { get; set; }



        [Display(Name = "From")]
        public string FromPersonId { get; set; }

        [Display(Name = "From")]
        public virtual Person FromPerson { get; set; }



        //[Display(Name = "To")]
        //public virtual ICollection<PeopleMessage> ToPeople { get; set; }

        #endregion

        public string FromPersonName { get; set; }

        public string Subject { get; set; }
        public string Subject_Print_Version
        {
            get
            {
                string _subject = "";
                string name = FromPerson.IsNull() ? "" : Subject + " By " + FromPerson.FullName();

                if (!Subject.IsNullOrWhiteSpace())
                    _subject = string.Format("{0} - {1}",
                        MetaData.Created.Date_NotNull_Min.ToString("dd-MMM-yyyy hh:mm:ss"),
                                                name);


                return _subject;
            }
        }

        public string Body { get; set; }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            FromPersonId.IsNullOrWhiteSpaceThrowException("FromPersonId");
            ListOfToPeopleId.IsNullOrEmptyThrowException("ToPeople");

            if (MessageEnum == MessageENUM.Unknown)
                throw new Exception("Message type is unknown.");
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            Message message = icommonWithId as Message;
            message.IsNullThrowException("Unable to unbox message");

            FromPersonId = message.FromPersonId;
            MenuPath1Id = message.MenuPath1Id;
            MenuPath2Id = message.MenuPath2Id;
            MenuPath3Id = message.MenuPath3Id;
            ProductId = message.ProductId;
            ProductChildId = message.ProductChildId;

            Subject = message.Subject;
            Body = message.Body;
            MessageEnum = message.MessageEnum;
        }

        public override string MakeUniqueName()
        {
            string name = string.Format("{0}", DateTime.Now.Ticks.ToString());
            return name;
        }

        public MessageENUM MessageEnum { get; set; }

    }
}
