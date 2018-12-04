using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace UowLibrary.PlayersNS.PlayerAbstractCategoryNS
{

    ///Player names will be the username. This is ensured in fix.
    ///Players are tesxted for being black listed here. 
    ///Player blacklist and suspended is presisted in Users
    ///For a player, we need at least ONE address. 
    ///In the case of customers, we will make an exception because we will force the customer to enter address.
    ///During billing/invoicing we need to be very very strict.
    public abstract partial class BusinessLayerPlayer<TEntity>
    {

        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {
            // errIfNotLoggedIn();

            var lst = (await base.GetListForIndexAsync(parms));

            if (lst.IsNullOrEmpty())
                return null;


            var lstIcommonwithId = (lst
                .Cast<TEntity>()
                .Where(x => x.UserId == UserId))
                .Cast<ICommonWithId>().ToList();

            return lstIcommonwithId;
        }
    }
}
