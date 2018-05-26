using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.ProductNS.Category
{
    [ComplexType]
    public class Picture
    {
        public Picture()
        {
            Created = new DateAndByComplex();
        }

        [NotMapped]
        public HttpPostedFileBase PictureFile { get; set; }

        public string Text { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string UrlString { get; set; }

        public DateAndByComplex Created { get; set; }

    }
}
