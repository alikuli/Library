

namespace InterfacesLibrary.SharedNS
{
    public interface ICommonNoIdWithState: ICommonBasic
    {

        /// <summary>
        /// If this is true, then overall encryption for the record is on.
        /// </summary>
        bool IsEncrypted { get; set; }
        bool IsDeleted { get; set; }
        IDateAndBy Created { get; set; }
        IDateAndBy Deleted { get; set; }
        IDateAndBy Modified { get; set; }
        IDateAndBy UnDeleted { get; set; }

        string GetCreatedTicks { get; }
        string GetSelfClassName();

        string GetSelfMethodName();



    }
}
