using System;
using System.Collections.Generic;
using InterfacesLibrary.SharedNS;
using UserModels;
using ModelsClassLibrary.ModelsNS.ProductNS;
using InterfacesLibrary.ProductNS;
using InterfacesLibrary.MenuNS;

namespace ModelsClassLibrary.MenuNS


{
    
    public partial class MenuPathMain 
    {
        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);

            MenuPathMain p = (MenuPathMain)icommonWithId;

            MenuPath1Id = p.MenuPath1Id;
            MenuPath2Id = p.MenuPath2Id;
            MenuPath3Id = p.MenuPath3Id;


        }



    }
}