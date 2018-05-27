using AliKuli.Extentions;
using DatastoreNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace UowLibrary.ProductNS
{
    public partial class ProductBiz : BusinessLayer<Product>
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
                foreach (var h in dataList)
                {
                    Product p = new Product();
                
                    p.Name = h.Name;
                    p.Dimensions.Height = h.Height;
                    p.Dimensions.Width = h.Width;
                    p.Dimensions.Length = h.Length;
                    p.ShipWeight = h.ShipWeight;

                    p.UomShipWeight = _uomWeightBiz.FindByName(h.UomShipWeightName);
                    if(p.UomShipWeight.IsNull())
                    {
                        ErrorsGlobal.Add("UomShipWeight is null", MethodBase.GetCurrentMethod());
                    }

                    p.UomVolume = _uomVolumeBiz.FindByName(h.UomVolumeName);
                    if (p.UomVolume.IsNull())
                    {
                        ErrorsGlobal.Add("UomVolume is null", MethodBase.GetCurrentMethod());
                    }

                    p.UomDimensions = _uomLengthBiz.FindByName(h.UomLengthName);
                    if (p.UomDimensions.IsNull())
                    {
                        ErrorsGlobal.Add("UomPackageLength is null", MethodBase.GetCurrentMethod());
                    }

                    if (ErrorsGlobal.HasErrors)
                    {
                        throw new Exception(ErrorsGlobal.ToString());
                    }

                    p.UomDimensionsId = p.UomDimensions.Id;
                    p.UomVolumeId = p.UomVolume.Id;
                    p.UomDimensionsId = p.UomDimensions.Id;

                    Create(p);
                }


            }
            SaveChanges();
        }

    }
}
