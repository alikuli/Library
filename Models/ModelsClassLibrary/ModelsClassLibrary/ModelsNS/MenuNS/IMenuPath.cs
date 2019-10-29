using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System.Collections.Generic;
namespace ModelsClassLibrary.ModelsNS.MenuNS
{
    public interface IMenuPath
    {
        string Name { get; set; }
        ICollection<MenuFeature> MenuFeatures { get; set; }
        List<MenuPathMain> MenuPathMains_Fixed { get; }
    }
}
