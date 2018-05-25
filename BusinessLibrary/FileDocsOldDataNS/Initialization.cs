//using AliKuli.Extentions;
//using DalLibrary.DalNS;
//using DalLibrary.Interfaces;
//using ErrorHandlerLibrary.ExceptionsNS;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//namespace UowLibrary.CountryNS
//{
//    public partial class FileDocOldDataBiz : BusinessLayer<OldFileData>
//    {
//        public new List<OldFileData> GetDataForStringArrayFormat
//        {
//            get
//            {
//                IRepositry<OldFileData> OldFileDataDAL = new Repositry<OldFileData>(_db, ErrorsGlobal);

//                var lstOldData = OldFileDataDAL.FindAll().ToList();
//                return lstOldData;
//            }
//        }


//        public override void AddInitData()
//        {
//            if (GetDataForStringArrayFormat.IsNullOrEmpty())
//            {
//                ErrorsGlobal.Add("No initialization data.", MethodBase.GetCurrentMethod());
//            }


//            foreach (var item in GetDataForStringArrayFormat)
//            {
//                OldFileData x = Factory();

//                x.Name = item.FullName;
//                x.FileNumber = long.Parse(item.ParentFileNumber);
//                x.UserId = UserId;

//                try
//                {
//                    CreateAndSave(x);
//                }
//                catch (NoDuplicateException)
//                {

//                    continue;
//                }
//                catch (Exception e)
//                {
//                    ErrorsGlobal.Add("Error while creating entity", MethodBase.GetCurrentMethod(), e);

//                }
//            }

//            //for (int i = 0; i < GetDataForStringArrayFormat.Length; i++)
//            //{
//            //    TEntity x = Factory();
//            //    x.Name = dataList[i];
//            //    Event_ChangeStringFormatInitializationDataBeforeAdding(x);
//            //    x.MetaData.IsEditLocked = Event_LockEditDuringInitialization();
//            //    try
//            //    {
//            //        CreateAndSave(x);
//            //    }
//            //    catch (NoDuplicateException)
//            //    {

//            //        continue;
//            //    }
//            //    catch (Exception e)
//            //    {
//            //        ErrorsGlobal.Add("Error while creating entity", MethodBase.GetCurrentMethod(), e);

//            //    }


//            //}
//        }
//    }
//}
