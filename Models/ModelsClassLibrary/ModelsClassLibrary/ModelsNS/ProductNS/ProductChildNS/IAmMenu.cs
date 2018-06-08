using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductChildNS
{
    public partial class ProductChild 
    {
        [NotMapped]
        public string MenuPath1Id { get; set; }

        [NotMapped]
        public string MenuPath2Id { get; set; }

        [NotMapped]
        public string MenuPath3Id { get; set; }


    }





}
