//using System;
//using System.Collections.Generic;
//using System.Linq;
//using AliKuli.ExcellUtilityLibrary;
//using AliKuli.Extentions;
//using AliKuli.UtilityNS.Excell;
//using DAL;
//using DalLibrary.DalNS.DocumentNS;
//using Microsoft.AspNet.Identity;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS;
//using UserModels;
//using UserModels.Models;



//namespace DalLibrary.DalNS
//{
//    public class FileDocDAL : AbstractDocHeaderDAL<FileDoc>
//    {

//        #region Constructor
//        public FileDocDAL(ApplicationDbContext db, IUser user)
//            : base(db, user)
//        {
//            Errors.ResetLibAndClass("FileDocDAL");

//        }

//        #endregion


//        #region Fix

//        public override void Fix(FileDoc entity)
//        {
//            base.Fix(entity);
//            Fix_User(entity);
//            Fix_Categories(entity);

//        }

//        /// <summary>
//        /// This adds the new file categories
//        /// </summary>
//        /// <param name="entity"></param>


//        private void Fix_User(FileDoc entity)
//        {
//            if (entity.User.IsNull())
//            {
//                if (entity.UserId.IsNullOrEmpty())
//                {
//                    throw new Exception(string.Format("No user found. Record: {0}. {1}", entity.ToString(), GetSelfMethodName()));
//                }
//                entity.User = (IUser)(new UserDAL(_db, _user).FindUserById(entity.UserId));

//                if (entity.User.IsNull())
//                {
//                    throw new Exception(string.Format("No user found (2). Record: {0}. {1}", entity.ToString(), GetSelfMethodName()));

//                }

//            }
//            else
//            {
//                if (entity.UserId.IsNullOrEmpty())
//                {
//                    entity.UserId = entity.User.Id;
//                }

//            }
//        }

//        private void Fix_Categories(FileDoc entity)
//        {

//            if (entity.FileAndCategoryVMs.IsNullOrEmpty())
//            {
//                //throw new Exception(string.Format("FileAndCategoryVMs is empty For: {0}. FileDocDAL.Fix_LoadCategoriesFrom_FileAndCategoryVMS ",entity.ToString()));
//                return; //nothing to fix... it is already fixed
//            }

//            var catMarkedTrue = entity.FileAndCategoryVMs.Where(x => x.Assigned == true).ToList();

//            RemoveAllCategoriesFromEntity(entity);


//            if (catMarkedTrue.IsNullOrEmpty())
//                return;

//            //categories is empty now. So add away safely.
//            FileCategoryDAL fcDAL = new FileCategoryDAL(_db, _user);
//            foreach (var fileCat in catMarkedTrue)
//            {
//                FileCategory fc = fcDAL.FindFor(fileCat.CategoryId);
//                entity.Categories.Add(fc);
//            }
//        }


//        #endregion


//        #region FileAndCategoryVMS Helpers


//        private static void RemoveAllCategoriesFromEntity(FileDoc entity)
//        {
//            if (!(entity.Categories.IsNullOrEmpty()))
//            {
//                //remove the old categories
//                foreach (var fCat in entity.Categories.ToList())
//                {
//                    entity.Categories.Remove(fCat);
//                    fCat.Files.Remove(entity);
//                }

//            }
//        }

//        public void Initialize_FileAndCategoryVMs(FileDoc entity)
//        {
//            //Before removing, add the selected items to list and
//            //later mark them true.
//            //first clear the list of previous info
//            entity.RemoveAll_FileAndCategoryVMs();



//            FileCategoryDAL fcDAL = new FileCategoryDAL(_db, _user);

//            var lstFileCategories = fcDAL.FindAllLight().ToList();

//            if (!lstFileCategories.IsNullOrEmpty())
//            {
//                foreach (var cat in lstFileCategories)
//                {
//                    entity.Add_FileCategoryToFileAndCategoryVMs(cat.Id, cat.Name, false);
//                }
//            }

//            //now mark the selected categories true
//            entity.MarkSelectedCategories();
//            entity.FileAndCategoryVMs = entity.FileAndCategoryVMs
//                .OrderByDescending(x => x.Assigned)
//                .ThenBy(x => x.Name)
//                .ToList();
//        }

