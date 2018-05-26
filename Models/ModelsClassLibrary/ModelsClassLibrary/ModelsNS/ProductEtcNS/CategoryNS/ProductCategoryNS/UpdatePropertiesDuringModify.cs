using System;
using System.Collections.Generic;
using InterfacesLibrary.SharedNS;
using UserModels;
using ModelsClassLibrary.ModelsNS.ProductNS;
using InterfacesLibrary.ProductNS;

namespace ModelsClassLibrary.ModelsNS.ProductNS

{
    
    public partial class ProductCategoryMain : ProductCategoryAbstract, IProductCategoryMain
    {
        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);

            ProductCategoryMain p = (ProductCategoryMain)icommonWithId;

            ProductCat1Id = p.ProductCat1Id;
            ProductCat2Id = p.ProductCat2Id;
            ProductCat3Id = p.ProductCat3Id;


        }



    }
}