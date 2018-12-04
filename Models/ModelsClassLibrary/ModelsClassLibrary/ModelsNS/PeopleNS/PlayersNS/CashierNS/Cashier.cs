using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.Interfaces.PeopleNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    /// <summary>
    /// Cashier is owner who has privilages to:
    ///     Invoice
    ///     Receive Payments against Invoice
    ///     Etc
    ///     Cashier = VENDOR
    /// </summary>
    public class Cashier : PlayerAbstract, IPlayer
    {


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Cashier;
        }



        [Display(Name = "Cateogry")]
        [MaxLength(128)]
        public virtual string CashierCategoryId { get; set; }
        public virtual CashierCategory CashierCategory { get; set; }






        //[Display(Name = "Default Ship Address")]
        //[MaxLength(128)]

        //public virtual string AddressDefaultShipFromId { get; set; }
        //public virtual AddressWithId AddressDefaultCashFrom { get; set; }


        [NotMapped]
        public SelectList SelectListCashierCategory { get; set; }



        //[NotMapped]
        //public SelectList SelectListBillAddress { get; set; }


        //[NotMapped]
        //public SelectList SelectListCashFromAddress { get; set; }

        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);
            Cashier cashier = ic as Cashier;
            cashier.IsNullThrowException("Unable to unbox cashier");

            PersonId = cashier.PersonId;
            CashierCategoryId = cashier.CashierCategoryId;

        }
    }
}