//        #endregion

//        #region Get Next Number
//        public override long GetNextDocNumber()
//        {

//            var fileDocs = FindAllLight().ToList();
//            if (fileDocs.IsNullOrEmpty())
//                return 1;

//            return _db.FileDocs.Max(x => x.DocNo) + 1;
//        }
//        public long GetNextFileNumber(Guid? parentId)
//        {

//            //get current user's role.Allow
//            var fileDocs = FindAllLight();

//            if (fileDocs.IsNullOrEmpty())
//                return 1;


//            if (!parentId.IsNullOrEmpty())
//                fileDocs = fileDocs.Where(x => x.ParentId == parentId);


//            if (fileDocs.IsNullOrEmpty())
//                return 1;

//            return fileDocs.Max(x => x.FileNumber) + 1;

//        }

//        /// <summary>
//        /// This returns the next file number. IF the item is a child item
//        /// then it starts from 1 and continues from the doc number that was the previous
//        /// one in the child item.
//        /// </summary>
//        /// <param name="parentId"></param>
//        /// <returns></returns>
//        public long GetNextDocNumber(Guid parentId)
//        {
//            //get last number for parent Id
//            var allChildren = FindAllLight().Where(x => x.ParentId == parentId);

//            if (allChildren.IsNullOrEmpty())
//                return 1;

//            return allChildren.ToList().Max(x => x.DocNo) + 1; ;
//        }

//        #endregion

//        #region Make Names
//        public override string MakeNameForIndexMethod(FileDoc entity)
//        {
//            //This loads the value...
//            string temp = entity.Name;

//            return string.Format("{2} {0} {1}",
//                entity.Name,
//                MakeCategoryNamesForIndex(entity),
//                entity.FileNumberComplete());
//        }

//        private string MakeCategoryNamesForIndex(FileDoc entity)
//        {

//            return StringifyCategories(entity);
//        }

//        #endregion

//        #region Read From Excell
//        private string StringifyCategories(FileDoc entity)
//        {
//            if (entity.Categories.IsNullOrEmpty())
//                return "";

//            List<FileCategory> listOfCategories = ((List<FileCategory>)entity.Categories).ToList();
//            string categoryNames = string.Empty;

//            try
//            {

//                if (!listOfCategories.IsNullOrEmpty())
//                {
//                    foreach (var item in listOfCategories)
//                    {
//                        string temp = item.Name;
//                        categoryNames += string.Format("[{0}] ", item.Name);
//                    }
//                }
//                return categoryNames;

//            }
//            catch (Exception e)
//            {
//                Errors.Add("", "StringifyCategories", e);
//                throw new Exception(Errors.ToString());



//                throw new Exception(Errors.ToString());
//            }
//        }


//        /// <summary>
//        /// This loads data from the Excel Sheet
//        ///     Col 0: File Number
//        ///     Col 1: File Description
//        ///     Col 2: Category Name
//        /// </summary>
//        /// <param name="excelFileName"></param>
//        public string LoadFromExcel(string excelFileName)
//        {
//            int noOfFiles = 0;

//            var excelFile = ExcellUtility.ImportFromExcelWithHeader(excelFileName, "AliKuliFiles");
//            if (excelFile.IsNullOrEmpty())
//                throw new Exception(string.Format("Utility Class failed to load. FileDocsDAL.LoadFromExcel"));

//            string totalFilesMsg = string.Format("Total Orignal Files: {0}", excelFile.Count());

//            var dataArray = excelFile.OrderBy(x => x.FileId).ToList();

//            if (dataArray.IsNullOrEmpty())
//                throw new Exception(string.Format("Data array failed to load. FileDocsDAL.LoadFromExcel"));

//            CheckTheData(dataArray);

//            //first make the categories
//            ApplicationUser theUser = (ApplicationUser)_user;


//            CreateCategories(dataArray, theUser);
//            CreateAndSaveFile(ref noOfFiles, dataArray, theUser);

//            //now add the parents
//            SaveTheParents();

//            totalFilesMsg += " Counted: " + noOfFiles;
//            //CreateFileWithCategory(dataArray, admin);
//            return totalFilesMsg;
//        }

