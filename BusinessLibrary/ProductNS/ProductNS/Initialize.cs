using AliKuli.Extentions;
using DatastoreNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace UowLibrary.ProductNS
{
    public partial class ProductBiz 
    {
        public List<ProductInitializerHelper> GetDataListForProduct
        {
            get
            {
                return new ProductDataArray().DataArray();
            }
        }

        public override void AddInitData()
        {
            //get the data
            List<ProductInitializerHelper> dataList = GetDataListForProduct;

            if (!dataList.IsNullOrEmpty())
            {
                foreach (var item in dataList)
                {
                    Product p = new Product();


                    p.UomWeightActual = _uomWeightBiz.FindByName(item.UomShipWeightName);
                    if (p.UomWeightActual.IsNull())
                    {
                        ErrorsGlobal.Add("UomShipWeight is null", MethodBase.GetCurrentMethod());
                    }

                    p.UomVolume = _uomVolumeBiz.FindByName(item.UomVolumeName);
                    if (p.UomVolume.IsNull())
                    {
                        ErrorsGlobal.Add("UomVolume is null", MethodBase.GetCurrentMethod());
                    }

                    p.UomDimensions = _uomLengthBiz.FindByName(item.UomLengthName);
                    if (p.UomDimensions.IsNull())
                    {
                        ErrorsGlobal.Add("UomPackageLength is null", MethodBase.GetCurrentMethod());
                    }

                    //p.UomStock = _uomQuantityBiz.FindByName(item.UomStock);
                    //if (p.UomStock.IsNull())
                    //{
                    //    ErrorsGlobal.Add("UomStock is null", MethodBase.GetCurrentMethod());
                    //}

                    if (ErrorsGlobal.HasErrors)
                    {
                        throw new Exception(ErrorsGlobal.ToString());

                    }
                    p.Name = item.Name;
                    p.Dimensions.Height = item.Height;
                    p.Dimensions.Width = item.Width;
                    p.Dimensions.Length = item.Length;
                    p.WeightActual = item.ShipWeight;
                    p.UomWeightActualId = p.UomWeightActual.Id;
                    p.UomDimensionsId = p.UomDimensions.Id;
                    p.UomVolumeId = p.UomVolume.Id;
                    p.UomDimensionsId = p.UomDimensions.Id;
                    //p.UomStockID = p.UomStock.Id;

                    CreateAndSave_ForInitializeOnly(p);
                }


            }
        }

        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }

    }
}
