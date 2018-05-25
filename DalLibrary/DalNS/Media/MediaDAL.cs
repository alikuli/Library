using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.MediaNS;
using UserModels.Models;

namespace DalLibrary.DalNS
{
    public class MediaDAL : Repositry<UploadedFile>
    {

        public MediaDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }


    }
}