//        private void CreateAndSaveFile(ref int noOfFiles, List<FilesDocImportVM> dataArray, ApplicationUser theUser)
//        {
//            foreach (var data in dataArray)
//            {
//                try
//                {
//                    //How much is the data? This is where its getting lost here.
//                    int fileNumber = data.GetFileNumberFromOldFileNumber();

//                    CreateFiles(
//                        data.Description,
//                        fileNumber,
//                        theUser,
//                        data.FileId,
//                        data.FileNo,
//                        data.CategoryId,
//                        data.ParentId);

//                    noOfFiles++;
//                    Save();
//                }
//                catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
//                {

//                    continue;
//                }
//                catch (Exception e)
//                {

//                    string error = string.Format("Error. Record is {0}. NoOfRecords: {2}",
//                        data.ToString(),
//                        noOfFiles);

//                    Errors.Add(error, "CreateAndSaveFile", e);
//                    throw new Exception(Errors.ToString());
//                }

//            }
//        }

//        private void CheckTheData(List<FilesDocImportVM> dataArray)
//        {

//            try
//            {
//                foreach (var data in dataArray)
//                {
//                    data.SelfErrorCheck();
//                }

//            }
//            catch (Exception e)
//            {


//                string error = string.Format("Data is incomplete (selfErrorCheck) {0}", dataArray.ToString());

//                Errors.Add(error, "CheckTheData", e);
//                throw new Exception(Errors.ToString());


//            }

//        }

//        private void SaveTheParents()
//        {
//            //get all files with parents
//            var filesWithParents = SearchFor(x => x.OldParentId != 0).ToList();

//            if (!filesWithParents.IsNull())
//            {
//                foreach (var f in filesWithParents)
//                {
//                    f.Parent = SearchFor(x => x.OldId == f.OldParentId).FirstOrDefault();
//                    if (f.Parent.IsNull())
//                        throw new Exception("No parent founnd for: " + f.ToString());
//                    f.ParentId = f.Parent.Id;

//                    try
//                    {
//                        Update(f);
//                        Save();

//                    }
//                    catch (Exception e)
//                    {

//                        string error = string.Format("Error. Record is {0}. ",
//                            f.ToString());

//                        Errors.Add(error, "SaveTheParents", e);

//                        throw new Exception(Errors.ToString());
//                    }
//                }
//            }
//        }



//        //private void CreateFileWithCategory(List<FilesDocImportVM> dataArray, User admin)
//        //{

//        //    FileCategoryDAL fcDAL = new FileCategoryDAL(_db,_user);
//        //    string errorDetails = "";
//        //    foreach (var r in dataArray)
//        //    {
//        //        FileDoc f = Factory();

//        //        f.OldId = r.FileId;
//        //        f.OldParentId = r.ParentId;
//        //        f.OldCategoryId = r.CategoryId;
//        //        f.FileNumber = r.FileNo.ToString();
//        //        f.User = admin;
//        //        f.UserId = admin.Id;
//        //        f.Name = r.Description;

//        //        //Get Parent
//        //        if (f.OldParentId != 0)
//        //        {


//        //            errorDetails = string.Format("Old ParentId: {0}, Incoming Record Details: {1}", f.OldCategoryId, r.ToString());

//        //            f.Parent = SearchFor(x => x.OldId == f.OldParentId).FirstOrDefault();
//        //            if(f.Parent.IsNull())
//        //            {
//        //                //oopsie doopsie... this may mean that there are 2 same parents with same name. This is not allowed...
//        //                //its probably saved as a different OldParentId.
//        //                //so first try and locate the parent through name.

//        //                //find the parent in the original data
//        //                FilesDocImportVM origParentRec = dataArray.Where(x => x.FileId == r.ParentId).FirstOrDefault();

//        //                if(!origParentRec.IsNull())
//        //                {
//        //                    //Now find the same named file
//        //                    f.Parent = FindForName(origParentRec.Description);
//        //                }

//        //                if (f.Parent.IsNull()) //now there is a problem...
//        //                    throw new Exception(string.Format("Parent not found! Details: {0}. FileDocDAL.CreateFileWitCategory", errorDetails));
//        //            }

