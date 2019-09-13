using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS
{
    [ComplexType]
    public class CommissionComplex : DecimalWithDateComplex
    {
        public decimal Percent { get; set; }
    }
}
