
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Common;
namespace InterfacesLibrary.SharedNS
{

    /// <summary>
    /// This interface is used so that we can make one single select list program that will work with everthing
    /// </summary>
    public interface IMetaData 
    {
        //string Comment { get; set; }
        DateAndByComplex Created { get; set; }
        DateAndByComplex Deleted { get; set; }
        DateAndByComplex Modified { get; set; }
        DateAndByComplex UnDeleted { get; set; }

        //string GetCreatedTicks { get; }
        string GetSelfClassName();
        string GetSelfMethodName();
        //bool IsEncrypted { get; set; }
        bool IsInactive { get; set; }
        bool IsEditLocked { get; set; }
        bool IsDeleted { get; set; }
        //IsEncryption is coming from IEncryption.
        //bool IsEncrypted { get; set; }
        void LoadFrom(IMetaData icommonNoId);
        void SelfErrorCheck();

        string GetCreatedTicks();


    }
}
