using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace UserModelsLibrary.Models.Changed
{

    // Summary:
    //     Token provider that uses an IDataProtector to generate encrypted tokens based
    //     off of the security stamp
    public class DataProtectorTokenProviderGuid<TUser> : DataProtectorTokenProvider<TUser, Guid> where TUser : class, global::Microsoft.AspNet.Identity.IUser<Guid>
    {
        // Summary:
        //     Constructor
        //
        // Parameters:
        //   protector:
        public DataProtectorTokenProviderGuid(IDataProtector protector) : base(protector) { }
    }

}