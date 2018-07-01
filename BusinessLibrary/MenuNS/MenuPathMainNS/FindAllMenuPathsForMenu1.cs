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

            if (menuPath1Enum != MenuPath1ENUM.Unknown)
            {
                //filter the allMenuPaths
                //find the menuPath1 which holds this
                allMenuPaths = allMenuPaths.Where(x => x.MenuPath1.MenuPath1Enum == menuPath1Enum);
            }
            return allMenuPaths;
        }

    }
}
