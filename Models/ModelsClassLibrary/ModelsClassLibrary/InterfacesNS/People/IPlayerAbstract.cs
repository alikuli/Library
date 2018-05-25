using ModelsClassLibrary.Models;
using ModelsClassLibrary.Interfaces;
using System;
using ModelsClassLibrary.Models.AddressNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;
namespace ModelsClassLibrary.Models.PeopleNS.Abstracts
{
    public interface IPlayerAbstract : ICommonWithId
    {
        IUser User { get; set; }
        string UserId { get; set; }

        IAddressWithTownClass AddressFromUser{get;}

    }
}
