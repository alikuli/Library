using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EnumLibrary.EnumNS
{

    public enum MaritalStatusENUM
    {
        Unknown,
        [Description("Single without Family")]
        Single,

        [Description("Single with Family")]
        SingleWithFamily,

        [Description("Married without Family")]
        Married,

        [Description("Married with Family")]
        MarriedWithFamily,

        [Description("Widowed without Family")]
        Widowed,

        [Description("Widowed with Family")]
        WidowedWithFamily,

    }
}