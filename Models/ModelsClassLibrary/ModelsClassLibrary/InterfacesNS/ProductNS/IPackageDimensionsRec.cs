using System;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace InterfacesLibrary.ProductNS
{
    public interface IDimensions
    {
        double Length { get; set; }
        double Height { get; set; }
        string ToString();
        //UomLength Uom { get; set; }
        double Width { get; set; }

        
    }
}
