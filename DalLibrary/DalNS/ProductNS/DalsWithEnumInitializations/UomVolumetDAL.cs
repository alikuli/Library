
using System;
using System.Linq;
using ErrorHandlerLibrary.ExceptionsNS;
using AliKuli.Extentions;

using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using Microsoft.AspNet.Identity;

using UserModels.Models;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// </summary>
    public class UomVolumeDAL : Repositry<UomVolume>, IEntityWithEnum
    {

        //private ApplicationDbContext _db;
        //private string _user;


        public UomVolumeDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }




        public override string MakeNameForIndexMethod(UomVolume entity)
        {
            return string.Format("{0} (Litre X {1})", entity.Name, entity.Multiplier.ToString("0.000000"));
        }
        public override void Delete(UomVolume entity)
        {
            if (IsExistName(entity.Name))
                throw new ErrorHandlerLibrary.ExceptionsNS.ChildDataExistsException(string.Format("You cannot delete '{0}' because it is being used as an identifier for a product. Remove it as an identifier, then delete it."));
            base.Delete(entity);
        }

        /// <summary>
        /// converts everything to litre
        /// </summary>
        public void InitializeFromEnumAndSave()
        {

            UomVolumeENUM c = UomVolumeENUM.Unknown;

            //this will create customer categories for the Enums
            var listOfEnums = Enum.GetNames(c.GetType()).ToList();
            listOfEnums.Remove(UomVolumeENUM.Unknown.ToString());
            foreach (var item in listOfEnums)
            {
                UomVolume uomWeight = Factory();
                uomWeight.Name = item.ToSentence().ToTitleCase();
                uomWeight.MetaData.IsAutoCreated = true;

                switch (item.ToLower())
                {
                    case "l":
                        uomWeight.Multiplier = 1;
                        break;

                    case "ml":
                        uomWeight.Multiplier = 1000;
                        break;

                    case "floz":
                        uomWeight.Multiplier = 33.814;
                        break;

                    case "gallonsus":
                        uomWeight.Multiplier = 0.264172;
                        break;

                    case "gallonsuk":
                        uomWeight.Multiplier = 0.0283495;
                        break;

                    case "pint":
                        uomWeight.Multiplier = 0.219969;
                        break;

                    case "barrel":
                        uomWeight.Multiplier = 0.0062898105697751;
                        break;

                    case "quart":
                        uomWeight.Multiplier = 1.05669;
                        break;
                }
                uomWeight.MetaData.Comment = "Initialized automatically for LITRE on " + DateTime.UtcNow;

                try
                {
                    Create(uomWeight);
                }
                catch (NoDuplicateException)
                {
                }

            }
            Save();

        }

        public UomVolume FindForEnum(UomVolumeENUM theEnum)
        {
            if (theEnum == UomVolumeENUM.Unknown)
                return null;

            string name = Enum.GetName(theEnum.GetType(), theEnum);

            return FindForName(name.ToSentence());
        }


    }
}
