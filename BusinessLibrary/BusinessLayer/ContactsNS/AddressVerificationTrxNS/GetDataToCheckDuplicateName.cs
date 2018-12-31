using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using System.Linq;

namespace UowLibrary.AddressNS
{
    public partial class AddressVerificationTrxBiz
    {

        /// <summary>
        /// The goal is to make sure a duplicate verification request is not issued. All AddressVerificationTrx for the address that is be
        /// verified are brought without failure ones. If nothing is open, then verification can be issued.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override IQueryable<AddressVerificationTrx> GetDataToCheckDuplicateName(AddressVerificationTrx entity)
        {
            var allForAddress = FindAll().Where(x => x.AddressId == entity.AddressId);
            //Now we want to check if mailer has anything open for this address in proccess or other wise
            //only ones we do not care about are the ones that have failed.

            var dataToCheckForDuplicates = allForAddress.Where(x => x.Verification.VerificaionStatusEnum != VerificaionStatusENUM.Failed);
            return dataToCheckForDuplicates;
        }
    }
}
