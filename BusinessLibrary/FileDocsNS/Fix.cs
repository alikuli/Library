using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using System;
using System.Linq;
using System.Reflection;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {

        public override void Fix(FileDoc entity)
        {
            base.Fix(entity);
            entity.Name = entity.Name.ToTitleCase();

            
            AttachUser(entity);
        }

        private void AttachUser(FileDoc entity)
        {

            entity.UserId = GetUserIdOrThrowErrorIfNull();
            entity.User = _db.Users.FirstOrDefault(x => x.Id == entity.UserId);

            if(entity.User.IsNull())
            {
                ErrorsGlobal.Add("User not found!", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }
        }


    }
}
