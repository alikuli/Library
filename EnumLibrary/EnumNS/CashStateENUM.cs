using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumLibrary.EnumNS
{
    public enum CashStateENUM
    {
        Available,   //If cash is available, you can do pretty much anything with it
        Allocated,   //Once cash is allocated, you can only make it available. It is part of your account but promised somewhere.
        All,
    }
}
