using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;
using UowLibrary.ParametersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.PlayersNS.DeliverymanCategoryNS
{
    public partial class DeliverymanCategoryBiz : BusinessLayer<DeliverymanCategory>
    {
        public DeliverymanCategoryBiz(IRepositry<DeliverymanCategory> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }



    }
}
