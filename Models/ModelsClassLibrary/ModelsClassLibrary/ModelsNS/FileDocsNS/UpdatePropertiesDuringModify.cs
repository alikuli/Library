﻿
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS
{
    //We will only use 
    public partial class FileDoc
    {
        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);

            FileDoc f = (FileDoc)icommonWithId;

            FileNumber = f.FileNumber;
            UserId = f.UserId;


        }

    }
}
