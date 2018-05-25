
namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public class ProductTrx : ProductTrxAbstract
    {
        public void LoadFrom(ProductTrx p)
        {
            base.LoadFrom(p as ProductTrxAbstract);
        }
    }
}