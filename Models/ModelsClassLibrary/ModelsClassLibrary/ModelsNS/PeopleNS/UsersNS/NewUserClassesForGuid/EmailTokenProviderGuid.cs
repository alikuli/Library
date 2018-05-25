using System;
using Microsoft.AspNet.Identity;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS.NewUserClassesForGuid
{
    // Summary:
    //     TokenProvider that generates tokens from the user's security stamp and notifies
    //     a user via their email
    //
    // Type parameters:
    //   TUser:
    public class EmailTokenProviderGuid<TUser> : EmailTokenProvider<TUser, Guid> where TUser : class, global::Microsoft.AspNet.Identity.IUser<Guid>
    {
        public EmailTokenProviderGuid(): base(){}
    }

}