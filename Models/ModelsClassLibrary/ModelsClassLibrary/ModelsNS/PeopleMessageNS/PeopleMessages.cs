using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.PeopleMessageNS
{
    public class PeopleMessage : CommonWithId
    {
        public PeopleMessage()
        {
            if (Name.IsNullOrWhiteSpace())
                Name = MakeUniqueName();
        }
        public string PersonId { get; set; }
        public virtual Person Person { get; set; }

        public string MessageId { get; set; }
        public virtual Message Message { get; set; }

        public override string MakeUniqueName()
        {
            string name = Id;
            return name;
        }
        public static PeopleMessage Unbox(ICommonWithId icommonWithId)
        {
            PeopleMessage peopleMessage = icommonWithId as PeopleMessage;
            peopleMessage.IsNullThrowException();
            return peopleMessage;
        }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.PeopleMessage;
        }


        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            MessageId.IsNullOrWhiteSpaceThrowException();
            PersonId.IsNullOrWhiteSpaceThrowException();
            Name.IsNullOrWhiteSpaceThrowException();
        }


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            PeopleMessage peopleMessage = PeopleMessage.Unbox(icommonWithId);

            PersonId = peopleMessage.PersonId;
            MessageId = peopleMessage.MessageId;
        }

    }
}
