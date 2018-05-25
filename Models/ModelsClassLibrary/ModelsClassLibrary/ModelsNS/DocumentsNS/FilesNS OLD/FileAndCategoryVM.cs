using AliKuli.UtilitiesNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS
{
    public class FileAndCategoryVM
    {
        public FileAndCategoryVM()
        {

        }
        public FileAndCategoryVM(Guid categoryId, string catName, bool assigned)
        {
            CategoryId = categoryId;
            Assigned = assigned;
            Name = catName;
        }

        public string Name { get; set; }
        public Guid CategoryId { get; set; }

        public bool Assigned { get; set; }

    }
}