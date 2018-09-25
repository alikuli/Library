using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using System;
using System.Reflection;
using UowLibrary.FileDocNS;
using AliKuli.Extentions;
using UowLibrary;
using ModelsClassLibrary.ModelsNS.SharedNS;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using ErrorHandlerLibrary;
using UowLibrary.PageViewNS;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class FileDocsController : EntityAbstractController<FileDoc>
    {
        FileDocBiz _fileDocBiz;

        public FileDocsController(FileDocBiz biz,  AbstractControllerParameters param)
            : base(biz, param) 
        {
            _fileDocBiz = biz;
            
        }



        public FileDocBiz FileDocBiz
        {
            get
            {
                return _fileDocBiz;
            }
        }


    }
}