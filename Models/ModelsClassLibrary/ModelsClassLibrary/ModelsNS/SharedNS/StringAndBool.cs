using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [NotMapped]
    public class StringAndBool
    {
        public StringAndBool()
        {

        }

        public StringAndBool(string str1, bool select):this()
        {
            Str1 = str1;
            Select = select;
        }
        public string Str1 { get; set; }
        public bool Select { get; set; }
    }
}
