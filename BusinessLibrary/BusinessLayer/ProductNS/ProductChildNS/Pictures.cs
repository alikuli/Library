//using AliKuli.Extentions;
//using InterfacesLibrary.SharedNS;
//using InterfacesLibrary.SharedNS.FeaturesNS;
//using ModelsClassLibrary.MenuNS;
//using ModelsClassLibrary.ModelsNS.FeaturesNS;
//using ModelsClassLibrary.ModelsNS.MenuNS;
//using ModelsClassLibrary.ModelsNS.PlayersNS;
//using ModelsClassLibrary.ModelsNS.ProductChildNS;
//using ModelsClassLibrary.ModelsNS.ProductNS;
//using ModelsClassLibrary.ModelsNS.SharedNS;
//using ModelsClassLibrary.ModelsNS.UploadedFileNS;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;

//namespace UowLibrary.ProductChildNS
//{
//    public partial class ProductChildBiz
//    {


//        public override List<string> GetPictureList(IHasUploads ihasUploads)
//        {
//            List<string> addresses = new List<string>();

//            if (ihasUploads.MiscFiles.Any(x => !x.MetaData.IsDeleted))
//            {
//                var lstUploadedFiles = ihasUploads.MiscFiles.Where(x => !x.MetaData.IsDeleted).ToList();
//                lstUploadedFiles.IsNullOrEmptyThrowException("Something went worng. This list cannot be empty.");
//                foreach (UploadedFile uploadFile in lstUploadedFiles)
//                {
//                    string pictureAddy = getImageAddressOf(uploadFile);
//                    if (!pictureAddy.IsNullOrWhiteSpace())
//                    {
//                        addresses.Add(pictureAddy);

//                    }
//                }
//            }

//            if (addresses.IsNullOrEmpty())
//            {
//                return GetDefaultPicture(ihasUploads as ProductChild);
//            }

//            return addresses;
//        }
//        /// <summary>
//        /// We need to load this because GetDefaultPicture override does not take a parameter
//        /// </summary>
//        //public ProductChild ProductChildForDefaultPicture { get; set; }

//        public List<string> GetDefaultPicture(ProductChild productChildForDefaultPicture)
//        {
//            List<string> lst = new List<string>();
//            productChildForDefaultPicture.IsNullThrowException("ProductChildForDefaultPicture");
//            productChildForDefaultPicture.Product.IsNullThrowException("ProductChildForDefaultPicture.Product");
//            Product product = productChildForDefaultPicture.Product;

//            if (product.MiscFiles.IsNullOrEmpty())
//            {
//                base.GetDefaultPicture();

//            }
//            else
//            {
//                foreach (UploadedFile uploadedFile in product.MiscFiles)
//                {
//                    string pictureAddy = getImageAddressOf(uploadedFile);
//                    if (pictureAddy.IsNullOrWhiteSpace())
//                        continue;
//                    lst.Add(pictureAddy);
//                }
//            }

//            return lst;
//        }

//    }
//}
