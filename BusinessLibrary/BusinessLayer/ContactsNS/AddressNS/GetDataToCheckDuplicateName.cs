using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InvoiceNS;
using MigraDocLibrary;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using System.Linq;
namespace UowLibrary.AddressNS
{
    public partial class AddressBiz 
    {

        /// <summary>
        /// The domain data for this can be narowed so that the search takes place
        /// between bounds as sometimes is required. Eg. Same user cannot have a duplicate address, 
        /// but other users can have the same address with a different record.
        /// </summary>
        //public override System.Linq.IQueryable<AddressMain> GetDataToCheckDuplicateName(AddressMain entity)
        //{
        //    var dataSet = FindAll().Where(x => x.PersonId== entity.PersonId);
        //    return dataSet;

        //}

    }
}
