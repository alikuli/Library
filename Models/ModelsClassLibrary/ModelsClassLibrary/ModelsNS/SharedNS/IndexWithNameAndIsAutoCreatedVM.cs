using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [NotMapped]
    public class IndexWithNameAndIsAutoCreatedVM
    {
        public IndexWithNameAndIsAutoCreatedVM()
        {

        }
        public IndexWithNameAndIsAutoCreatedVM(string id, string name,bool isAutoCreated)
        {
            Initialize(id.ToString(), name, isAutoCreated);
        }



        
        private void Initialize(string id, string name, bool isAutoCreated)
        {
            Id = id;
            Name = name ?? "";
            IsAutoCreated = isAutoCreated;

        }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsAutoCreated { get; set; }

        public override string ToString()
        {
            return string.Format("Id: '{0}'; Name = '{1}'; IsAutoCreated: '{2}'", Id,Name,IsAutoCreated);
        }

    }
}