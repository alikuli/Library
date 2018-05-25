
using AliKuli.UtilitiesNS;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using ErrorHandlerLibrary.ExceptionsNS;

using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;

using UserModels.Models;



namespace DalLibrary.DalNS
{
    /// <summary>
    /// </summary>
    public class UomQtyDAL : Repositry<UomQty>, IEntityWithEnum
    {

        //private ApplicationDbContext _db;
        //private string _user;


        public UomQtyDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }


        
        public override string   MakeNameForIndexMethod(UomQty  entity)
        {
            return string.Format("{0} ({1} Ea)", entity.Name, entity.Multiplier.ToString("0.00"));
        }



        public void InitializeFromEnumAndSave()
        {

            UomQtyENUM c = UomQtyENUM.Unknown;

            //this will create customer categories for the Enums


            var listOfEnums = Enum.GetNames(c.GetType()).ToList();
            listOfEnums.Remove(UomQtyENUM.Unknown.ToString());


            foreach (var item in listOfEnums)
            {
                UomQty uomQty = Factory();
                uomQty.Name = item.ToSentence().ToTitleCase();
                uomQty.MetaData.IsAutoCreated = true;

                switch (item.ToLower())
                {
                    case "count":
                        uomQty.Multiplier = 1;
                        break;

                    case "dz":
                        uomQty.Multiplier = 12;
                        break;

                    case "ea":
                        uomQty.Multiplier = 1;
                        break;

                    case "pair":
                        uomQty.Multiplier = 2;
                        break;

                    case "single":
                        uomQty.Multiplier = 1;
                        break;
                }
                uomQty.MetaData.Comment = "Initialized automatically on " + DateTime.UtcNow;

                try
                {
                    Create(uomQty);
                }
                catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
                {
                }
            }
            Save();



        }


        public UomQty FindForEnum(UomQtyENUM theEnum)
        {
            if (theEnum == UomQtyENUM.Unknown)
                return null;

            string name = Enum.GetName(theEnum.GetType(), theEnum);

            return FindForName(name.ToSentence());
        }

    }
}
