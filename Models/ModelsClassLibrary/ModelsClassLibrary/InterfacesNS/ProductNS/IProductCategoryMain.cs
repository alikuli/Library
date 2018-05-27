using System;
using System.Collections.Generic;
//using InterfacesLibrary.DiscountNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.MenuNS;

namespace InterfacesLibrary.MenuNS
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
        MenuPath1 MenuPath1 { get; set; }
        string MenuPath1Id { get; set; }
        MenuPath2 MenuPath2 { get; set; }
        string MenuPath2Id { get; set; }
        MenuPath3 MenuPath3 { get; set; }
        string MenuPath3Id { get; set; }
        //ICollection<IDiscount> ProductCategoryDiscounts { get; set; }
        void SelfErrorCheck();
    }
}
