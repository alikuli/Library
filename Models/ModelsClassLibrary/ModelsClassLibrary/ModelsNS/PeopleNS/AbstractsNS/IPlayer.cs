using Microsoft.AspNet.Identity;
using ModelsClassLibrary.Models.AddressNS;
namespace InterfacesLibrary.Interfaces.PeopleNS
{
    public interface IPlayer
    {
        IAddressWithTownClass AddressFromUser { get; }
        string FullName();
        bool IsAllowedToShip { get; set; }
        bool IsBlackListed { get; set; }
        bool IsSuspended { get; set; }
        void LoadFrom(IPlayer p);
        string MakeUniqueName();
        void SelfErrorCheck();
        IUser User { get; set; }
        string UserId { get; set; }
    }
}
