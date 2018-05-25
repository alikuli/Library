using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AliKuli.Extentions;

namespace ModelsClassLibrary.Models.Shared
{
    public class ExcelUtilityClass
    {
        public ICollection<string> Data { get; set; }
        public ICollection<string> ColumnNames { get; set; }
        public int NoOfCols
        {
            get
            {
                if (ColumnNames.IsNullOrEmpty())
                    return 0;
                
                return ColumnNames.Count();
            }
        }
    }
}