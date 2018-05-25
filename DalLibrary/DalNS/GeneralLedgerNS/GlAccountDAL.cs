

using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.GeneralLedgerNS;

using UserModels.Models;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// </summary>
    public class GlAccountDAL : Repositry<GlAccount>
    {

        public GlAccountDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());

        }


    }
}
