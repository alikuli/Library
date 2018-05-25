using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Collections.Generic;

namespace ModelsClassLibrary.ViewModels
{
    /// <summary>
    /// This contains all the items that are required extra to run menus' I have tried to use as much of the old machinery
    /// in IndexListVM and IndexItemVM.
    /// </summary>
    public class MenuModel
    {
        public MenuModel()
        {
            ProductList = new List<Product>();
            ProductCategoryMain = new ProductCategoryMain();
        }

        public List<Product> ProductList { get; set; }

        public MenuLevelENUM MenuLevelEnum { get; set; }
        public bool IsMenu { get; set; }

        public string ProductCategory1_Id
        {
            get
            {
                throwErrorIfProductCategoryMain();
                return ProductCategoryMain.ProductCat1Id;
            }

        }

        private void throwErrorIfProductCategoryMain()
        {
            if (ProductCategoryMain.IsNull())
            {
                throw new Exception("ProductCategoryMain is null. Programming Error");
            }
        }

        public string ProductCategory2_Id
        {
            get
            {
                throwErrorIfProductCategoryMain();
                return ProductCategoryMain.ProductCat2Id;
            }

        }

        public string ProductCategory3_Id
        {
            get
            {
                throwErrorIfProductCategoryMain();
                return ProductCategoryMain.ProductCat3Id;
            }

        }


        public ProductCategoryMain ProductCategoryMain { get; set; }

        public string Cat1Name
        {
            get
            {
                throwErrorIfProductCategoryMain();

                if (ProductCategoryMain.ProductCat1.IsNull())
                    return "";

                return ProductCategoryMain.ProductCat1.Name;
            }
        }

        public string Cat2Name
        {
            get
            {
                throwErrorIfProductCategoryMain();

                if (ProductCategoryMain.ProductCat2.IsNull())
                    return "";

                return ProductCategoryMain.ProductCat2.Name;
            }
        }

        public string Cat3Name
        {
            get
            {
                throwErrorIfProductCategoryMain();

                if (ProductCategoryMain.ProductCat3.IsNull())
                    return "";

                return ProductCategoryMain.ProductCat3.Name;
            }
        }

        //This gets the previous menu level
        public MenuLevelENUM GetPreviousMenuLevel()
        {
            //for the sort we need to go back one menu level
            MenuLevelENUM previousMenuLevel = MenuLevelENUM.unknown;
            switch (MenuLevelEnum)
            {
                case MenuLevelENUM.unknown:
                    break;
                case MenuLevelENUM.Level_1:
                    break;
                case MenuLevelENUM.Level_2:
                    previousMenuLevel = MenuLevelENUM.Level_1;

                    break;
                case MenuLevelENUM.Level_3:
                    previousMenuLevel = MenuLevelENUM.Level_2;
                    break;
                case MenuLevelENUM.Level_4:
                    previousMenuLevel = MenuLevelENUM.Level_3;
                    break;
                default:
                    break;
            }

            return previousMenuLevel;

        }
    }
}
