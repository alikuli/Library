using System;

namespace AliKuli.ExcellUtilityLibrary
{
    //[NotMapped]
    public class FilesDocImportVM
    {
        public FilesDocImportVM()
        {
            FileId = -1;
        }
        #region Files
        /// <summary>
        /// The file number's ID
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// This is the file number that the user sees.
        /// </summary>
        public string FileNo { get; set; }

        /// <summary>
        /// This is the name of the file
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// This is the parent's ID. 0 means no ID
        /// </summary>
        public int ParentId { get; set; }

        #endregion

        public int GetFileNumberFromOldFileNumber()
        {

            if (string.IsNullOrWhiteSpace(FileNo) || FileId == -1 || string.IsNullOrWhiteSpace(Description))
                throw new Exception(string.Format("Proper Data not received. Record is {0}.FilesDocImportVM.GetFileNumberFromOldFileNumber ", this.ToString()));


            return new FileDoc().GetFileNumberFromOldFileNumber(FileNo);
        }

        public void SelfErrorCheck()
        {
            if (FileId == -1 || string.IsNullOrWhiteSpace(FileNo) || string.IsNullOrWhiteSpace(Description))
                throw new Exception(string.Format("Proper Data not received. Record is {0}. FilesDocImportVM.SelfErrorCheck", this.ToString()));

        }
        #region Category
        /// <summary>
        /// This is the categories ID
        /// </summary>
        public int CategoryId { get; set; }


        /// <summary>
        /// This is the name of the category
        /// </summary>
        public string CategoryName { get; set; }
        #endregion

        public override string ToString()
        {
            return string.Format("FileId: {0}, FileNo: {1}, Description: {2}, ParentId: {3}, CategoryId: {4}, CategoryName: {5}",
                FileId,
                FileNo,
                Description,
                ParentId,
                CategoryId,
                CategoryName);
        }

    }
}