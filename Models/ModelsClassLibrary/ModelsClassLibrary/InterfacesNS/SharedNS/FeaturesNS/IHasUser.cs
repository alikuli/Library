
using UserModels;
namespace InterfacesLibrary.SharedNS.FeaturesNS

{
    public interface IHasUser
    {
        string UserId { get; set; }
        ApplicationUser User { get; set; }

    }
}
