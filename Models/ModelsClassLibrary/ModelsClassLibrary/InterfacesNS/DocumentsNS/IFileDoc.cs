using System;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.SharedNS;



namespace InterfacesLibrary.DocumentsNS
{
    public interface IFileDoc : ICommonWithId
    {
        void Add_FileCategoryToFileAndCategoryVMs(Guid catId, string catName, bool assigned);
        //ICollection<IFileCategory> Categories { get; set; }
        //ICollection<IFileDoc> ChildFiles { get; set; }
        //IList<FileAndCategoryVM> FileAndCategoryVMs { get; set; }
        long FileNumber { get; set; }
        string FileNumberComplete();
        int GetFileNumberFromOldFileNumber();
        int GetFileNumberFromOldFileNumber(string oldFileNumberIn);
        int GetOldFileNumber();
        //void Initialize();
        void LoadFrom(IFileDoc fileDoc);
        void MarkSelectedCategories();
        int OldCategoryId { get; set; }
        string OldFileNumber { get; set; }
        int OldId { get; set; }
        int OldParentId { get; set; }
        IFileDoc Parent { get; set; }
        Guid? ParentId { get; set; }
        void RemoveAll_FileAndCategoryVMs();
        //void SelfErrorCheck();
        string ToString();
        IUser User { get; set; }
        Guid UserId { get; set; }
    }
}
