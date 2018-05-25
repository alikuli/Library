using EnumLibrary.EnumNS;
using AliKuli.Extentions;
namespace ModelClassLibrary.MigraDocNS
{
    public class PdfHeaderInfo
    {
        public PdfHeaderInfo()
        {

        }
        public PdfHeaderInfo(string title, string subject, string author )
        {
            Title = title;
            Subject = subject;
            Author = author;
        }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }





    }
}
