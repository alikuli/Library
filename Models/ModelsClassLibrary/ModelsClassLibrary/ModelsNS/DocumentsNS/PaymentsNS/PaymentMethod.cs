
using System;
using InterfacesLibrary.DocumentsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.DeliveryMethodNS
{
    
    public class PaymentMethod : CommonWithId
    {

        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.PaymentMethod;
        }
    }
}