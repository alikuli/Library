﻿using DatastoreNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace UowLibrary.MenuNS


{
    public partial class MenuPath1Biz 
    {


        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }

        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return MenuPath1Array.DataArray();
            }
        }






    }
}