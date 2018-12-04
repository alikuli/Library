using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.PlayersNS.SalesmanCategoryNS
{
    public partial class SalesmanCategoryBiz : BusinessLayer<SalesmanCategory>
    {
        public SalesmanCategoryBiz(IRepositry<SalesmanCategory> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }



    }
}
