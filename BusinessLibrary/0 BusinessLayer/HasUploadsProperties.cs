using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using UowLibrary.Abstract;
using UowLibrary.Interface;

namespace UowLibrary
{
    /// <summary>
    /// These are properties we can use.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {


        public bool IsHasUploads
        {
            get
            {
                return !IHasUploadsEntity.IsNull();

            }
        }


        public bool IsUserHasUploads
        {
            get
            {
                return !IUserHasUploadsEntity.IsNull();

            }
        }


        public IHasUploads IHasUploadsEntity
        {
            get
            {
                Type t = typeof(TEntity);
                var entity = Activator.CreateInstance(t);
                IHasUploads i = entity as IHasUploads;
                return i;
            }

        }

        public IHasUploads IUserHasUploadsEntity
        {
            get
            {
                Type t = typeof(TEntity);
                var entity = Activator.CreateInstance(t);
                IUserHasUploads i = entity as IUserHasUploads;
                return i;
            }

        }

    }
}
