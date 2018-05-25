using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AliKuli.Extentions;

namespace AliKuli.Validators
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple=false)]
    public class IsNumericAttribute: ValidationAttribute
    {

        private const string defaultError="Sorry, this is not numeric. Please enter a number. Try again!";
        public string ErrorMessageNotNumber { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string theValueString = Convert.ToString( value);

            if (theValueString.IsNullOrEmpty())
            {
                return ValidationResult.Success;
            }

            //check to see if the string is a number, double
            double dbl;
            bool succes = double.TryParse(theValueString, out dbl);

            if (succes)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessageNotNumber??defaultError);
        }



        
    }
}