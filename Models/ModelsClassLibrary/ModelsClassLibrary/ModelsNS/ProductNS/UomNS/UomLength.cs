
using EnumLibrary.EnumNS;
using EnumLibrary.EnumNS.UomNS;
using System.Collections.Generic;
namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public class UomLength : UomAbstract
    {
        public virtual ICollection<Product> Products { get; set; }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.UomLength;
        }
    }
}