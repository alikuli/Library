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
    /// <summary>
    /// Email is not presised. You need to move it to the db.
    /// Contact Phone is not presisted. Move it to the Db.
    /// </summary>
    public partial class AddressDAL
    {


        public override IQueryable<AddressWithId> GetDomainDataForDuplicateNameSearch(AddressWithId addy)
        {
            var dataSet = FindAll().Where(x => x.UserId == addy.UserId);
            return dataSet;
        }

    }
}