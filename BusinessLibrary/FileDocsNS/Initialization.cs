using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {
        public new List<OldFileData> GetDataForStringArrayFormat
        {
            get
            {
                //IRepositry<OldFileData> OldFileDataDAL = new Repositry<OldFileData>(_db, ErrorsGlobal);
                throw new NotImplementedException();
                //var lstOldData = _db.OldFileDatas.ToList();
                //return lstOldData;
            }
        }


        public override void AddInitData()
        {
            if (GetDataForStringArrayFormat.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("No initialization data.", MethodBase.GetCurrentMethod());
            }


            foreach (var item in GetDataForStringArrayFormat)
            {
                FileDoc x = Factory() as FileDoc;

                x.Name = string.Format("{0}", item.FullName);

                if (item.ParentFileNumber.IsNullOrWhiteSpace())
                {
                    x.FileNumber = Convert.ToInt64(item.ChildFilenumber);

                }
                else
                {
                    x.FileNumber = Convert.ToInt64(item.ParentFileNumber);


                }

                x.OldFileNumber = item.CompleteFileNumber;

                x.UserId = UserId;

                try
                {
                    CreateAndSave(CreateControllerCreateEditParameter(x as ICommonWithId));
                }
                catch (NoDuplicateException)
                {

                    continue;
                }
                catch (Exception e)
                {
                    ErrorsGlobal.Add("Error while creating entity", MethodBase.GetCurrentMethod(), e);

                }
            }

            //for (int i = 0; i < GetDataForStringArrayFormat.Length; i++)
            //{
            //    TEntity x = Factory();
            //    x.Name = dataList[i];
            //    Event_ChangeStringFormatInitializationDataBeforeAdding(x);
            //    x.MetaData.IsEditLocked = Event_LockEditDuringInitialization();
            //    try
            //    {
            //        CreateAndSave(x);
            //    }
            //    catch (NoDuplicateException)
            //    {

            //        continue;
            //    }
            //    catch (Exception e)
            //    {
            //        ErrorsGlobal.Add("Error while creating entity", MethodBase.GetCurrentMethod(), e);

            //    }


            //}
        }
    }
}
