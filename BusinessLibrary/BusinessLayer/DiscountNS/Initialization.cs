using AliKuli.Extentions;
using DatastoreNS;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DiscountNS;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace UowLibrary.DiscountPrecedenceNS
{
    public partial class DiscountPrecedenceBiz : BusinessLayer<DiscountPrecedence>
    {


        public override void AddInitData()
        {
            List<DiscountPrecedenceInitParameter> DiscountPrecedenceNamesLst = DiscountPrecedenceData.DataList();

            if (DiscountPrecedenceNamesLst.IsNullOrEmpty())
            {
                ErrorsGlobal.Add("No Discount Precedences received!!", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            //int rank = 0;
            foreach (DiscountPrecedenceInitParameter discPrecedenceParameter in DiscountPrecedenceNamesLst)
            {
                try
                {
                    if (discPrecedenceParameter.DiscountRuleEnum != DiscountRuleENUM.Unknown || discPrecedenceParameter.DiscountTypeEnum != DiscountTypeENUM.Unknown)
                    {
                        DiscountPrecedence dp = Factory() as DiscountPrecedence;

                        //rank += DiscountPrecedence.RANK_SPACING_CONST;
                        //dp.Rank = rank;

                        //Item will be put at the end of the list with calculated rank
                        dp.DiscountTypeEnum = discPrecedenceParameter.DiscountTypeEnum;
                        dp.DiscountRuleEnum = discPrecedenceParameter.DiscountRuleEnum;


                        //Create(dp);
                        //SaveChanges();
                        CreateSave_ForInitializeOnly(dp);

                    }
                    else
                    {
                        continue;
                    }
                }
                catch (NoDuplicateException)
                {
                    ErrorsGlobal.AddMessage(string.Format("{0} already exists", discPrecedenceParameter));
                    continue;
                    //fail silently...
                }
                catch (Exception e)
                {

                    ErrorsGlobal.Add("There was an error while creating Discount Precedences", MethodBase.GetCurrentMethod(), e);
                    throw new Exception(ErrorsGlobal.ToString());
                }
            }

            ErrorsGlobal.AddMessage("All Discount Precedences added to database");
        }



    }
}
