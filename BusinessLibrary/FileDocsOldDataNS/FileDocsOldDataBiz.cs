using AliKuli.Extentions;
using ApplicationDbContextNS;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using DalNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using System;
using System.Reflection;
using WebLibrary.Programs;

namespace UowLibrary.CountryNS
{
    public partial class FileDocOldDataBiz : BusinessLayer<OldFileData>
    {
        public FileDocOldDataBiz(UserDAL userDAL, IRepositry<OldFileData> ifileDocDAL, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db)
            : base(userDAL, memoryMain, errorSet, ifileDocDAL, db)
        {

        }


    }
}
