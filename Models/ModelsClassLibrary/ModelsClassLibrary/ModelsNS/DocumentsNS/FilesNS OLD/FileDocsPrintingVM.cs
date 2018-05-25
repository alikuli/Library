using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS
{
    public class FileDocsPrintingVM
    {
        public FileDocsPrintingVM()
        {
            Files = new List<FilesDetail>();
        }
        public string CategoryName { get; set; }
        public ICollection<FilesDetail> Files { get; set; }
        public bool Selected { get; set; }

    }



}