using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;


namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {


        public override void Event_ApplyChangesAfterCreate(FileDoc entity)
        {
            base.Event_ApplyChangesAfterCreate(entity);
            entity.FileNumber = GetNextFileNumber(entity);
        }

    }
}
