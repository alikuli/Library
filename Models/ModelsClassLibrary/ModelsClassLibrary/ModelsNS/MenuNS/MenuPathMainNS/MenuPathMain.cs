﻿using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ModelsClassLibrary.MenuNS
{
    /// <summary>
    /// This is the Menu Path
    /// </summary>
    public partial class MenuPathMain : MenuPathAbstract, IHaveMenuManager
    {

        public MenuPathMain()
        {
            //DONT do this!! IT messes with LOADING!!
            //MenuPath1 = new MenuPath1();
            //MenuPath2 = new MenuPath2();
            //MenuPath3 = new MenuPath3();
            //Products = new List<Product>();

            //MenuManager = new MenuManager(Id, this, null, null, EnumLibrary.EnumNS.MenuLevelENUM.unknown, "", false, "", "", "", SortOrderENUM.Item1_Asc, ActionNameENUM.Unknown);


        }
        StringBuilder sb = new StringBuilder(); //used to collect erors

        /// <summary>
        /// if this is marked true, then product children for sale exist.
        /// </summary>
        [NotMapped]
        public bool HasLiveProductChildren { get; set; }

        
        [NotMapped]
        public int NoOfItems { get; set; }
        [NotMapped]
        public int NoOfShops { get; set; }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.MenuPathMain;
        }

        #region Navigation Properties



        //public virtual ICollection<IDiscount> ProductCategoryDiscounts { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        [NotMapped]
        public List<Product> Products_Fixed
        {
            get
            {
                if (Products.IsNullOrEmpty())
                    return new List<Product>();

                List<Product> products = Products.Where(x => x.MetaData.IsDeleted == false).ToList();
                return products;
            }
        }

        [NotMapped]
        public List<Product> Products_Fixed_And_Approved
        {
            get
            {

                List<Product> products = Products_Fixed.Where(x => x.IsUnApproved == false).ToList();
                return products;
            }
        }

        [NotMapped]

        public List<ProductChild> ProductChildren_Fixed_Not_Hidden
        {
            get
            {
                if (Products_Fixed_And_Approved.IsNullOrEmpty())
                    return new List<ProductChild>();

                List<ProductChild> pChildren = new List<ProductChild>();
                foreach (Product product in Products_Fixed_And_Approved)
                {

                    if (product.ProductChildren_Fixed_Not_Hidden.IsNullOrEmpty())
                        continue;

                    foreach (var child in product.ProductChildren_Fixed_Not_Hidden)
                    {
                        pChildren.Add(child);
                    }
                }

                return pChildren;
            }
        }

        [NotMapped]

        public List<Product> Product_Shops_Not_Expired
        {
            get
            {
                if (Products_Fixed.IsNullOrEmpty())
                    return new List<Product>();

                List<Product> shops = new List<Product>();
                foreach (Product shop in Products_Fixed)
                {

                    if (shop.IsShopExpired)
                        continue;

                    shops.Add(shop);
                }

                return shops;
            }
        }




        #endregion
        #region booleans

        public bool IsMenuPath3Null()
        {
            bool cat3Null = MenuPath3Id.IsNullOrWhiteSpace() && MenuPath3.IsNull();
            return cat3Null;
        }

        public bool IsMenuPath2Null()
        {
            bool cat2Null = MenuPath2Id.IsNullOrWhiteSpace() && MenuPath2.IsNull();
            return cat2Null;
        }

        public bool IsMenuPath11Null()
        {
            bool cat1Null = MenuPath1Id.IsNullOrWhiteSpace() && MenuPath1.IsNull();
            return cat1Null;
        }


        /// <summary>
        /// Only Cat 1 is used. Others are empty
        /// </summary>
        /// <returns></returns>
        //public bool Is_Level_1_Category()
        //{
        //    return !IsMenuPath11Null() && IsMenuPath2Null() && IsMenuPath3Null();
        //}

        //public bool Is_Level_2_Category()
        //{
        //    return !IsMenuPath11Null() && !IsMenuPath2Null() && IsMenuPath3Null();

        //}

        //public bool Is_Level_3_Category()
        //{
        //    return !IsMenuPath11Null() && !IsMenuPath2Null() && !IsMenuPath3Null();

        //}


        #endregion
        #region SelfErrorCheck
        public override void SelfErrorCheck()
        {
            Errorcheck_All_3_Categories_Cannot_Be_null();
            Errorcheck_If_You_Have_Not_Filled_Cat1_You_Cannot_use_Cat2_Or_Cat3();

            if (sb.Length > 0)
            {
                sb.Append("Product Category Main. ");
                throw new Exception(sb.ToString());
            }
        }

        private void Errorcheck_If_You_Have_Not_Filled_Cat1_You_Cannot_use_Cat2_Or_Cat3()
        {
            if (IsMenuPath11Null())
            {
                if (!IsMenuPath2Null())
                {
                    string error = "You cannot use the 2nd category, if you have not used the first category. ";
                    sb.Append(error); ;
                }

                if (!IsMenuPath3Null())
                {
                    string error = "You cannot use the 3rd category, if you have not used the 2nd category. ";
                    sb.Append(error); ;

                }


            }
            else
            {
                // and if you have not filled the 2nd... you cannot use the 3rd.
                if (IsMenuPath2Null())
                {
                    if (!IsMenuPath3Null())
                    {
                        string error = "You cannot use the 3rd category, if you have not used the 2nd category. ProductCategoryMain.CheckChildCategoriesValid. ";
                        sb.Append(error); ;

                    }
                }


            }
        }

        private void Errorcheck_All_3_Categories_Cannot_Be_null()
        {
            if (IsMenuPath11Null() && IsMenuPath2Null() && IsMenuPath3Null())
            {
                string error = "All 3 categories cannot be null. ProductCategoryMain.CheckChildCategoriesValid. ";
                sb.Append(error);
            }
        }

        #endregion


        public string MakeName(string menupath1name, string menupath2name, string menupath3name)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(menupath1name);

            if (!menupath2name.IsNullOrWhiteSpace())
            {
                sb.Append(" - " + menupath2name);

            }

            if (!menupath3name.IsNullOrWhiteSpace())
            {
                sb.Append(" - " + menupath3name);

            }


            return sb.ToString();
        }


        public override string ToString()
        {

            return MakeName(MenuPath1.Name, MenuPath2.Name, MenuPath3.Name);
        }


        public static decimal Payment_To_Buy_Shop()
        {
            string minBalanceStr = ConfigurationManager.AppSettings["payment_to_open_Shop.amount"];
            decimal minBalance = minBalanceStr.ToDecimal();

            return minBalance;
        }


    }
}