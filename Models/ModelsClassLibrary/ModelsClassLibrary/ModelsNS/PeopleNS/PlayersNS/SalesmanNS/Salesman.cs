using EnumLibrary.EnumNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    /// <summary>
    /// Salesman is owner who has privilages to:
    ///     Invoice
    ///     Receive Payments against Invoice
    ///     Etc
    ///     Salesman = VENDOR
    /// </summary>
    public class Salesman : PlayerAbstract
    {


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Salesman;
        }



        [Display(Name = "Cateogry")]
        [MaxLength(128)]
        public virtual string SalesmanCategoryId { get; set; }
        public virtual SalesmanCategory SalesmanCategory { get; set; }






        //[Display(Name = "Default Ship Address")]
        //[MaxLength(128)]
        //public virtual string AddressDefaultShipFromId { get; set; }
        //public virtual AddressWithId AddressDefaultCashFrom { get; set; }


        [NotMapped]
        public SelectList SelectListSalesmanCategory { get; set; }



        //[NotMapped]
        //public SelectList SelectListBillAddress { get; set; }


        //[NotMapped]
        //public SelectList SelectListCashFromAddress { get; set; }
    }
}