//        //            f.ParentId = f.Parent.Id;
//        //        }

//        //        //Now get Category
//        //        if(f.OldCategoryId !=0)
//        //        {
//        //            FileCategory fc = fcDAL.SearchFor(x => x.OldId == f.OldCategoryId).FirstOrDefault();
//        //            if(fc.IsNull())
//        //            {
//        //                //oopsie doopsie... this may mean that there are 2 same categories with same name. This is not allowed...
//        //                //its probably saved as a different OldCategoryId.
//        //                //so first try and locate the category through name.
//        //                fc = fcDAL.FindForName(r.CategoryName);

//        //                if (fc.IsNull())
//        //                {
//        //                    errorDetails = string.Format("Old Category name: {0}, Incoming Record Details: {1}", r.CategoryName, r.ToString());
//        //                    throw new Exception(string.Format("Category not found! Details: {0}. FileDocDAL.CreateFileWitCategory", errorDetails));
//        //                }
//        //            }

//        //            f.Categories.Add(fc);
//        //        }

//        //        try
//        //        {
//        //            errorDetails = string.Format("Currently saving: {0}", f.ToString());
//        //            Create(f);
//        //            Save();

//        //        }
//        //        catch (AliKuli.Exceptions.MiscNS.NoDuplicateException)
//        //        {

//        //            continue;
//        //        }
//        //        catch(Exception e)
//        //        {
//        //            string errorMsg = errorDetails + AliKuli.Utilities.ExceptionNS.ErrorMsgClass.GetInnerException(e);
//        //            throw new Exception(errorMsg);

//        //        }
//        //    }
//        //}

//        /// <summary>
//        /// Gets the users
//        /// </summary>
//        /// <returns></returns>


//        public FileDoc CreateFiles(
//      string fileName,
//      int fileNumber,// 
//      ApplicationUser user,
//      int oldFileId,
//      string oldFileNo,
//      int oldCatId,
//      int oldParentId)
//        {
//            //Check_Parameters(fileName, categoryName, user, fileNumber);


//            var fileDoc = Create_File(fileName, fileNumber, oldParentId, user);

//            //Load the file with old access values.

//            if (!oldFileNo.IsNullOrEmpty())
//                fileDoc.OldFileNumber = oldFileNo;

//            if (oldFileId != 0)
//                fileDoc.OldId = oldFileId;

//            if (oldCatId != 0)
//                fileDoc.OldCategoryId = oldCatId;

//            if (oldParentId != 0)
//                fileDoc.OldParentId = oldParentId;


//            //find the category from its old Id.
//            if (oldCatId != 0)
//            {
//                FileCategory fc = new FileCategoryDAL(_db, _user).SearchFor(x => x.OldId == oldCatId)
//                    .FirstOrDefault();

//                if (!fc.IsNull())
//                    fileDoc.Categories.Add(fc);
//            }


//            Create(fileDoc);
//            return fileDoc;

//        }
//        private static void CreateCategories(List<FilesDocImportVM> dataArray, ApplicationUser admin)
//        {
//            FileCategoryDAL fileCatDAL = new FileCategoryDAL(_db, _user);

//            foreach (var row in dataArray)
//            {
//                FileCategory fc = fileCatDAL.Factory();

//                fc.OldId = row.CategoryId;
//                fc.Name = row.CategoryName;


//                try
//                {
//                    fileCatDAL.Create(fc);
//                    fileCatDAL.Save();

//                }
//                catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
//                {

//                    continue;
//                }
//            }
//        }
//        #endregion

//        #region AutoCreate Helpers

//        private void AddParent(FileDoc fileDoc)
//        {
//            if (fileDoc.OldParentId != 0)
//            {
//                FileDoc parent = SearchFor(x => x.OldFileNumber == fileDoc.OldParentId.ToString()).FirstOrDefault();
//                if (parent.IsNull())
//                    throw new Exception(string.Format("Parent not found for OldParentId: {0}, File: {1}", fileDoc.OldParentId, fileDoc.ToString()));

