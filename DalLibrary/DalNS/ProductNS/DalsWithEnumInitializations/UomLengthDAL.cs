
using AliKuli.UtilitiesNS;
using AliKuli.Extentions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using ModelsClassLibrary.ModelsNS.ProductNS;

using UserModels.Models;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// </summary>
    public class UomLengthDAL:Repositry<UomLength>, IEntityWithEnum
    {

        //private ApplicationDbContext _db;
        //private string _user;


        public UomLengthDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }




        public override string MakeNameForIndexMethod(UomLength entity)
        {
            return string.Format("{0} (Meter X {1})", entity.Name, entity.Multiplier.ToString("0.000000"));
        }
        public override void Delete(UomLength entity)
        {
            if (IsExistNameInProduct(entity))
                throw new ErrorHandlerLibrary.ExceptionsNS.ChildDataExistsException(string.Format("You cannot delete '{0}' because it is being used as an identifier for a product. Remove it as an identifier, then delete it."));
            base.Delete(entity);
        }

        private bool IsExistNameInProduct(UomLength entity)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Convert everything to Meters
        /// </summary>
        public void InitializeFromEnumAndSave()
        {

            UomLengthENUM c = UomLengthENUM.Unknown;

            //this will create customer categories for the Enums
            var listOfEnums = Enum.GetNames(c.GetType()).ToList();
            listOfEnums.Remove(UomLengthENUM.Unknown.ToString());


            foreach (var item in listOfEnums)
            {
                UomLength uomLen = Factory();
                uomLen.Name = item.ToSentence().ToTitleCase();
                uomLen.MetaData.IsAutoCreated = true;

                switch (item.ToLower())
                {
                    case "cm":
                        uomLen.Multiplier = 0.01;
                        break;

                    case "ft":
                        uomLen.Multiplier = 0.3048;
                        break;

                    case "inch":
                        uomLen.Multiplier = 0.0254;
                        break;

                    case "m":
                        uomLen.Multiplier = 1;
                        break;

                    case "mm":
                        uomLen.Multiplier = 0.001;
                        break;
                    case "yd":
                        uomLen.Multiplier = 0.9144;
                        break;
                }
                uomLen.MetaData.Comment = "Initialized automatically on " + DateTime.UtcNow;
                try
                {
                    Create(uomLen);

                }
                catch(ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
                { }
            }
            Save();



        }
        public UomLength FindForEnum(UomLengthENUM theEnum)
        {
            if (theEnum == UomLengthENUM.Unknown)
                return null;

            string name = Enum.GetName(theEnum.GetType(), theEnum);

            return FindForName(name.ToSentence());
        }


    }
}
