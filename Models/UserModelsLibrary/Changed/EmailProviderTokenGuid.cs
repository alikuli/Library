using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace UserModelsLibrary.Models.Changed
{
    // Summary:
    //     TokenProvider that generates tokens from the user's security stamp and notifies
    //     a user via their email
    //
    // Type parameters:
    //   TUser:
    public class EmailProviderTokenGuid<TUser> : EmailTokenProvider<TUser, Guid> where TUser : class, global::Microsoft.AspNet.Identity.IUser<Guid>
    {
        public EmailProviderTokenGuid() : base() { }
    }

}