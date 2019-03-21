
using System.ComponentModel.DataAnnotations;
namespace ModelsClassLibrary.ModelsNS.SharedNS.Parameters
{
    public class YesParameter
    {
        public YesParameter()
        {

        }


        public YesParameter(string question, string returnUrl, bool isYes)
        {
            Question = question;
            isYes = IsYes;
            ReturnUrl = returnUrl;
        }
        public string Question { get; set; }
        [Display(Name="Yes")]
        public bool IsYes { get; set; }
        public string ReturnUrl { get; set; }
    }
}
