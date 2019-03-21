using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
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

        public Message(Person fromPerson, Person toPerson, string subject, string body, MessageENUM messageEnum, ICollection<ProductChild> productChildrenBeingAdvertised)
            : this()
        {
            toPerson.IsNullThrowExceptionArgument("toPerson");
            List<Person> toPeople = new List<Person>();
            toPeople.Add(toPerson);
            initialize(fromPerson, toPeople, subject, body, messageEnum, productChildrenBeingAdvertised);

        }

        public Message(Person fromPerson, List<Person> toPeople, string subject, string body, MessageENUM messageEnum, ICollection<ProductChild> productChildrenBeingAdvertised)
            : this()
        {
            initialize(fromPerson, toPeople, subject, body, messageEnum, productChildrenBeingAdvertised);

        }

        private void initialize(Person fromPerson, List<Person> toPeople, string subject, string body, MessageENUM messageEnum, ICollection<ProductChild> productChildrenBeingAdvertised)
        {
            FromPersonId = fromPerson.Id;
            FromPerson = fromPerson;
            //ToPeople = toPeople; FIX
            Subject = subject;
            Body = body;
            MessageEnum = messageEnum;
            ProductChildrenBeingAdvertised = productChildrenBeingAdvertised;

        }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Message;
        }

        [NotMapped]
        public SelectList SelectListPersonFrom { get; set; }


        public string MenuPath1Id { get; set; }
        public virtual MenuPath1 MenuPath1 { get; set; }


        public string MenuPath2Id { get; set; }
        public virtual MenuPath2 MenuPath2 { get; set; }

        public string MenuPath3Id { get; set; }
        public virtual MenuPath3 MenuPath3 { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        //public string ProductChildId { get; set; }

        ///// <summary>
        ///// This comes from the parameters. It tells us that the message is being sent from the productChild.
        ///// </summary>
        //public virtual ProductChild ProductChild { get; set; }

        /// <summary>
        /// These are the product children that are being advertised by the user.
        /// </summary>
        public virtual ICollection<ProductChild> ProductChildrenBeingAdvertised { get; set; }

        [Display(Name = "From")]
        public string FromPersonId { get; set; }

        [Display(Name = "From")]
        public virtual Person FromPerson { get; set; }



        [Display(Name = "To")]
        public virtual ICollection<PeopleMessage> ToPeople { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            FromPersonId.IsNullOrWhiteSpaceThrowException("FromPersonId");
            ToPeople.IsNullOrEmptyThrowException("ToPeople"); 

            if (MessageEnum == MessageENUM.Unknown)
                throw new Exception("Message type is unknown.");
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            Message message = icommonWithId as Message;
            message.IsNullThrowException("Unable to unbox message");
            FromPersonId = message.FromPersonId;
            //ToPeople = message.ToPeople;
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
