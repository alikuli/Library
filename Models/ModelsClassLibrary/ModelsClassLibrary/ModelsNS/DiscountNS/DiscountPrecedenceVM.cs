using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.DiscountNS
{
    
    [NotMapped]
    public class DiscountPrecedenceVM : DiscountPrecedence, IDiscountPrecedence
    {

        public DiscountPrecedenceVM()
        {

        }

        public DiscountPrecedenceVM(DiscountPrecedence dp)
        {
            this.DiscountRuleEnum = dp.DiscountRuleEnum;
            this.DiscountTypeEnum = dp.DiscountTypeEnum;
            this.Id = dp.Id;
            this.MetaData = dp.MetaData;
            this.Rank = dp.Rank;
            this.UserId = dp.UserId;
        }

        public DiscountPrecedence LoadEditInfo(DiscountPrecedence dp)
        {
            dp.DiscountRuleEnum = this.DiscountRuleEnum;
            dp.DiscountTypeEnum = this.DiscountTypeEnum;
            dp.MetaData.Created.DateStart = this.MetaData.Created.DateStart;
            dp.Rank = this.Rank;
            dp.UserId = this.UserId;

            return dp;
        }
        public bool selected { get; set; }
        public SelectList UsersSelectList { get; set; }
    }
}