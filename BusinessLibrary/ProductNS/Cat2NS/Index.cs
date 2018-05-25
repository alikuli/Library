﻿using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System.Linq;
namespace UowLibrary.ProductNS
{
    public partial class ProductCat2Biz : BusinessLayer<ProductCategory2>
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Product Category 2";
            //indexListVM.MainHeading = "Product Category 2";
            indexListVM.IsImageTiled = true;
            indexListVM.Show.EditDeleteAndCreate = true;

        }




    }
}
