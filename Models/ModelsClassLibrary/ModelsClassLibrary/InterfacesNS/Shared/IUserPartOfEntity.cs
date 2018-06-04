
using UserModels;
namespace ModelsClassLibrary.InterfacesNS.Shared
{
    public interface IUserPartOfEntity
    {
        string UserId { get; set; }
        ApplicationUser User { get; set; }

    }
}
