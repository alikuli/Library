using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
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

        //public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        //{
        //    // errIfNotLoggedIn();
        //    //throw new NotImplementedException();
        //    var lstEntities = await FindAllAsync();

        //    if (lstEntities.IsNullOrEmpty())
        //        return null;

        //    string personId = GetPersonIdForCurrentUser();

        //    Person person = UserBiz.GetPersonFor(UserId);
        //    person.IsNullThrowException("Person not found");

        //    var lstIcommonwithId = (lstEntities
        //        .Cast<AddressMain>()
        //        .Where(x => x.People.Any(y => y.Id == personId)))
        //        .Cast<ICommonWithId>().ToList();

        //    return lstIcommonwithId;
        //}
    }
}
