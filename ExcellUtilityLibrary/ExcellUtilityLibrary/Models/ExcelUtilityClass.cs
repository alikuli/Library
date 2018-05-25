using System.Collections.Generic;
using System.Linq;
using AliKuli.Extentions;

namespace AliKuli.UtilityNS.Excell
{
    public class ExcelUtilityClass
    {
        public ICollection<string> Data { get; set; }
        public ICollection<string> ColumnNames { get; set; }
        public int NoOfCols
        {
            get
            {
                if (  ColumnNames.IsNull())
                    return 0;

                return ColumnNames.Count();
            }
        }
    }
}