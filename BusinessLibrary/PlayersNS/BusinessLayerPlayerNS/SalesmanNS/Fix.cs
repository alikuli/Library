using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Web.Mvc;
using AliKuli.Extentions;

namespace UowLibrary.PlayersNS.SalesmanNS
{
    public partial class SalesmanBiz
    {
        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            Salesman salesman = parm.Entity as Salesman;
            salesman.IsNullThrowException("Unable to unbox salesman");


            if (salesman.SalesmanCategoryId.IsNullOrWhiteSpace())
                salesman.SalesmanCategoryId = null;

        }

    }
}
