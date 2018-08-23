using AliKuli.Extentions;
using DatastoreNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
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

            #region Copy File
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
                    file.MetaData.Created.SetToTodaysDate(UserName);


                    //initializes navigation if it is null

                    //You need to add a refrence here to save the file in the UploadedFile as well.
                    file.ProductChild = pc;
                    file.ProductChildId = pc.Id;

                    if (pcHasuploads.MiscFiles.IsNull())
                        pcHasuploads.MiscFiles = new List<UploadedFile>(); //intializing

                    pcHasuploads.MiscFiles.Add(file);

                    _uploadedFileBiz.Create(CreateControllerCreateEditParameter(file as ICommonWithId));

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
            ApplicationUser user = UserBiz.FindAll().FirstOrDefault(x =>
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

                    p.UomVolume = UomVolumeBiz.FindByName(prodInitHelper.UomVolumeName);
                    p.UomVolume.IsNullThrowException();
                    
                    p.UomDimensions = UomLengthBiz.FindByName(prodInitHelper.UomLengthName);
                    p.UomDimensions.IsNullThrowException();

                    p.UomWeightActual = UomWeightBiz.FindByName(prodInitHelper.UomShipWeightName);


                    p.UomWeightListed = UomWeightBiz.FindByName(prodInitHelper.UomWeightListedName);
                    p.UomWeightListed.IsNullThrowException();

                    p.UomPurchase = UomQuantityBiz.FindByName(prodInitHelper.UomPurchaseName);
                    p.UomPurchase.IsNullThrowException();

                    p.UomSale = UomQuantityBiz.FindByName(prodInitHelper.UomSaleName);
                    p.UomSale.IsNullThrowException();

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

                    if (ErrorsGlobal.HasErrors)
                    {
                        throw new Exception(ErrorsGlobal.ToString());

                    }



                    #region Menu Path
                    
                    prodInitHelper.Menupaths.IsNullOrEmptyThrowException();

                    foreach (MenuPathHelper mph in prodInitHelper.Menupaths)
                    {
                        MenuPathMain mpm = MenuPathMainBiz.FindAll().FirstOrDefault(x =>
                                x.MenuPath1.Name.ToLower() == mph.MenuPath1Name.ToLower() &&
                                x.MenuPath2.Name.ToLower() == mph.MenuPath2Name.ToLower() &&
                                x.MenuPath3.Name.ToLower() == mph.MenuPath3Name.ToLower());

                        mpm.IsNullThrowException();

                        if (p.MenuPathMains.IsNull())
                            p.MenuPathMains = new List<MenuPathMain>();

                        p.MenuPathMains.Add(mpm);

                        if (mpm.Products.IsNull())
                            mpm.Products = new List<Product>();

                        mpm.Products.Add(p);
                    }

                    #endregion






                    #region ProductIdentifier
                    prodInitHelper.ProductIdentifiers.IsNullOrEmptyThrowException();

                    foreach (string piStr in prodInitHelper.ProductIdentifiers)
                    {
                        ProductIdentifier pi = ProductIdentifierBiz.Find(piStr);

                        if (!pi.IsNull())
                            throw new Exception(string.Format("Duplicate product Identifier: '{0}'", piStr));

                        pi = ProductIdentifierBiz.Factory() as ProductIdentifier;

                        pi.Name = piStr;
                        pi.Product = p;
                        pi.ProductId = p.Id;

                        if (p.ProductIdentifiers.IsNull())
                            p.ProductIdentifiers = new List<ProductIdentifier>();

                        p.ProductIdentifiers.Add(pi);
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
