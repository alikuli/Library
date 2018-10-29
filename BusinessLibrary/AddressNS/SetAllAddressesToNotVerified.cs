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
using AliKuli.Extentions;
using EnumLibrary.EnumNS;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz 
    {
        public void SetAllAddressesToNotVerified()
        {
            var addressLst = FindAll().ToList();

            if (addressLst.IsNullOrEmpty())
                return;

            foreach (var item in addressLst)
            {
                item.Verification.VerificaionStatusEnum = VerificaionStatusENUM.NotVerified;
                UpdateAndSave(item);
            }



        }


    }
}
