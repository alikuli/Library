using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS.Complex
{
    [ComplexType]
    public class BoolDateAndByComplexWithConfirmationCode : DateAndByComplex
    {
        public string ConfirmationCode { get; set; }

        public void AddCode(string userName, string userId, string code)
        {
            base.SetToTodaysDate(userName, userId);
            ConfirmationCode = code;

        }
        public void AddCodeReceived(string userName, string userId, string code)
        {
            base.SetToTodaysDate(userName, userId);
            ConfirmationCode = code;

        }
        public override void Clear()
        {
            base.Clear();
            ConfirmationCode = "";
        }

        public bool MatchCode(string code)
        {
            return code == ConfirmationCode;
        }
    }
}
