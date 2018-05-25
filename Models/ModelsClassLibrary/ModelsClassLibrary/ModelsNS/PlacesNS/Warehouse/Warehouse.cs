using ModelsClassLibrary.ModelsNS.People;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;

namespace ModelsClassLibrary.ModelsNS
{
    public class Warehouse:CommonWithId
    {

        [Display(Name = "Owned By")]
        public long OwnerId { get; set; }
        public virtual Owner Owner { get; set; }



        [Display(Name = "Address")]
        public long WareHouseAddressId { get; set; }
        //public virtual Address WareHouseAddress { get; set; }

    }
}