using System.Collections.Generic;
using System.Linq;
using EnumLibrary.EnumNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DiscountNS;

using UserModels.Models;

namespace DalLibrary.DalNS
{
    public class DiscountPrecedenceDAL : Repositry<DiscountPrecedence>
    {

        //private ApplicationDbContext db;
        //private string user;

        public DiscountPrecedenceDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass("DiscountPrecedenceDAL");

        }


        public override void ErrorCheck(DiscountPrecedence entity)
        {
            var found = this.SearchFor(x => x.DiscountRuleEnum == entity.DiscountRuleEnum &&
                x.Id != entity.Id).FirstOrDefault();

            if (found != null)
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException();
        }


        public override void Create(DiscountPrecedence entity)
        {
            if (entity.DiscountRuleEnum != DiscountRuleENUM.Unknown)
                base.Create(entity);
        }


        public ICollection<DiscountPrecedence> FindAllRulesByRankSale()
        {
            return FindAll().Where(x => x.DiscountTypeEnum == DiscountTypeENUM.Sale).OrderBy(x => x.Rank).ToList();
        }


        public ICollection<DiscountPrecedence> FindAllRulesByRankPurchase()
        {
            return FindAll().Where(x => x.DiscountTypeEnum == DiscountTypeENUM.Purchase).OrderBy(x => x.Rank).ToList();
        }
        public void FixRanking()
        {
            var listOfRecs = this.FindAll().OrderBy(x => x.Rank).ToList();

            if (listOfRecs != null)
            {
                if (listOfRecs.Count() > 0)
                {
                    int counter = 0;
                    foreach (var item in listOfRecs)
                    {
                        item.Rank = counter;
                        counter += 5;
                    }
                }
            }
        }

    }
}
