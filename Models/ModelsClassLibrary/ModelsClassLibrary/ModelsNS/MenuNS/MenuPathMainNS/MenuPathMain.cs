﻿using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.MenuNS;
using InterfacesLibrary.ProductNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace ModelsClassLibrary.MenuNS

{
    /// <summary>
    /// This is the Menu Path
    /// </summary>
    public partial class MenuPathMain : MenuPathAbstract, IProductCategoryMain
    {
        StringBuilder sb = new StringBuilder();

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.MenuPathMain;
        }

        #region Navigation Properties

        [Display(Name = "Menu Path 1")]
        public string MenuPath1Id { get; set; }
        public virtual MenuPath1 MenuPath1 { get; set; }


        [Display(Name = "Menu Path 2")]

        public string MenuPath2Id { get; set; }
        public virtual MenuPath2 MenuPath2 { get; set; }


        [Display(Name = "Menu Path 3")]
        public string MenuPath3Id { get; set; }
        public virtual MenuPath3 MenuPath3 { get; set; }



        //public virtual ICollection<IDiscount> ProductCategoryDiscounts { get; set; }
        public ICollection<Product> Products { get; set; }


        #endregion
        #region booleans
        public bool IsCat3Null()
        {
            bool cat3Null = MenuPath3Id.IsNullOrWhiteSpace() && MenuPath3.IsNull();
            return cat3Null;
        }

        public bool IsCat2Null()
        {
            bool cat2Null = MenuPath2Id.IsNullOrWhiteSpace() && MenuPath2.IsNull();
            return cat2Null;
        }

        public bool IsCat1Null()
        {
            bool cat1Null = MenuPath1Id.IsNullOrWhiteSpace() && MenuPath1.IsNull();
            return cat1Null;
        }

        /// <summary>
        /// Only Cat 1 is used. Others are empty
        /// </summary>
        /// <returns></returns>
        public bool Is_Level_1_Category()
        {
            return !IsCat1Null() && IsCat2Null() && IsCat3Null();
        }

        public bool Is_Level_2_Category()
        {
            return !IsCat1Null() && !IsCat2Null() && IsCat3Null();

        }

        public bool Is_Level_3_Category()
        {
            return !IsCat1Null() && !IsCat2Null() && !IsCat3Null();

        }


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
            if (IsCat1Null())
            {
                if (!IsCat2Null())
                {
                    string error = "You cannot use the 2nd category, if you have not used the first category. ";
                    sb.Append(error); ;
                }

                if (!IsCat3Null())
                {
                    string error = "You cannot use the 3rd category, if you have not used the 2nd category. ";
                    sb.Append(error); ;

                }


            }
            else
            {
                // and if you have not filled the 2nd... you cannot use the 3rd.
                if (IsCat2Null())
                {
                    if (!IsCat3Null())
                    {
                        string error = "You cannot use the 3rd category, if you have not used the 2nd category. ProductCategoryMain.CheckChildCategoriesValid. ";
                        sb.Append(error); ;

                    }
                }


            }
        }

        private void Errorcheck_All_3_Categories_Cannot_Be_null()
        {
            if (IsCat1Null() && IsCat2Null() && IsCat3Null())
            {
                string error = "All 3 categories cannot be null. ProductCategoryMain.CheckChildCategoriesValid. ";
                sb.Append(error);
            }
        }

        #endregion


        public void MakeName(string menupath1name, string menupath2name, string menupath3name)
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


            Name = sb.ToString();
        }



 
    }
}