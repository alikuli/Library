using System;
using System.Collections.Generic;
//using InterfacesLibrary.DiscountNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.MenuNS;

namespace InterfacesLibrary.ProductNS
{
    public interface IProductCategoryMain
    {
        string FullName();
        bool Is_Level_1_Category();
        bool Is_Level_2_Category();
        bool Is_Level_3_Category();
        bool IsCat1Null();
        bool IsCat2Null();
        bool IsCat3Null();
        MenuPath1 ProductCat1 { get; set; }
        string ProductCat1Id { get; set; }
        MenuPath2 ProductCat2 { get; set; }
        string ProductCat2Id { get; set; }
        ProductCategory3 ProductCat3 { get; set; }
        string ProductCat3Id { get; set; }
        //ICollection<IDiscount> ProductCategoryDiscounts { get; set; }
        void SelfErrorCheck();
    }
}
