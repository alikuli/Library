using ModelsClassLibrary.ModelsNS;
using System.ComponentModel.DataAnnotations;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.SharedNS;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;

namespace ModelsClassLibrary.ModelsNS
{

    /// <summary>
    /// This setups all the values for the program.
    /// Note: Name contains the string value of SetupEnum.
    /// </summary>
    public class Setup : CommonWithId
    {


        #region Properties
        [MaxLength(1000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        public string Description { get; set; }

        [Required]
        public EnumTypes Type { get; set; }


        public string Value { get; set; }
        public string HelpStringValue { get; set; }

        
        #endregion

        public override string FullName()
        {
            string currValue = Value.ToTitleCase();;

            if (Value.IsNullOrEmpty())
                currValue = "(none)";
            string theName = string.Format("{0} [Curr Value: {1}]", Description, currValue);
            return theName;
        }

        public void LoadFrom(Setup s)
        {
            Description = s.Description;
            Type = s.Type;
            Value = s.Value;
            HelpStringValue = s.HelpStringValue;

            ICommonWithId t = this as ICommonWithId;
            LoadFrom(s as ICommonWithId);
        }

    }


}