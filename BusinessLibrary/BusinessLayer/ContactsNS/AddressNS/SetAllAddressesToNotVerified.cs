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
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using InterfacesLibrary.SharedNS;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz 
    {
        public void SetAllAddressesToNotVerified(GlobalObject globalObject)
        {
            var addressLst = FindAll().ToList();

            if (addressLst.IsNullOrEmpty())
                return;

            foreach (var item in addressLst)
            {
                item.Verification.VerificaionStatusEnum = VerificaionStatusENUM.NotVerified;

                ControllerCreateEditParameter param = new ControllerCreateEditParameter();
                param.Entity = item as ICommonWithId;
                param.GlobalObject = globalObject;


                UpdateAndSave(param);
            }



        }


    }
}
