
using EnumLibrary.EnumNS;
using System.Collections.Generic;
namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public class UomWeight : UomAbstract
    {
        public virtual ICollection<Product> Products_WeightListed { get; set; }
        public virtual ICollection<Product> Products_WeightActual { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.UomWeight;
        }
    }
}