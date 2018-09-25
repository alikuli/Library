using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {

        public override void EVENT_DeleteRelatedRecordsActually(ICommonWithId entity)
        {
            base.EVENT_DeleteRelatedRecordsActually(entity);

            Product p = entity as Product;
            p.IsNullThrowException("Programming error. Entity is not Product.");

            //Delete the Product Identifiers Uploads
            deleteProductIdentifiersActually(p);
            deleteChildProductsUploadsActually(p);
            //deleteProductUploadsActually(p);
            //delete Products own Upload Records
            deleteUploadsAndPhysicalFiles(p.MiscFiles);
            
            
            
            //Delete the Product Product Children Uploads
        }

        private void deleteUploadsAndPhysicalFiles(ICollection<UploadedFile> uploads)
        {
            if (!uploads.IsNullOrEmpty())
            {
                List<string> lstOfUploadsToDelete = new List<string>();
                //delete the physical files and note the Upload Id
                foreach (var upload in uploads)
                {
                    //delete the physical file.
                    AliKuli.ToolsNS.FileTools.Delete(upload.AbsolutePathWithFileName());
                    lstOfUploadsToDelete.Add(upload.Id);

                }

                //delete the records now
                if (!lstOfUploadsToDelete.IsNull())
                {
                    foreach (var id in lstOfUploadsToDelete)
                    {
                        UploadedFileBiz.DeleteActuallyAndSave(id);
                    }
                }

                uploads.Clear();
            }
        }


        //private void deleteProductUploadsActually(Product p)
        //{

        //    var productUploads = p.MiscFiles;

        //    if (productUploads.IsNullOrEmpty())
        //        return;

        //    List<string> lstIdsToDelete = new List<string>();
        //    foreach (var pi in productUploads)
        //    {
        //        lstIdsToDelete.Add(pi.Id);
        //    }

        //    foreach (var id in lstIdsToDelete)
        //    {
        //        UploadedFileBiz.DeleteActuallyAndSave(id);
        //    }
        //    productUploads.Clear();
        //}

        private  void deleteProductIdentifiersActually(Product p)
        {

            var productIdentifiers = p.ProductIdentifiers;

            if (productIdentifiers.IsNullOrEmpty())
                return;

            List<string> lstIdsToDelete = new List<string>();
            foreach (var pi in productIdentifiers)
            {
                lstIdsToDelete.Add(pi.Id);
            }

            foreach (var id in lstIdsToDelete)
            {
                ProductIdentifierBiz.DeleteActuallyAndSave(id);
            }
            productIdentifiers.Clear();

        }


        private void deleteChildProductsUploadsActually(Product p)
        {

            var childProducts = p.ProductChildren;

            if (childProducts.IsNullOrEmpty())
                return;

            List<string> lstOfIdsToDelete = new List<string>();

            foreach (var child in childProducts)
            {
                deleteUploadsAndPhysicalFiles(child.MiscFiles);
                lstOfIdsToDelete.Add(child.Id);
            }

            lstOfIdsToDelete.IsNullThrowException("Programming error. List of ids cannot be null.");

            foreach (var id in lstOfIdsToDelete)
            {
                ProductChildBiz.DeleteActuallyAndSave(id);

                
            }

            childProducts.Clear();

        }

    }
}
