using AliKuli.Extentions;
using DatastoreNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Collections.Generic;
using System.Linq;
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


                    p.UomWeightActual = _uomWeightBiz.FindByName(item.UomShipWeightName);
                    if (p.UomWeightActual.IsNull())
                    {
                        ErrorsGlobal.Add("UomShipWeight is null", MethodBase.GetCurrentMethod());
                    }

                    p.UomWeightListed = _uomWeightBiz.FindByName(item.UomWeightListedName);
                    if (p.UomWeightListed.IsNull())
                    {
                        ErrorsGlobal.Add("UomWeightListedName is null", MethodBase.GetCurrentMethod());
                    }




                    p.UomPurchase = _uomQuantityBiz.FindByName(item.UomPurchaseName);
                    if (p.UomPurchase.IsNull())
                    {
                        ErrorsGlobal.Add("UomPurchaseName is null", MethodBase.GetCurrentMethod());
                    }
                    p.UomSale = _uomQuantityBiz.FindByName(item.UomSaleName);
                    if (p.UomSale.IsNull())
                    {
                        ErrorsGlobal.Add("UomSaleName is null", MethodBase.GetCurrentMethod());
                    }

                    if (!item.Menupaths.IsNullOrEmpty())
                    {
                        foreach (MenuPathHelper mph in item.Menupaths)
                        {
                            MenuPathMain mpm = MenuPathMainBiz.FindAll().FirstOrDefault(x =>
                                    x.MenuPath1.Name.ToLower() == mph.MenuPath1Name.ToLower() &&
                                    x.MenuPath2.Name.ToLower() == mph.MenuPath2Name.ToLower());

                            if (mpm.IsNull())
                            {
                                MenuPath1 mp1 = MenuPath1Biz.Find(mph.MenuPath1Name);
                                if (mp1.IsNull())
                                {
                                    ErrorsGlobal.Add("Menu Path 1 is null", MethodBase.GetCurrentMethod());
                                    ErrorsGlobal.Add(string.Format("Programming error. Menu Path 1 does not exist... it should!. Path is: {0}", mph.MenuPath1Name), MethodBase.GetCurrentMethod());
                                    throw new Exception(ErrorsGlobal.ToString());
                                }
                                mpm.MenuPath1 = mp1;
                                mpm.MenuPath1.Id = mp1.Id;

                                MenuPath2 mp2 = MenuPath2Biz.Find(mph.MenuPath2Name);
                                if (mp2.IsNull())
                                {
                                    ErrorsGlobal.Add("Menu Path 2 is null", MethodBase.GetCurrentMethod());
                                }
                                mpm.MenuPath2 = mp2;
                                mpm.MenuPath2.Id = mp2.Id;

                                mpm.Name = mpm.MakeName(mph.MenuPath1Name, mph.MenuPath2Name, "");

                                try
                                {
                                    MenuPathMainBiz.CreateAndSave(mpm);

                                }
                                catch (NoDuplicateException)
                                {
                                    ErrorsGlobal.Add(string.Format("Programming error. Duplicate Menu Path Main. Item is: {0}", mpm), MethodBase.GetCurrentMethod());
                                    throw new Exception(ErrorsGlobal.ToString());
                                }

                            }

                            p.MenuPathMains.Add(mpm);
                            mpm.Products.Add(p);


                        }
                    }

                    if (ErrorsGlobal.HasErrors)
                    {
                        throw new Exception(ErrorsGlobal.ToString());

                    }



                    p.Name = item.Name;
                    p.Dimensions.Height = item.Height;
                    p.Dimensions.Width = item.Width;
                    p.Dimensions.Length = item.Length;
                    p.UomDimensionsId = p.UomDimensions.Id;
                    p.UomVolumeId = p.UomVolume.Id;
                    p.UomDimensionsId = p.UomDimensions.Id;
                    p.WeightActual = item.ShipWeight;
                    p.UomWeightActualId = p.UomWeightActual.Id;

                    p.UomWeightListedId = p.UomWeightListed.Id;
                    p.UomPurchaseId = p.UomPurchase.Id;
                    p.UomSaleId = p.UomSale.Id;

                    p.WeightListed = item.WeightListed;
                    p.Volume = item.ShipVolume;
                    Create_ForInitializeOnly(p);
                }


            }
        }

        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }

    }
}
