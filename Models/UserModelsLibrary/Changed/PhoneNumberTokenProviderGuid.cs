using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace UserModelsLibrary.Models.Changed
{

    // Summary:
    //     TokenProvider that generates tokens from the user's security stamp and notifies
    //     a user via their phone number
    //
    // Type parameters:
    //   TUser:
    public class PhoneNumberTokenProviderGuid<TUser> : PhoneNumberTokenProvider<TUser, Guid> where TUser : class, global::Microsoft.AspNet.Identity.IUser<Guid>
    {
        public PhoneNumberTokenProviderGuid() : base() { }
    }

}