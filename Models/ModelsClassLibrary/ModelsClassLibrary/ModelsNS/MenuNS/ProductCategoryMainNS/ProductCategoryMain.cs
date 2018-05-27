using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.ProductNS;
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
    public partial class ProductCategoryMain : MenuPathAbstract, IProductCategoryMain
    {
        StringBuilder sb = new StringBuilder();

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.MenuPathMain;
        }
        [Display(Name = "ProductCategory 1")]

        public string ProductCat1Id { get; set; }
        public virtual MenuPath1 ProductCat1 { get; set; }

        #region Navigation Properties


        [Display(Name = "ProductCategory 2")]

        public string ProductCat2Id { get; set; }
        public virtual MenuPath2 ProductCat2 { get; set; }


        [Display(Name = "ProductCategory 3")]
        public string ProductCat3Id { get; set; }
        public virtual ProductCategory3 ProductCat3 { get; set; }



        //public virtual ICollection<IDiscount> ProductCategoryDiscounts { get; set; }
        public ICollection<Product> Products { get; set; }


        #endregion
        #region booleans
        public bool IsCat3Null()
        {
            bool cat3Null = ProductCat3Id.IsNullOrWhiteSpace() && ProductCat3.IsNull();
            return cat3Null;
        }

        public bool IsCat2Null()
        {
            bool cat2Null = ProductCat2Id.IsNullOrWhiteSpace() && ProductCat2.IsNull();
            return cat2Null;
        }

        public bool IsCat1Null()
        {
            bool cat1Null = ProductCat1Id.IsNullOrWhiteSpace() && ProductCat1.IsNull();
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


        public void MakeName(string cat1Name, string cat2Name, string cat3Name)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(cat1Name);

            if (!cat2Name.IsNullOrWhiteSpace())
            {
                sb.Append(" - " + cat2Name);

            }

            if (!cat3Name.IsNullOrWhiteSpace())
            {
                sb.Append(" - " + cat3Name);

            }


            Name = sb.ToString();
        }



 
    }
}