using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.ParametersNS;
using AliKuli.Extentions;

namespace UowLibrary.DiscountPrecedenceNS
{
    public partial class DiscountPrecedenceBiz : BusinessLayer<DiscountPrecedence>
    {
        public DiscountPrecedenceBiz(IRepositry<DiscountPrecedence> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }



        public override void CreateSimple(ControllerCreateEditParameter parm)
        {
            DiscountPrecedence dp = parm.Entity as DiscountPrecedence;
            if (dp.DiscountRuleEnum == DiscountRuleENUM.Unknown || dp.DiscountTypeEnum == DiscountTypeENUM.Unknown)
                return;

            base.CreateSimple(parm);
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


        public List<DiscountPrecedence> FixRanks(List<DiscountPrecedence> listIn)
        {
            if (listIn.IsNullOrEmpty())
                return null;

            listIn.Sort();

            int curRank = 0;
            foreach (var d in listIn)
            {
                curRank = curRank + 5;
                d.Rank = curRank;
                //Update
            };

            return listIn;

        }

        public override void Event_DoSpecialInitializationStuff(DiscountPrecedence tentity)
        {
            base.Event_DoSpecialInitializationStuff(tentity);
            

        }



    }
}
