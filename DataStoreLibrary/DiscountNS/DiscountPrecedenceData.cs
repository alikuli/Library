using System;
using System.Collections.Generic;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DiscountNS;


namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database 
    /// </summary>
    public class DiscountPrecedenceData
    {
        public static List<DiscountPrecedenceInitParameter> DataList()
        {
            List<DiscountPrecedenceInitParameter> listOfRules = new List<DiscountPrecedenceInitParameter>();
            foreach (string dpType in Enum.GetNames(typeof(DiscountTypeENUM)))
	        {
                DiscountTypeENUM dpTypeEnum = (DiscountTypeENUM) Enum.Parse(typeof(DiscountTypeENUM), dpType);
                if (dpTypeEnum == DiscountTypeENUM.Unknown)
                    continue;

                foreach (var dpRule in Enum.GetNames(typeof(DiscountRuleENUM)))
                {
                    DiscountRuleENUM discountRuleEnum = (DiscountRuleENUM)Enum.Parse(typeof(DiscountRuleENUM), dpRule);

                    if (discountRuleEnum == DiscountRuleENUM.Unknown)
                        continue;

                    DiscountPrecedenceInitParameter dp = new DiscountPrecedenceInitParameter(discountRuleEnum,dpTypeEnum);

                    listOfRules.Add(dp);
                }
	        }
            return listOfRules;
        }
    }
}
