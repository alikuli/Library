﻿using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.StateNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary
{
    public partial class UomQuantityBiz : BusinessLayer<UomQty>
    {

        public UomQuantityBiz(IRepositry<UomQty> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }





    }
}
