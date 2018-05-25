
using AliKuli.Extentions;

using EnumLibrary.EnumNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.GeneralLedgerNS;

using UserModels.Models;

namespace DalLibrary.DalNS
{
    /// <summary>
    /// </summary>
    public class GlTrxDAL : Repositry<GlTrx>
    {

        //private ApplicationDbContext _db;
        //private string _user;
        //private ProductCategoryMainDAL prodCatMainDAL;

        public GlTrxDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());

        }

        public void InitializeFromEnumAndSave()
        {
            //this will create customer categories for the Enums
            //var listOfEnums = Enum.GetNames(typeof(DebitCreditENUM)).ToList();

            var listOfEnums = EnumExtention<DebitCreditENUM>.ToList();
            listOfEnums.Remove(DebitCreditENUM.Unknown.ToString());
            CreateEntitiesFromListOfStrings(listOfEnums);
            Save();

        }
    }
}
