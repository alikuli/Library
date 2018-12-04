using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.AddressNS.PhoneNumberNS
{
    /// <summary>
    /// The phone numbers will be owned by the User. i.e. user's children. The name will have the phone number only
    /// thereby allowing other users to share the number... NO!
    /// </summary>
    public class PhoneNumber : CommonWithId
    {
        public string PersonId { get; set; }
        public Person Person { get; set; }

        public virtual string Number { get; set; }


        public virtual ApplicationUser User { get; set; }

        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return base.ClassNameForRights();
        }
        public override List<string> FieldsToLoadFromView()
        {
            return base.FieldsToLoadFromView();
        }
        public override string FullName()
        {
            return base.FullName();
        }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
        }

        public override string ToString()
        {
            string str = string.Format("{0} ", Name);
            return str;
        }
        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            PhoneNumber phn = icommonWithId as PhoneNumber;
            Number = phn.Number;
        }
        public override string MakeUniqueName()
        {
            //we need to add user id here as well
            string str = string.Format("{0}", Number);
            return base.MakeUniqueName();
        }
    }
}
