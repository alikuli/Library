using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.DiscountPrecedenceNS
{
    public partial class DiscountPrecedenceBiz : BusinessLayer<DiscountPrecedence>
    {
        public DiscountPrecedenceBiz(IRepositry<DiscountPrecedence> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)

        {

        }



        public override void Create(ControllerCreateEditParameter parm)
        {
            DiscountPrecedence dp = parm.Entity as DiscountPrecedence;
            if (dp.DiscountRuleEnum == DiscountRuleENUM.Unknown || dp.DiscountTypeEnum == DiscountTypeENUM.Unknown)
                return;

            base.Create(parm);
        }

        public override void Fix(ControllerCreateEditParameter parm)
        {
            //This uses the following fields to make the name
            // Rule
            // Type
            // UserName (If User is not null, o/w it leaves user out)
            DiscountPrecedence dp = parm.Entity as DiscountPrecedence;

            dp.Name = dp.FixNameFor(dp);
            //putCurrentPrecedenceInLastRank(entity);
            base.Fix(parm);
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
