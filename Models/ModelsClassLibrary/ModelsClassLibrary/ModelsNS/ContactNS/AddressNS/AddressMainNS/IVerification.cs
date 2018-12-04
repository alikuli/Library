using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public interface IVerification
    {
        DateAndByComplex AcceptDate { get; set; }
        VerificaionStatusENUM VerificaionStatusEnum { get; set; }
        DateAndByComplex FailedDate { get; set; }
        DateAndByComplex MailedDate { get; set; }
        DateAndByComplex PrintedDate { get; set; }
        DateAndByComplex ProccessExpirationDate { get; set; }
        DateAndByComplex RequestDate { get; set; }
        void SetTo(VerificaionStatusENUM addressVerificaionEnum);
        void UpdateProccessExpirationDate(int noOfDays);
        long VerificationNumber { get; set; }
        DateAndByComplex VerifiedDate { get; set; }
    }
}
