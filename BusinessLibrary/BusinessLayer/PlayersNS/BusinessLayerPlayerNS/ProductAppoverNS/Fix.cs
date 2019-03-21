using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.ProductApproverNS
{
    public partial class ProductApproverBiz
    {
        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            ProductApprover productApprover = parm.Entity as ProductApprover;
            productApprover.IsNullThrowException("Unable to unbox product approver");


            if (productApprover.ProductApproverCategoryId.IsNullOrWhiteSpace())
                productApprover.ProductApproverCategoryId = null;

        }
    }
}
