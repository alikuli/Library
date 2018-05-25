using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using EnumLibrary.EnumNS;
using InterfacesLibrary.ProductNS;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    [ComplexType]
    public class Dimensions : IDimensions                               
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }

        public override string ToString()
        {
            string s = " "; string.Format("h:{0} x w:{1} x l:{2}", Height, Width, Length);
            return s;
        }


    }
}
