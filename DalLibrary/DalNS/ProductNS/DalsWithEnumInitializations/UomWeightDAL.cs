
using AliKuli.UtilitiesNS;
using AliKuli.Extentions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using ModelsClassLibrary.ModelsNS.ProductNS;
using EnumLibrary.EnumNS;

using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;

using UserModels.Models;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// </summary>
    public class UomWeightDAL : Repositry<UomWeight>, IEntityWithEnum
    {

        //private ApplicationDbContext _db;
        //private string _user;


        public UomWeightDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }




        public override string MakeNameForIndexMethod(UomWeight entity)
        {
            return string.Format("{0} (KG X {1})", entity.Name, entity.Multiplier.ToString("0.000000"));
        }
        public override void Delete(UomWeight entity)
        {
            if (IsExistName(entity.Name))
                throw new ErrorHandlerLibrary.ExceptionsNS.ChildDataExistsException(string.Format("You cannot delete '{0}' because it is being used as an identifier for a product. Remove it as an identifier, then delete it."));
            base.Delete(entity);
        }

        public void InitializeFromEnumAndSave()
        {

            UomWeightENUM c = UomWeightENUM.Unknown;

            //this will create customer categories for the Enums
            //var listOfEnums = c.ToTitleSentanceList();
            var listOfEnums = Enum.GetNames(c.GetType()).ToList();

            listOfEnums.Remove(UomWeightENUM.Unknown.ToString());


            foreach (var item in listOfEnums)
            {

                UomWeight uomWeight = Factory();
                uomWeight.Name = item.ToSentence().ToTitleCase();
                uomWeight.MetaData.IsAutoCreated = true;

                switch (item.ToLower())
                {
                    case "gm":
                        uomWeight.Multiplier = 0.001;
                        break;

                    case "kg":
                        uomWeight.Multiplier = 1;
                        break;

                    case "lb":
                        uomWeight.Multiplier = 0.453592;
                        break;

                    case "oz":
                        uomWeight.Multiplier = 0.0283495;
                        break;
                }
                uomWeight.MetaData.Comment = "Initialized automatically for KG on " + DateTime.UtcNow;

                try
                {
                    Create(uomWeight);
                }
                catch(ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
                {

                }
            }
            Save();



        }

        public UomWeight FindForEnum(UomWeightENUM theEnum)
        {
            if (theEnum == UomWeightENUM.Unknown)
                return null;

            string name = Enum.GetName(theEnum.GetType(), theEnum);

            return FindForName(name.ToSentence());
        }
    }
}
