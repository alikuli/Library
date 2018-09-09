using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketPlace.Web6.Models
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