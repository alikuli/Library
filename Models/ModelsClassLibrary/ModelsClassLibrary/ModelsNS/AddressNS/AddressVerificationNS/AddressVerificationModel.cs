
namespace ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS
{
    public class AddressVerificationModel
    {
        public void Load(string verificatioNumber, string addressMailTo, string requestDate, string inProccessDate, string letterNumber)
        {
            VerificationNumber = verificatioNumber;
            AddressMailTo = addressMailTo;
            RequestDate = requestDate;
            InProcessDate = InProcessDate;
            LetterNumber = letterNumber;
        }
        public string VerificationNumber { get; set; }
        public string AddressMailTo { get; set; }
        public string RequestDate { get; set; }
        public string InProcessDate { get; set; }

        public string LetterNumber { get; set; }
        public string CompleteNumber(string batchNumber)
        {
            string completeNumber = string.Format("Document No: {0}-{1}", batchNumber, LetterNumber);
            return completeNumber;
        }

    }
}
