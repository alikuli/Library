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
                foreach (ProductInitializerHelper prodInitHelper in dataList)
                {
                    Product p = new Product();

                    p.UomVolume = _uomVolumeBiz.FindByName(prodInitHelper.UomVolumeName);
                    if (p.UomVolume.IsNull())
                    {
                        ErrorsGlobal.Add("UomVolume is null", MethodBase.GetCurrentMethod());
                    }

                    p.UomDimensions = _uomLengthBiz.FindByName(prodInitHelper.UomLengthName);
                    if (p.UomDimensions.IsNull())
                    {
                        ErrorsGlobal.Add("UomPackageLength is null", MethodBase.GetCurrentMethod());
                    }


                    p.UomWeightActual = _uomWeightBiz.FindByName(prodInitHelper.UomShipWeightName);
                    if (p.UomWeightActual.IsNull())
                    {
                        ErrorsGlobal.Add("UomShipWeight is null", MethodBase.GetCurrentMethod());
                    }

                    p.UomWeightListed = _uomWeightBiz.FindByName(prodInitHelper.UomWeightListedName);
                    if (p.UomWeightListed.IsNull())
                    {
                        ErrorsGlobal.Add("UomWeightListedName is null", MethodBase.GetCurrentMethod());
                    }




                    p.UomPurchase = _uomQuantityBiz.FindByName(prodInitHelper.UomPurchaseName);
                    if (p.UomPurchase.IsNull())
                    {
                        ErrorsGlobal.Add("UomPurchaseName is null", MethodBase.GetCurrentMethod());
                    }
                    p.UomSale = _uomQuantityBiz.FindByName(prodInitHelper.UomSaleName);
                    if (p.UomSale.IsNull())
                    {
                        ErrorsGlobal.Add("UomSaleName is null", MethodBase.GetCurrentMethod());
                    }

                    #region Menu Path

                    if (!prodInitHelper.Menupaths.IsNullOrEmpty())
                    {
                        foreach (MenuPathHelper mph in prodInitHelper.Menupaths)
                        {
                            MenuPathMain mpm = MenuPathMainBiz.FindAll().FirstOrDefault(x =>
                                    x.MenuPath1.Name.ToLower() == mph.MenuPath1Name.ToLower() &&
                                    x.MenuPath2.Name.ToLower() == mph.MenuPath2Name.ToLower());

                            if (mpm.IsNull())
                            {
                                mpm = MenuPathMainBiz.Factory();

                                MenuPath1 mp1 = MenuPath1Biz.FindByName(mph.MenuPath1Name);
                                if (mp1.IsNull())
                                {
                                    ErrorsGlobal.Add("Menu Path 1 is null", MethodBase.GetCurrentMethod());
                                    ErrorsGlobal.Add(string.Format("Programming error. Menu Path 1 does not exist... it should!. Path is: {0}", mph.MenuPath1Name), MethodBase.GetCurrentMethod());
                                    throw new Exception(ErrorsGlobal.ToString());
                                }
                                mpm.MenuPath1 = mp1;
                                mpm.MenuPath1.Id = mp1.Id;

                                MenuPath2 mp2 = MenuPath2Biz.FindByName(mph.MenuPath2Name);
                                if (mp2.IsNull())
                                {
                                    ErrorsGlobal.Add("Menu Path 2 is null", MethodBase.GetCurrentMethod());
                                }
                                mpm.MenuPath2 = mp2;
                                mpm.MenuPath2.Id = mp2.Id;

                                mpm.Name = mpm.MakeName(mph.MenuPath1Name, mph.MenuPath2Name, "");

                                //try
                                //{
                                //    MenuPathMainBiz.CreateAndSave(mpm);

                                //}
                                //catch (NoDuplicateException)
                                //{
                                //    ErrorsGlobal.Add(string.Format("Programming error. Duplicate Menu Path Main. Item is: {0}", mpm), MethodBase.GetCurrentMethod());
                                //    throw new Exception(ErrorsGlobal.ToString());
                                //}

                            }

                            p.MenuPathMains.Add(mpm);
                            mpm.Products.Add(p);


                        }
                    }
                    #endregion


                    if (ErrorsGlobal.HasErrors)
                    {
                        throw new Exception(ErrorsGlobal.ToString());

                    }



                    p.Name = prodInitHelper.Name;
                    p.Dimensions.Height = prodInitHelper.Height;
                    p.Dimensions.Width = prodInitHelper.Width;
                    p.Dimensions.Length = prodInitHelper.Length;
                    p.UomDimensionsId = p.UomDimensions.Id;
                    p.UomVolumeId = p.UomVolume.Id;
                    p.UomDimensionsId = p.UomDimensions.Id;
                    p.WeightActual = prodInitHelper.ShipWeight;
                    p.UomWeightActualId = p.UomWeightActual.Id;

                    p.UomWeightListedId = p.UomWeightListed.Id;
                    p.UomPurchaseId = p.UomPurchase.Id;
                    p.UomSaleId = p.UomSale.Id;

                    p.WeightListed = prodInitHelper.WeightListed;
                    p.Volume = prodInitHelper.ShipVolume;


                    #region ProductIdentifier
                    if (!prodInitHelper.ProductIdentifiers.IsNullOrEmpty())
                    {
                        foreach (string piStr in prodInitHelper.ProductIdentifiers)
                        {
                            //if it does then add a message

                            //first look for the product identifier.
                            ProductIdentifier pi = ProductIdentifierBiz.Find(piStr);

                            //if it already exists, STOP.
                            if (!pi.IsNull())
                                continue;

                            pi = ProductIdentifierBiz.Factory();

                            pi.Name = piStr;
                            pi.Product = p;
                            pi.ProductId = p.Id;

                            p.ProductIdentifiers.Add(pi);
                        }
                    }

                    CreateSave_ForInitializeOnly(p);

                    #endregion

                }


            }
        }

        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }

    }
}
