
using System;
using InterfacesLibrary.DocumentsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.DeliveryMethodNS
{
    
    public class PaymentMethod : CommonWithId
    {

        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.PaymentMethod;
        }

        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}