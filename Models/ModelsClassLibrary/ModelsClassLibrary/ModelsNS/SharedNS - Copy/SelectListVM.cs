using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS
{
    [NotMapped]
    public class SelectListVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}