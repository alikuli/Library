//using Microsoft.Owin.BuilderProperties;
using ModelsClassLibrary.ModelsNS.People;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS.Address;

namespace ModelsClassLibrary.ModelsNS
{
    public class Salepoint:CommonWithId
    {
        
        [Display(Name = "Owned By")]
        public long OwnerId { get; set; }
        public virtual Owner Owner { get; set; }


        [Display(Name = "Address")]
        public long SalepointAddressId { get; set; }
        public virtual Address SalepointAddress { get; set; }

    }
}