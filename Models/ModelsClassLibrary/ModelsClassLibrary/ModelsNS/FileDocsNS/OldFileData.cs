using System;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS
{
    public class OldFileData
    {
        public string CompleteFileNumber { get; set; }
        public string ParentFileNumber { get; set; }
        public float ChildFilenumber { get; set; }
        public string ParentDescription { get; set; }
        public float ChildNo { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public DateTime CreationDate { get; set; }
        public float FileId { get; set; }

        [Key]
        public int Id { get; set; }

    }
}
