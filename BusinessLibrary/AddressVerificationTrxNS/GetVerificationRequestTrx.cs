using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using System.Linq;

namespace UowLibrary.AddressNS
{
    public partial class AddressVerificationTrxBiz
    {

        public IQueryable<AddressVerificationTrx> GetVerificationRequestTrx_Pakistan()
        {
            string pakistanId = CountryBiz.PakistanId;
            var iq = GetTrxFor(VerificaionStatusENUM.Requested).Where(x => x.Address.CountryId == pakistanId);
            return iq;
        }

        public IQueryable<AddressVerificationTrx> GetVerificationRequestTrx_Other()
        {
            string pakistanId = CountryBiz.PakistanId;
            var iq = GetTrxFor(VerificaionStatusENUM.Requested).Where(x => x.Address.CountryId != pakistanId);
            return iq;
        }

        public IQueryable<AddressVerificationTrx> GetTrxFor(VerificaionStatusENUM verificaionStatusEnum)
        {
            var trx = FindAll()
                .Where(x => x.VerificaionStatusEnum == verificaionStatusEnum);
            return trx;
        }

    }
}
