using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.RightsNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.StateNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary
{
    public partial class UomLengthBiz : BusinessLayer<UomLength>
    {

        public UomLengthBiz(IRepositry<UomLength> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }




    }
}
