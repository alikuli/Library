using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using System.Linq;

namespace UowLibrary.ProductNS
{
    public partial class MenuPathMainBiz
    {

        public IQueryable<MenuPathMain> FindAllMenuPathMainsFor(MenuPath1ENUM menuPath1Enum)
        {
            var allMenuPaths = FindAll();
            allMenuPaths = allMenuPaths.Where(x => x.MenuPath1.MenuPath1Enum == menuPath1Enum);

            return allMenuPaths;
        }

    }
}
