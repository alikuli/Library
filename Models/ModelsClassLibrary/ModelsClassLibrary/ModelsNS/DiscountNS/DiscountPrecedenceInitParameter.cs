using System;
using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DiscountNS
{
    
    public class DiscountPrecedenceInitParameter
    {
        public DiscountPrecedenceInitParameter(DiscountRuleENUM discountRuleEnum, DiscountTypeENUM discountTypeEnum)
        {
            DiscountTypeEnum = discountTypeEnum;
            DiscountRuleEnum = discountRuleEnum;
        }
        public DiscountRuleENUM DiscountRuleEnum { get; set; }

        /// <summary>
        /// This is where the rule will apply, Sale or Purchase at the moment.
        /// </summary>
        public DiscountTypeENUM DiscountTypeEnum { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", DiscountTypeEnum.ToString(), DiscountRuleEnum.ToString());
        }

    }
}
