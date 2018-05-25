using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {

        public override void BusinessRulesFor(FileDoc entity)
        {
            base.BusinessRulesFor(entity);

            if (entity.FileNumber.IsNull())
                entity.FileNumber = GetNextFileNumber(entity);

            
        }


    }
}
