using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EnumLibrary.EnumNS
{

    public enum BoardingENUM
    {
        Unknown,
        Yes,
        No,
        [Description("Dont Care")]
        DontCare,
        
    }
}