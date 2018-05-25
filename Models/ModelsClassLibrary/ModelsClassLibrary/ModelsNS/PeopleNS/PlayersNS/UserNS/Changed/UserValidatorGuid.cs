using System;
using Microsoft.AspNet.Identity;

namespace UserModelsLibrary.ModelsNS.Changed
{
    // Summary:
    //     Validates users before they are saved
    //
    // Type parameters:
    //   TUser:
    public class UserValidatorGuid<TUser> : UserValidator<TUser, Guid> where TUser : class, global::Microsoft.AspNet.Identity.IUser<Guid>
    {
        // Summary:
        //     Constructor
        //
        // Parameters:
        //   manager:
        public UserValidatorGuid(UserManager<TUser, Guid> manager) : base(manager) { }
    }
}