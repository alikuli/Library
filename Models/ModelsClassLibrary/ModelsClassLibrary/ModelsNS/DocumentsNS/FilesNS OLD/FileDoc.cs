using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using AliKuli.Extentions;
using InterfacesLibrary.DocumentsNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using Microsoft.AspNet.Identity;
using UserModelsLibrary.ModelsNS;


namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS
{
    /// <summary>
    /// This class saves data for your concrete file cabinet. This is the file name. Every user can have their own
    /// data for their file cabinet. Empty user files are allowed. These will be for paper produced by the software
    /// </summary>
    public class FileDoc : AbstractDocHeader, IFileDoc
    {

        public FileDoc()
        {
            Categories = new List<FileCategory>();
            ChildFiles = new List<FileDoc>();
            FileAndCategoryVMs = new List<FileAndCategoryVM>();
        }

        #region User
        /// <summary>
        /// This is the owning User.
        /// </summary>
        public virtual IUser User { get; set; }
        public Guid UserId { get; set; }

        #endregion

        #region FileNumber

        /// <summary>
        /// This is a non-unique File number. You can have many files with the same number.
        /// </summary>
        [Display(Name = "File Number")]
        public long FileNumber { get; set; }


        /// <summary>
        /// This is the complete file number.
        /// </summary>
        /// <returns></returns>
        public string FileNumberComplete()
        {
            if (Parent.IsNull())
            {
                string paddedNo = FileNumber.ToString().PadLeft(5);
                return string.Format("{0}", paddedNo);
            }
            else
            {
                //need recursion here. There may be several parents
                string paddedNo = FileNumber.ToString().PadLeft(5);
                string paddedNoParent = Parent.FileNumber.ToString().PadLeft(5);
                return string.Format("{0}-{1}", paddedNoParent, paddedNo);
            }

        }

        #endregion

        #region Parent
        /// <summary>
        /// This is the parent file. The current file lies inside the parent file.
        /// The current files numbering within the parent will again begin at 1
        /// and the file name will be: Parent.Filenumber - This.FileNumber
        /// </summary>
        /// 
        [Display(Name = "Parent")]

        public Guid? ParentId { get; set; }
        public virtual IFileDoc Parent { get; set; }

        #endregion


        #region Access Db Data. This is the original data.

        public int OldId { get; set; }
        public int OldParentId { get; set; }
        public int OldCategoryId { get; set; }

        /// <summary>
        /// The orignal file number is the file number "x" or a combination of the parent file number and the file number "parent-FileNumber"
        /// So, this always gets the filenumber
        /// </summary>
        /// <returns></returns>
        /// 
        [MaxLength(10)]
        public string OldFileNumber { get; set; }



        /// <summary>
        /// The orignal file number is the file number "x" or a combination of the parent file number and the file number "parent-FileNumber"
        /// So, this always gets the filenumber
        /// </summary>
        /// <returns></returns>
        public int GetFileNumberFromOldFileNumber(string oldFileNumberIn)
        {
            if (oldFileNumberIn.IsNullOrEmpty())
                throw new Exception("No filenumber received");

            string[] fileNumber = oldFileNumberIn.Split('-');

            switch (fileNumber.Count())
            {
                case 0: throw new Exception(string.Format("Error. No file number received. Data received is : '{0}. FilesDocImportVM.GetFileNumber ", oldFileNumberIn));
                case 1: return fileNumber[0].ToInt();
                case 2: return fileNumber[1].ToInt();
                default: throw new Exception(string.Format("Error. No file number received. Out of range. Data received is : '{0}.FilesDocImportVM.GetFileNumber", oldFileNumberIn));
            }
        }

        public int GetFileNumberFromOldFileNumber()
        {
            return GetFileNumberFromOldFileNumber(OldFileNumber);
        }

        public int GetOldFileNumber()
        {
            var fileNoArray = OldFileNumber.Split('-');

            if (fileNoArray.Count() == 2)
                return int.Parse(fileNoArray[1]);
            else
                return int.Parse(fileNoArray[0]);

        }


        #endregion

        #region Navigation Properties
        public virtual ICollection<FileCategory> Categories { get; set; }

        public virtual ICollection<FileDoc> ChildFiles { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            string parentName = Parent.IsNull() ? "" : Parent.Name;

            string categories = StringifyCategories();
            string children = StringifyChildren();

            string userName = User.IsNull() ? "" : ((User)User).UserName;

            string error = string.Format("User: '{9}', Name: '{7}', DocNo: '{8}', FileNumber: '{0}', ParentName: '{1}', OldId: '{2}', OldParentId: '{3}', OldCategoryId: '{4}', Categories: '{5}', ChildFiles: '{6}'",
                FileNumber,
                parentName,
                OldId,
                OldParentId,
                OldCategoryId,
                categories,
                children,
                Name,
                DocNo,
                userName);

            return error;
        }

        private string StringifyChildren()
        {
            if (ChildFiles.IsNullOrEmpty())
                return "";

            var childrenList = ChildFiles.ToList();

            string children = "";

            if (!childrenList.IsNullOrEmpty())
            {
                foreach (var item in childrenList)
                {
                    children += item.Name + "; ";
                }
            }
            return children;
        }

        private string StringifyCategories()
        {
            if (Categories.IsNullOrEmpty())
                return "";

            string categories = "";
            var categoriesList = Categories.ToList();
            if (!(categoriesList.IsNullOrEmpty()))
            {
                foreach (var item in categoriesList)
                {
                    categories += item.Name + "; ";
                }
            }
            return categories;
        }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            ErrorCheck_User();
        }

        #endregion

        #region ErrorCheck Helpers
        private void ErrorCheck_User()
        {
            if (User.IsNull())
                throw new Exception("There is no user. Not allowed. FileDoc.Check_User");

            if (UserId.IsNullOrEmpty())
                throw new Exception("User Id is empty. Not allowed. FileDoc.Check_User");


        }

        #endregion

        #region FileAndCategoryVMs List and helpers
        /// <summary>
        /// This holds a list of all the file categories. The ones that are assigned are marked true
        /// and the others are false.
        /// </summary>
        [NotMapped]
        public List<FileAndCategoryVM> FileAndCategoryVMs { get; set; }


        /// <summary>
        /// This adds a category to the list after making sure that it does not already exist
        /// </summary>
        /// <param name="catId"></param>
        /// <param name="assigned"></param>
        public void Add_FileCategoryToFileAndCategoryVMs(Guid catId, string catName, bool assigned)
        {
            bool categoryFound = false;

            if ((((ICollection<object>)FileAndCategoryVMs).IsNullOrEmpty()))
            {
                var cat = FileAndCategoryVMs.Where(x => x.CategoryId == catId).FirstOrDefault();
                categoryFound = cat != null;
            }
            if (!categoryFound)
            {
                FileAndCategoryVM fAndCvm = new FileAndCategoryVM(catId, catName, assigned);
                FileAndCategoryVMs.Add(fAndCvm);
            }
        }

        public void RemoveAll_FileAndCategoryVMs()
        {
            if ((((ICollection<object>)FileAndCategoryVMs).IsNullOrEmpty()))
                return;

            FileAndCategoryVMs.RemoveAll(RemoveAllEntries);
        }

        private static bool RemoveAllEntries(FileAndCategoryVM s)
        {
            return true;
        }
        /// <summary>
        /// This loads all the categories that are already assigned to this file and marks them true.
        /// </summary>
        public void MarkSelectedCategories()
        {
            if ((((ICollection<object>)Categories).IsNullOrEmpty()))
                return;

            foreach (var cat in Categories.ToList())
            {
                //locate the cateogry in FileAndCategoryVMs
                var f = FileAndCategoryVMs.Where(x => x.CategoryId == cat.Id).FirstOrDefault();

                if (f.IsNull())
                    throw new Exception(string.Format("File not found in FileAndCategoryVMs for rec: {0}", this.ToString()));
                f.Assigned = true;
            }
        }

        #endregion

        public void LoadFrom(IFileDoc fileDoc)
        {
            //Navigation
            Categories = ((FileDoc)fileDoc).Categories;
            ChildFiles = ((FileDoc)fileDoc).ChildFiles;

            User = fileDoc.User;
            UserId = fileDoc.UserId;
            Parent = fileDoc.Parent;
            ParentId = fileDoc.ParentId;
            FileNumber = fileDoc.FileNumber;
            OldCategoryId = fileDoc.OldCategoryId;
            OldFileNumber = fileDoc.OldFileNumber;
            OldId = fileDoc.OldId;
            OldParentId = fileDoc.OldParentId;
            FileAndCategoryVMs = ((FileDoc)fileDoc).FileAndCategoryVMs;

            this.LoadFrom(fileDoc as AbstractDocHeader);
        }

    }
}