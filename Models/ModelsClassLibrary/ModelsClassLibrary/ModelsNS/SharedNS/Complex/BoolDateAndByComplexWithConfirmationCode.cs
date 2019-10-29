using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS.Complex
{
    [ComplexType]
    public class BoolDateAndByComplexWithConfirmationCode : DateAndByComplex
    {
        public bool IsTrue { get; set; }
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
            IsTrue = false;
        }

        public bool MatchCode(string code)
        {
            return code == ConfirmationCode;
        }

        public override void SetToTodaysDate(string byUser, string byUserId)
        {
            base.SetToTodaysDate(byUser, byUserId);
            IsTrue = true;
        }
    }
}
