
namespace CountryDATA.ProductNS.CategoryNS
{
    public class ProductCatMainHelper
    {
        public ProductCatMainHelper (string c1, string c2, string c3)
        {
            Cat1 = c1;
            Cat2 = c2;
            Cat3 = c3;

            //string s = string.Format("\"{0}\", \"{1}\", \"{2}\"", Cat1, Cat2, Cat3);
            //return "{" + s + "}, ";

        }
        public string Cat1 { get; set; }
        public string Cat2 { get; set; }
        public string Cat3 { get; set; }

    }
}
