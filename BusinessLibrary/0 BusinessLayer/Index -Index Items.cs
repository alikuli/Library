using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System.Linq;
using UowLibrary.Abstract;
using UowLibrary.Interface;

namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {


        #region Create Index List for Controller

        public virtual bool AddEntryToIndex { get; set; }
        /// <summary>
        /// Use this to add different fields... such as Image.
        /// </summary>
        /// <param name="indexItem"></param>
        public virtual void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            AddEntryToIndex = true;
            IHasUploads ientityHasUploads = icommonWithId as IHasUploads;

            if (!ientityHasUploads.IsNull())
            {
                indexItem.ImageAddressStr = addressOfImageForDisplay(ientityHasUploads);


            }
        }

        private string addressOfImageForDisplay(IHasUploads entity)
        {

            if (entity.MiscFiles.IsNullOrEmpty())
                return new UploadedFile().RelativePathWithFileName();

            //Get a list of images for this category item.
            UploadedFile image = entity.MiscFiles.FirstOrDefault(x => x.MetaData.IsDeleted == false);

            if (image.IsNull())
                image = new UploadedFile();


            return image.RelativePathWithFileName();

        }



        #endregion







    }
}
