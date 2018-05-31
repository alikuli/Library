
using EnumLibrary.EnumNS;
using System.Collections.Generic;
namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public class UomVolume : UomAbstract
    {
        public virtual ICollection<Product> Products { get; set; }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.UomVolume;
        }

    }
}