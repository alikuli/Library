using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;

namespace ModelsClassLibrary.ModelsNS.MessagesNS
{
    public class OwnersChildProductsWithLink
    {
        public OwnersChildProductsWithLink()
        {

        }
        public OwnersChildProductsWithLink(ProductChild productChild, string returnUrl)
        {
            ProductChild = productChild;
            ReturnUrl = returnUrl;
        }

        /// <summary>
        /// this is the product child of the owner
        /// </summary>
        public ProductChild ProductChild { get; set; }


        //this is the landing page link of the product child
        public MenuENUM MenuEnum { get { return MenuENUM.IndexDefault; } }


        /// <summary>
        /// this will be the returnUrl to the message.
        /// </summary>
        public string ReturnUrl { get; set; }

        public string Controller { get { return "ProductChilds"; } }
        public string Action { get { return "ProductChildLandingPage"; } }
    }
}
