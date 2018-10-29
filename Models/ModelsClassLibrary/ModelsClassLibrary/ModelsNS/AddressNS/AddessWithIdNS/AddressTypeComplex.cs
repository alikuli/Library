
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ModelsClassLibrary.ModelsNS.AddressNS
{
    /// <summary>
    /// This is either General, ShipTo, InformTo, default is true for both.
    /// </summary>
    [ComplexType]
    public class AddressTypeComplex
    {
        public AddressTypeComplex()
        {
            ShipTo = true;
            InformTo = true;
            BillTo = true;
        }
        /// <summary>
        /// this can be used for shipto only
        /// </summary>
        /// 
        [Display(Name = "Ship to me")]
        public bool ShipTo { get; set; }


        /// <summary>
        /// this can be used for inform to only
        /// </summary>
        [Display(Name = "Inform me")]
        public bool InformTo { get; set; }

        [Display(Name = "Bill me")]
        public bool BillTo { get; set; }




    }
}