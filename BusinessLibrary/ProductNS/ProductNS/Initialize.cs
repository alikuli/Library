using AliKuli.Extentions;
using DatastoreNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UserModels;

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

        public List<ProductChildInitializer> GetDataListForProductChild
        {
            get
            {
                return new ProductChildtDataArray().DataArray();
            }
        }

        public override void AddInitData()
        {
            addProducts();
            addProductChildren();
        }

        private void addProductChildren()
        {
            //get the data
            List<ProductChildInitializer> dataList = GetDataListForProductChild;

            if (dataList.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("No data available.", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }


            foreach (ProductChildInitializer item in dataList)
            {
                Product p = FindByName(item.ParentName);
                p.IsNullThrowException("Parent Product not found! Programming error.");

                //check for duplicates.
                if (!p.ProductChildren.IsNull())
                {
                    ProductChild pFound = p.ProductChildren.FirstOrDefault(x => x.Name.ToLower() == item.ProductName.ToLower());
                    if (!pFound.IsNull())
                        continue;

                }
                ProductChild pc = new ProductChild();
                pc.Name = item.ProductName;

                getUser(item, pc);
                getParent(item, pc);
                getPicture(item, pc);

            }
            SaveChanges();
        }

        private void getPicture(ProductChildInitializer item, ProductChild pc)
        {

            IHasUploads pcHasuploads = pc as IHasUploads;
            pc.IsNullThrowException(string.Format("Programming Error. Product Child is not showing as IHasUploads. It is. Currently initializing '{0}'", item.ProductName));

            string originalname = item.ProductName.RemoveAllSpaces().ToString();
            string relative_SrcPath = pc.MiscFilesLocation_Initialization();
            string relative_targetPath = pc.MiscFilesLocation();

            string filenameNoExtention = getFileNameWithoutExtention(relative_SrcPath, originalname);


            if (!imageFileExists(filenameNoExtention))
                return;

            #region MyRegion
            string originalnameWithoutExtention = originalname;
            List<UploadedFile> uploadedFileLst = new List<UploadedFile>();

            //copy the actual file to the new spot. We need to do it here so we can get it's new name
            //== COPY FILE
            string newNameWithMappedPathPlusExtention = CopyFile(relative_SrcPath, relative_targetPath, Path.ChangeExtension(originalnameWithoutExtention, ExtentionFound));

            //create the upload file
            UploadedFile uf = new UploadedFile(
                originalnameWithoutExtention,
                Path.GetFileNameWithoutExtension(newNameWithMappedPathPlusExtention),
                ExtentionFound,
                relative_targetPath);

            //add to uploadlist
            uploadedFileLst.Add(uf);

            #endregion
            if (!uploadedFileLst.IsNullOrEmpty())
            {
                foreach (UploadedFile file in uploadedFileLst)
                {
                    file.MetaData.Created.SetToTodaysDate(UserNameBiz);


                    //initializes navigation if it is null

                    //You need to add a refrence here to save the file in the UploadedFile as well.
                    file.ProductChild = pc;
                    file.ProductChildId = pc.Id;

                    if (pcHasuploads.MiscFiles.IsNull())
                        pcHasuploads.MiscFiles = new List<UploadedFile>(); //intializing

                    pcHasuploads.MiscFiles.Add(file);

                    _uploadedFileBiz.Create(file);

                }
            }
        }

        private void getParent(ProductChildInitializer item, ProductChild pc)
        {
            //Get the parent;
            Product p = FindByName(item.ParentName);
            p.IsNullThrowException("Product Not found. Initialization Data error.");

            pc.Product = p;
            pc.ProductId = p.Id;

            if (p.ProductChildren.IsNull())
                p.ProductChildren = new List<ProductChild>();

            p.ProductChildren.Add(pc);

        }




        private void getUser(ProductChildInitializer item, ProductChild pc)
        {
            //get user
            ApplicationUser user = UserDal.FindAll().FirstOrDefault(x =>
                x.UserName.ToLower() == item.UserName.ToLower());

            user.IsNullThrowException(string.Format("User '{0}' Not found. Erronious starting data.", item.UserName));

            pc.User = user;
            pc.UserId = user.Id;

            if (user.ProductChildren.IsNull())
                user.ProductChildren = new List<ProductChild>();

            user.ProductChildren.Add(pc);
        }

        private void addProducts()
        {
            //get the data
            List<ProductInitializerHelper> dataList = GetDataListForProduct;

            if (!dataList.IsNullOrEmpty())
            {
                foreach (ProductInitializerHelper prodInitHelper in dataList)
                {
                    //check to see if the product exists... if it does continue.
                    Product p = FindByName(prodInitHelper.Name);

                    if (!p.IsNull())
                        continue;

                    p = new Product();

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
                            }

                            p.MenuPathMains.Add(mpm);
                            mpm.Products.Add(p);


                        }
                    }
                    #endregion

                    #region Get Product Uploads
                    //this comes automaticly.
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


                    #endregion
                    CreateSave_ForInitializeOnly(p);

                }


            }
        }

        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }

    }
}
