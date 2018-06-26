using DatastoreNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.IO;
using System.Web;
using AliKuli.Extentions;

namespace UowLibrary.MenuNS


{
    public partial class MenuPath1Biz 
    {


        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }

        //public override string[] GetDataForStringArrayFormat
        //{
        //    get
        //    {
        //        return MenuPath1Array.DataArray();
        //    }
        //}


        public override void AddInitData()
        {
            MenuPath1ENUM[] dataList = MenuPath1Array.DataArray2();

            dataList.IsNullOrEmptyThrowException("No data");

            for (int i = 0; i < dataList.Length; i++)
            {
                MenuPath1 mp1 = Factory() as MenuPath1;
                mp1.Name = mp1.CreateNameFromEnum(dataList[i]);
                mp1.MenuPath1Enum = dataList[i];

                bool recordExists = !FindByName(mp1.Name).IsNull();
                bool recordIsUnkown = mp1.Name.ToLower() == "unknown";

                if (recordIsUnkown)
                {
                    Dal.Detach(mp1);
                    continue;
                }
                if (recordExists)
                {
                    Dal.Detach(mp1);
                    continue;
                }
                CreateSave_ForInitializeOnly(mp1);

            }
                
        }



    }
}
