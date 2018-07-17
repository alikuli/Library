using BreadCrumbsLibraryNS.Models;
namespace BreadCrumbsLibraryNS.Programs
{
    public interface IBreadCrumbManager
    {
        int Count();
        bool IsNullOrEmpty();
        BreadCrumb Pop();
        void Push(BreadCrumb bc);
        BreadCrumb[] ToArray();
        System.Collections.Generic.List<BreadCrumb> ToList();
    }
}
