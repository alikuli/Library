//using AliKuli.Extentions;
//using ModelsClassLibrary.ModelsNS.ProductNS;
//using ModelsClassLibrary.ModelsNS.SharedNS;
//using ModelsClassLibrary.ViewModels;
//using System;
//using System.Reflection;

//namespace UowLibrary
//{
//    public partial class ProductIdentifierBiz 
//    {



//        public override void HandleRelatedRecord(ControllerCreateEditParameter parm)
//        {
//            base.HandleRelatedRecord(parm);
//            ProductIdentifier pi = parm.Entity as ProductIdentifier;

//            if(pi.IsNull())
//            {
//                ErrorsGlobal.Add("Programming Error. Entity is null", MethodBase.GetCurrentMethod());
//                throw new Exception(ErrorsGlobal.ToString());
//            }

//            string prodId = pi.ProductId;

//            if (prodId.IsNullOrWhiteSpace())
//                return;

//            Product p = _productBiz.Find(prodId);

//            if(p.IsNull())
//            {
//                ErrorsGlobal.Add("Product not found.", MethodBase.GetCurrentMethod());
//                throw new Exception(ErrorsGlobal.ToString());

//            }

//            //Add the identifier to the product.
//            p.ProductIdentifiers.Add(pi);
//        }



//    }
//}
