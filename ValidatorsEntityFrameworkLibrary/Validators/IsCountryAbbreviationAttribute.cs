using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using AliKuli.Extentions;

namespace AliKuli.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsCountryAbbreviationAttribute : ValidationAttribute
    {

        private const string defaultError = "You are only allowed alphabets (2 alphabets Eg: 'PK' for Pakistan, 'US' for USA, 'UK' for England etc.) ";

        public string ErrorMessageCountryAbbreviationString{get;set;}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string theValueString = (string)value;

            //This is checked by the Required Attribute. If I dont have this I get an error when saving the
            //record because this comes as empty.
            if (theValueString.IsNullOrEmpty())
            {
                return ValidationResult.Success;
            }

            //check to see if the string is a number, double
            Regex rx = new Regex(@"^[a-zA-Z]+$");

            if (!rx.IsMatch(theValueString))
            {
                return new ValidationResult(ErrorMessageCountryAbbreviationString ?? defaultError);
            }
            if (theValueString.Trim().Length!=2)
            {
                return new ValidationResult(ErrorMessageCountryAbbreviationString ?? defaultError);
            }

            return ValidationResult.Success;

        }




    }
}