//                fileDoc.Parent = parent;
//                fileDoc.ParentId = parent.Id;
//            }
//        }

//        private static FileCategory Create_Category(string categoryName, ApplicationUser user, FileCategoryDAL fcDAL)
//        {
//            //first make sure the category does not exist
//            var categoryFound = fcDAL.FindForNameAll(categoryName)
//                .Where(y => y.UserId == user.Id)
//                .FirstOrDefault();

//            if (categoryFound.IsNull())
//            {
//                categoryFound = fcDAL.Factory();
//                categoryFound.Name = categoryName;
//                fcDAL.Create(categoryFound);
//            }
//            return categoryFound;
//        }

//        private FileDoc Create_File(string fileName, int fileNumber, int oldParentId, ApplicationUser user)
//        {
//            //first check to see if the file already exists for User
//            var fileFound = SearchFor(x => x.OldFileNumber == fileNumber.ToString())
//                .Where(x => x.UserId == user.Id && x.OldParentId == oldParentId)
//                .FirstOrDefault();

//            //if file is found, no need to create one
//            if (!fileFound.IsNull())
//            {
//                throw new Exception(string.Format("File is already there! File: {0}. FildsDocDAL.Create_File", fileFound.ToString()));

//            }

//            var f = Factory();
//            f.Name = fileName;
//            f.FileNumber = fileNumber;
//            fileFound = f;

//            return fileFound;
//        }

//        private static void Check_Parameters(string fileName, string categoryName, ApplicationUser user, int fileNumber)
//        {
//            if (fileName.IsNullOrEmpty())
//                throw new Exception("File name is empty. FileDocDAL.AutoCreate.");

//            if (categoryName.IsNullOrEmpty())
//                throw new Exception("Category name is empty. FileDocDAL.AutoCreate.");

//            if (user.IsNull())
//                throw new Exception("User is empty. FileDocDAL.AutoCreate.");

//            if (fileNumber < 0)
//                throw new Exception("Filenumber is negative. FileDocDAL.AutoCreate.");

//        }

//        #endregion

//        #region Factory
//        public override FileDoc Factory()
//        {
//            if (_user.IsNull())
//                throw new Exception("There is no user. Not allowed. FileDAL.Factory");

//            var f = base.Factory();
//            UserDAL userDAL = new UserDAL(_db, _user);

//            f.User = _user;
//            return f;

//        }

//        #endregion


//        public override IQueryable<FileDoc> FindAllLight(bool deleted = false)
//        {
//            var fileDocs = base.FindAllLight(deleted);
//            //find all for user
//            //get current userID
//            var userId = new UserDAL(_db, _user).GetCurrentUserId();

//            fileDocs = fileDocs.Where(x => x.UserId == userId);

//            return fileDocs;

//        }
//        #region Error Check
//        public override void ErrorCheck(FileDoc entity)
//        {
//            IsDuplicateNameAllowed = true;
//            base.ErrorCheck(entity);
//            ErrorCheck_OnlyUniqueFilenumber(entity);
//            ErrorCheck_IsUserEmpty(entity);


//        }

//        private void ErrorCheck_IsUserEmpty(FileDoc entity)
//        {
//            if (entity.User.IsNull())
//                throw new Exception(string.Format("User is empty for: {0}. FilesDocDAL.ErrorCheck_IsUserEmpty", entity.ToString()));

//            if (entity.UserId.IsNullOrEmpty())
//                throw new Exception(string.Format("UserID is empty for: {0}. FilesDocDAL.ErrorCheck_IsUserEmpty", entity.ToString()));

//        }

//        private void ErrorCheck_OnlyUniqueFilenumber(FileDoc entity)
//        {
//            //look for file number

//            if (isCreating)
//            {
//                var file = SearchFor(x => x.FileNumber == entity.FileNumber)
//                    .Where(x => x.User.UserName.ToLower() == _user.UserName.ToLower() && x.ParentId == entity.ParentId).FirstOrDefault();

//                bool fileNumberFound = file != null;

//                if (fileNumberFound)
//                    throw new Exception(string.Format("This file number already exists for record: '{0}'", entity.ToString()));
//            }
//        }

//        #endregion



//    }

//}
