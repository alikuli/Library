using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS
{
    public class IdentityUserLoginGuid : IdentityUserLogin<Guid>
    {
        public IdentityUserLoginGuid() : base() { }


    }
}