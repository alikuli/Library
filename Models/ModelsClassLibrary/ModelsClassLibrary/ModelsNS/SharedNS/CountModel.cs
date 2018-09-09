using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class CountModel
    {
        public int Count { get; set; }
        public override string ToString()
        {
            return Count.ToString() + (Count == 1? "Record" : "Records");
        }
    }
}