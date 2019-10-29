using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System.Collections.Generic;
namespace ModelsClassLibrary.ModelsNS.MenuNS
{
    public interface IHasMenuPaths
    {
        List<MenuPathMain> MenuPathMains_Fixed { get; }
    }
}
