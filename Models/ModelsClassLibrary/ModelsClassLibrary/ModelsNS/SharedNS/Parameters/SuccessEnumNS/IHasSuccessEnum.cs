using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.SharedNS.SuccessEnumNS
{
    public interface IHasSuccessEnum
    {
        SuccessENUM SuccessEnum { get; set; }
    }
}
