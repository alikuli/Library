using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsClassLibrary.ModelsNS.AddressNS;

namespace ModelsClassLibrary.ModelsNS.ContactNS.AddressNS.AddressNS
{
    public class AddressJsVm : AddressStringWithNames
    {
        /// <summary>
        /// This can be a customer or owner Id
        /// </summary>
        public string AddressOwnerId { get; set; }
    }
}
