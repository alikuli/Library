using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EnumLibrary.EnumNS
{

    public enum EducationLevelENUM
    {
        Unknown,
        None,
        [Description("Can Read")]
        CanRead,
        Primary,
        Secondary,
        Matric,
        [Description("First Year")]
        FirstYear,
        [Description("Second Year")]
        SecondYear,
        University,
        Masters,
        Proffessor,
    }
}