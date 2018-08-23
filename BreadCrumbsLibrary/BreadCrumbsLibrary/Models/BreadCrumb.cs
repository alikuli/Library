
namespace BreadCrumbsLibraryNS.Models
{
    public class BreadCrumb
    {
        public BreadCrumb(string url, string linkName)
        {
            Url = url;
            LinkName = linkName;
        }

        public string Url { get; set; }
        public string LinkName { get; set; }
    }
}
