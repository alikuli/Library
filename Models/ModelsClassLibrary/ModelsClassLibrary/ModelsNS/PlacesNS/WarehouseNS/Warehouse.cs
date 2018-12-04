using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.PlacesNS.WarehouseNS
{
    public class Warehouse : CommonWithId
    {
        [Display(Name = "Address")]
        public virtual string AddressId { get; set; }
        public virtual AddressWithId Address { get; set; }


        [Display(Name = "Warehouse Owner")]
        public virtual string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }



        [NotMapped]
        public SelectList SelectListUsers { get; set; }


        [NotMapped]
        public SelectList SelectListAddresses { get; set; }


    }
}
