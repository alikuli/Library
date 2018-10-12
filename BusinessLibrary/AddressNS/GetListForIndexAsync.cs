using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {




        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {
            // errIfNotLoggedIn();

            var lst = (await base.GetListForIndexAsync(parms));

            if (lst.IsNullOrEmpty())
                return null;


            var lstIcommonwithId = (lst
                .Cast<AddressWithId>()
                .Where(x => x.UserId == UserId))
                .Cast<ICommonWithId>().ToList();

            return lstIcommonwithId;
        }
    }
}
