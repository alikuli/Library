using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumLibrary.EnumNS
{
    public enum WhoPaysWhoENUM
    {
        Unknown,
        OwnerPaysCustomer,
        OwnerPaysDeliveryMan,
        CustomerPaysOwner,
        CustomerPaysDeliveryman,
        DeliverymanPaysOwner,
        DeliverymanPaysCustomer,
    }
}
