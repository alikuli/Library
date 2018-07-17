
namespace BreadCrumbsLibraryNS.Models
{
    public class BreadCrumb
    {
        public BreadCrumb(string currentUrl, string linkName)
        {
            CurrentUrl = currentUrl;
            LinkName = linkName;
        }

        public string CurrentUrl { get; set; }
        public string LinkName { get; set; }
    }
}
