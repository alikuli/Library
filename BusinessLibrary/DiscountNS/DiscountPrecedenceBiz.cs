using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using DalNS;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.DiscountPrecedenceNS
{
    public partial class DiscountPrecedenceBiz : BusinessLayer<DiscountPrecedence>
    {
        public DiscountPrecedenceBiz(IRepositry<ApplicationUser> userDal, IRepositry<DiscountPrecedence> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {

        }



        public override void Create(DiscountPrecedence entity)
        {
            if (entity.DiscountRuleEnum == DiscountRuleENUM.Unknown || entity.DiscountTypeEnum == DiscountTypeENUM.Unknown)
                return;

            base.Create(entity);
        }

        public override void Fix(DiscountPrecedence entity)
        {
            //This uses the following fields to make the name
            // Rule
            // Type
            // UserName (If User is not null, o/w it leaves user out)

            entity.Name = entity.FixNameFor(entity);
            //putCurrentPrecedenceInLastRank(entity);
            base.Fix(entity);
        }

        public List<DiscountPrecedence> FindAllDiscountPrecWhereUserIsNull()
        {
            var lst = FindAll()
                .Where(x => string.IsNullOrEmpty(x.UserId))
                .OrderBy(x => x.Rank)
                .ToList();
            return lst;
        }

        public List<DiscountPrecedence> FindAllDiscountPrecWhereUserIsNotNull(string userId)
        {
            var lst = FindAll()
                .Where(x => x.UserId.Equals(userId))
                .OrderBy(x => x.Rank)
                .ToList();

            return lst;
        }






    }
}
