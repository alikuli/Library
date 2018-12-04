using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsClassLibrary.ModelsNS.SharedNS;
using EnumLibrary.EnumNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddessWithIdNS;

namespace ModelsClassLibrary.ModelsNS.PlacesNS.EmailAddressNS
{
    public class EmailAddress : PhoneEmailAddressAbstract
    {

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Email;
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            EmailAddress email = icommonWithId as EmailAddress;
            email.IsNullThrowException("Unable to unbox email");
            Name = email.Name;
        }

    }
}
