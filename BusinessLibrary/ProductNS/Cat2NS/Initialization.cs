﻿using DatastoreNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace UowLibrary.ProductNS
{
    public partial class ProductCat2Biz 
    {


        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }

        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return MenuPath2Array.DataArray();
            }
        }

        

    }
}
