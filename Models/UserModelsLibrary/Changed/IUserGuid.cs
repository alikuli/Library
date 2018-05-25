using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace UserModelsLibrary.Models.Changed
{
    public interface IUserGuid : IUser<Guid>
    {
    }

    // Summary:
    //     Minimal interface for a user with id and username
    //
    // Type parameters:
    //   TKey:

    public interface IUserGuid<out TKey>
    {
        // Summary:
        //     Unique key for the user
        TKey Id { get; }
        //
        // Summary:
        //     Unique username
        string UserName { get; set; }
    }
}
