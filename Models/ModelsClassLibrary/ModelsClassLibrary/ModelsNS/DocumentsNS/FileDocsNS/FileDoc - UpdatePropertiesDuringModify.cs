using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS
{
    //We will only use 
    public partial class FileDoc : CommonWithId
    {
        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            FileDoc f = (FileDoc)icommonWithId;
            FileNumber = f.FileNumber;

        }

    }
}
