using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterfacesLibrary.DocumentsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.DeliveryMethodNS
{
    public class DeliveryMethod:CommonWithId, IDeliveryMethod
    {
     //   public string Name { get; set; }

        public void LoadFrom(IDeliveryMethod d)
        {
            base.LoadFrom(d as CommonWithId);
        }
    }
}