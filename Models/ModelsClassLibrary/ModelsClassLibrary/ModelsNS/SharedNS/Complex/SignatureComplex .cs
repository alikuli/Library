using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS.Complex
{
    [ComplexType]
    public class SignatureComplex : DateAndByComplex
    {
        public bool Signed { get; set; }
        string Comment { get; set; }

        public void SignDocumentBy(string userName, string userId, string comment)
        {
            Signed = true;
            Comment = comment;
            base.SetToTodaysDate(userName, userId);
        }
        public override void Clear()
        {
            Signed = false;
            Comment = "";
            Clear();
        }
    }
}
