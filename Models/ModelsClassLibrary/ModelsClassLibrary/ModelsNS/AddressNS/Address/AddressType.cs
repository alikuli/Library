
using System.ComponentModel.DataAnnotations.Schema;
namespace ModelsClassLibrary.ModelsNS.AddressNS
{
    /// <summary>
    /// This is either General, ShipTo, InformTo, default is true for both.
    /// </summary>
    [ComplexType]
    public class AddressType
    {
        public AddressType()
        {
            ShipTo = true;
            InformTo = true;
        }
        /// <summary>
        /// this can be used for shipto only
        /// </summary>
        public bool ShipTo { get; set; }


        /// <summary>
        /// this can be used for inform to only
        /// </summary>
        public bool InformTo { get; set; }

        public void LoadFrom(AddressType a)
        {
            ShipTo = a.ShipTo;
            InformTo = a.InformTo;
        }



    }
}