using System;
using System.Linq;
using AliKuli.Extentions;
using InterfacesLibrary.AddressNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;

using ApplicationDbContextNS;
using ErrorHandlerLibrary;

namespace DalLibrary.DalNS
{
    public partial class AddressDAL : Repositry<AddressWithId>
    {

        public AddressDAL(ApplicationDbContext db, IErrorSet errorSet)
            : base(db, errorSet)
        {
        }


    }
}

