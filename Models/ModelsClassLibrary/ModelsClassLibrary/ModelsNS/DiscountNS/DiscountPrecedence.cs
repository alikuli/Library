using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.DiscountNS
{
    
    public class DiscountPrecedence : CommonWithId, IComparable, IDiscountPrecedence
    {

        public override ClassesWithRightsENUM ClassNameForRights()
        {
                return ClassesWithRightsENUM.DiscountPrecedence;
        }
        public static int RANK_SPACING_CONST = 1;

        /// <summary>
        /// The smaller the number of the rank, the higher the precedence. 
        /// i.e. Number 1 will be treated as first.
        /// </summary>
        [Display(Name = "Rank")]
        public int Rank { get; set; }

        /// <summary>
        /// This is the Rule that will be applied
        /// </summary>
        [Display(Name = "Rule")]
        public DiscountRuleENUM DiscountRuleEnum { get; set; }

        /// <summary>
        /// This is where the rule will apply, Sale or Purchase at the moment.
        /// </summary>
        [Display(Name = "Type")]
        public DiscountTypeENUM DiscountTypeEnum { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public override string NameInput1
        {
            get
            {
                return "Rank";
            }
        }
        public override string Input1SortString
        {
            get
            {
                return Rank.ToString("00000");
            }
        }


        public override string NameInput2
        {
            get
            {
                return "Type";
            }
        }
        public override string Input2SortString
        {
            get
            {
                return FixNameFor(this);
            }
        }



        //public override string NameInput3
        //{
        //    get
        //    {
        //        return "Rule";
        //    }
        //}
        //public override string Input3SortString
        //{
        //    get
        //    {
        //        return Enum.GetName(typeof(DiscountRuleENUM), DiscountRuleEnum).ToString() + Enum.GetName(typeof(DiscountTypeENUM), DiscountTypeEnum).ToString();
        //    }
        //}



        public string FixNameFor(DiscountPrecedence discountPrecedence)
        {
            string discRuleName = FixDiscountRuleName(discountPrecedence.DiscountRuleEnum.ToString());
            string finalOutput = "";
            if (User.IsNull())
            {

                finalOutput = string.Format("{0} - {1}",
                    discountPrecedence.DiscountTypeEnum.ToString().ToSentence().ToTitleCase(),
                    discRuleName);
            }
            else
            {
                finalOutput = string.Format("{0} - {1} - {2}",
                    discountPrecedence.DiscountTypeEnum.ToString().ToSentence().ToTitleCase(),
                    discRuleName,
                    User.UserName);
            }

            return finalOutput;
        }

        public static string FixDiscountRuleName(string discountRuleStr)
        {
            string fullname = discountRuleStr.ToSentence().ToTitleCase();
            string discRuleName = Regex.Replace(fullname, "And", " - AND - ");

            //Get ridm of the underscores...
            discRuleName = Regex.Replace(discRuleName, "_", "");
            return discRuleName;
        }

        public override string FullName()
        {
            return string.Format("{0} [{1}]", Name, Rank.ToString("00000"));
        }

        #region Error Checks


        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            Check_DiscountRuleENUM();
            Check_DiscountTypeEnum();
            Check_Rank();
        }

        private void Check_Rank()
        {
            //Todo DiscountPrecedence.Check_Rank
        }

        private void Check_DiscountRuleENUM()
        {
            if (DiscountRuleEnum == DiscountRuleENUM.Unknown)
            {
                throw new Exception("Discount rule is Unknown. DiscountPrecedence.Check_DiscountEnum");
            }
        }
        private void Check_DiscountTypeEnum()
        {
            if (DiscountTypeEnum == DiscountTypeENUM.Unknown)
            {
                throw new Exception("Discount Type is Unknown. DiscountPrecedence.Check_DiscountEnum");
            }
        }



        #endregion        //public void LoadFrom(DiscountPrecedence d)

        /// <summary>
        /// This re-indexes the ranking
        /// </summary>
        /// <param name="listIn"></param>
        /// <returns></returns>
        public static List<DiscountPrecedence> FixRanks(List<DiscountPrecedence> listIn)
        {
            if (listIn.IsNullOrEmpty())
                return null;

            listIn.Sort();

            int curRank = 0;
            foreach (var d in listIn)
            {
                curRank = curRank + RANK_SPACING_CONST;
                d.Rank = curRank;
            };

            return listIn;

        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            DiscountPrecedence dp = obj as DiscountPrecedence;

            if (dp.IsNull())
                throw new Exception("Object Recieved in CompareTo is not DiscountPrecedence. DiscountPrecedence");

            if (dp.Rank < Rank)
                return 1;


            if (dp.Rank > Rank)
                return -1;

            return 0;
        }

        #endregion


        public override List<string> FieldsToLoadFromView()
        {
            List<string> lst = base.FieldsToLoadFromView();

            lst.Add("Rank");
            lst.Add("DiscountRuleEnum");
            lst.Add("DiscountTypeEnum");
            lst.Add("UserId");

            return lst;
        }

        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);

            DiscountPrecedence d = ic as DiscountPrecedence ;

            if(d == null)
            {
                throw new Exception("Unable to box DiscountPrecedence. UpdatePropertiesDuringModify");
            }

            Rank = d.Rank;
            DiscountRuleEnum = d.DiscountRuleEnum;
            DiscountTypeEnum = d.DiscountTypeEnum;
            UserId = d.UserId;

        }
    }
}