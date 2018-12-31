using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.AbstractNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySell
{
    public class BuySellDoc : DocumentAbstract
    {
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.BuySellDoc;
        }

        public override bool HideNameInView()
        {
            return true;
        }

        public override string FullName()
        {
            //Owner.IsNullThrowException("Owner");
            //Customer.IsNullThrowException("Customer");
            string fullName =  Name;
            if (!Owner.IsNull())
            {
                fullName = string.Format("[{0}] {1} By: {2} To: {3}", DocumentNumber, MetaData.Created.Date_NotNull_Min.ToString("dd-MMM-yyyy"), Owner.Name, Customer.Name);

            }
            return fullName;
        }
    }
}
