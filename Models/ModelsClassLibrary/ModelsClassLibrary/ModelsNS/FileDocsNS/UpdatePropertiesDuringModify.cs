
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS
{
    //We will only use 
    public partial class FileDoc
    {
        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);

            FileDoc fileDoc = icommonWithId as FileDoc;

            FileNumber = fileDoc.FileNumber;
            PersonId = fileDoc.PersonId;


        }

    }
}
