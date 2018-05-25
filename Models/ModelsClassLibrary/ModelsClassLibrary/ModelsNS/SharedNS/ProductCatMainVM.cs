using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [NotMapped]
    public class ProductCatMainVM
    {
        public string Id { get; set; }
        public string ProductCatId1 { get; set; }
        public string ProductCatId2 { get; set; }
        public string ProductCatId3 { get; set; }
    }
}
