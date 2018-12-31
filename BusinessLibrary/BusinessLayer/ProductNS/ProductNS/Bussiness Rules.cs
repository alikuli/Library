using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;


namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {
        private List<string> lstOfIds = new List<string>();
        private Stack<Product> trailStack = new Stack<Product>();

        public override void BusinessRulesFor(ControllerCreateEditParameter parm)
        {
            base.BusinessRulesFor(parm);

            Product p = parm.Entity as Product;
            GetDataFromMenuPathCheckBoxes(p);

            IProduct iproduct = p as IProduct;
            //FixProductFeatures(iproduct);
        }
    }
}
