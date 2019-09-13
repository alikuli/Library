using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.MessagesToPeopleListNS
{
    /// <summary>
    /// These are the list of people who are in the To part of the message. For every message this is
    /// either one or more people. Duplicates not allowed.
    /// </summary>
    public class MessageToPeopleList : CommonWithId
    {

        public MessageToPeopleList()
        {

        }

        [Display(Name = "Person")]
        public string PersonId { get; set; }
        public virtual Person Person { get; set; }
        [NotMapped]
        public SelectList SelectListPerson { get; set; }




        [Display(Name = "Message")]
        public string MessageId { get; set; }
        public virtual Message Message { get; set; }

        [NotMapped]
        public SelectList SelectListMessage { get; set; }

        public static MessageToPeopleList Unbox(ICommonWithId icommonWithId)
        {
            MessageToPeopleList messageToPeopleList = icommonWithId as MessageToPeopleList;
            messageToPeopleList.IsNullThrowException();
            return messageToPeopleList;
        }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.MessageToPeopleList;
        }


        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            MessageId.IsNullOrWhiteSpaceThrowException();
            PersonId.IsNullOrWhiteSpaceThrowException();
            Name.IsNullOrWhiteSpaceThrowException();
        }


        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            MessageToPeopleList messageToPeople = icommonWithId as MessageToPeopleList;
            messageToPeople.IsNullThrowException();

            PersonId = messageToPeople.PersonId;
            MessageId = messageToPeople.MessageId;
        }

    }
}
