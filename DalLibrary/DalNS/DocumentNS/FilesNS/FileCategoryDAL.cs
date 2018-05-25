using System;
using System.Collections.Generic;
using System.Linq;
using AliKuli.Extentions;

using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS;

using UserModels.Models;


namespace DalLibrary.DalNS
{
    public class FileCategoryDAL : Repositry<FileCategory>
    {

        public FileCategoryDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass("FileCategoryDAL");

        }



        public override FileCategory Factory()
        {
            var fc = base.Factory();

            Fix_User(fc);
            return fc;
        }
        public FileCategory AutoCreate(string categoryName)
        {
            var fc = Factory();
            fc.Name = categoryName;
            return fc;
        }


        #region Fix
        public override void Fix(FileCategory entity)
        {
            base.Fix(entity);
            Fix_User(entity);
        }
        private void Fix_User(FileCategory entity)
        {
            if (_user.IsNull())
                throw new Exception("No User. You must be logged in to continue. FileCategoryDAL.Fix_User");


            entity.User = (User)_user;
            entity.UserId = _user.Id;

        }

        #endregion



        #region ErrorCheck Helpers
        public override void ErrorCheck(FileCategory entity)
        {
            base.ErrorCheck(entity);
            ErrorCheck_User(entity);
        }

        private void ErrorCheck_User(FileCategory entity)
        {
            if (entity.User.IsNull())
                throw new Exception("No User found. FileCategoryDAL.ErrorCheck_User");

            if (entity.UserId.IsNullOrEmpty())
                throw new Exception("No UserId. FileCategoryDAL.ErrorCheck_User");
        }

        #endregion


        #region Helpers

        public List<FileDocsPrintingVM> MakeListOfFileCategoriesWithFiles(string search)
        {

            List<FileDocsPrintingVM> listOfCategories = SelectCategoriesToAdd(search).ToList();

            if (listOfCategories.IsNullOrEmpty())
            {
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("No File Categories Exist");
            }

            return listOfCategories;
        }


        /// <summary>
        /// This selects the categories to add
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private IList<FileDocsPrintingVM> SelectCategoriesToAdd(string search)
        {
            var lstCategories = FindAll();
            List<FileDocsPrintingVM> lstFileDocPrintingVM = new List<FileDocsPrintingVM>();

            if (lstCategories.IsNullOrEmpty())
                return lstFileDocPrintingVM;

            //if (search.IsNullOrEmpty())
            //    return lstFileDocPrintingVM;

            foreach (var category in lstCategories)
            {
                if (!search.IsNullOrEmpty())
                {
                    if (category.Name.ToLower().Contains(search.ToLower()))
                    {
                        AddCategoryWithFiles(lstFileDocPrintingVM, category);
                    }
                    else //any of the files contains the search, add the category
                    {
                        AddCategoryWithSelectedFiles(lstFileDocPrintingVM, category, search);

                    }
                }
                else//just add the category
                {
                    AddCategoryWithFiles(lstFileDocPrintingVM, category);
                }

            }
            return lstFileDocPrintingVM;

        }


        /// <summary>
        /// This adds the category but only those files which match the search
        /// </summary>
        /// <param name="lstFileDocPrintingVM"></param>
        /// <param name="category"></param>
        /// <param name="search"></param>
        private void AddCategoryWithSelectedFiles(List<FileDocsPrintingVM> lstFileDocPrintingVM, FileCategory category, string search)
        {

            var listOfFilesInCatWithSearchString = category.Files
                .Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()))
                .ToList();

            if (listOfFilesInCatWithSearchString.IsNullOrEmpty())
                return;


            int noOfFiles = listOfFilesInCatWithSearchString.Count();
            FileDocsPrintingVM fdpVM = new FileDocsPrintingVM();

            fdpVM.CategoryName = MakeCategoryName(category.Name, noOfFiles);

            foreach (var file in listOfFilesInCatWithSearchString)
            {
                FilesDetail fileDetail = new FilesDetail(file.FileNumber.ToString(), file.Name);
                fdpVM.Files.Add(fileDetail);
            }

            lstFileDocPrintingVM.Add(fdpVM);
        }

        private static void AddCategoryWithFiles(List<FileDocsPrintingVM> lstFileDocPrintingVM, FileCategory category)
        {
            int noOfFiles = category.Files.Count();
            FileDocsPrintingVM fdpVM = new FileDocsPrintingVM();

            fdpVM.CategoryName = MakeCategoryName(category.Name, noOfFiles);

            foreach (var file in category.Files.ToList())
            {
                FilesDetail fileDetail = new FilesDetail(file.FileNumber.ToString(), file.Name);
                fdpVM.Files.Add(fileDetail);
            }

            lstFileDocPrintingVM.Add(fdpVM);
        }


        #endregion

        #region MakeListOfFileCategoriesWithFiles Helpers

        ///// <summary>
        ///// This creates the print out for the files where the category is shown as a parent.
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="listOfCategories"></param>
        ///// <param name="lstFileDocsPrintingVM"></param>
        ///// <param name="allowEmpty"></param>
        //private static void LoadCategoriesAndFiles(string search, List<FileCategory> listOfCategories, List<FileDocsPrintingVM> lstFileDocsPrintingVM, bool allowEmpty = true)
        //{
        //    foreach (var category in listOfCategories)
        //    {
        //        FileDocsPrintingVM fdVM = new FileDocsPrintingVM();
        //        fdVM.Files = new List<FilesDetail>();

        //        var listOfFiles = category.Files.ToList();
        //        int noOfFiles = listOfFiles.Count();
        //        string categroyName = category.Name;

        //        fdVM.CategoryName = MakeCategoryName(categroyName, noOfFiles);

        //        if (!listOfFiles.IsNullOrEmpty())
        //        {
        //            listOfFiles = AddListOfFilesToCategory(search, fdVM, listOfFiles);
        //        }
        //        else
        //        {
        //            if (!allowEmpty)
        //                continue;
        //        }

        //        lstFileDocsPrintingVM.Add(fdVM);
        //    }
        //}

        /// <summary>
        /// This makes the category name for LoadingCategoriesAndFiles
        /// </summary>
        /// <param name="catNameRaw">This is the simple category name</param>
        /// <param name="noOfFiles">This is the number of files</param>
        /// <returns></returns>
        private static string MakeCategoryName(string catNameRaw, int noOfFiles)
        {
            string showingFileOrFiles = noOfFiles == 1 ? "File" : "Files";

            return string.Format("{0} ({1} {2})",
                catNameRaw,
                noOfFiles,
                showingFileOrFiles);

        }

        ///// <summary>
        ///// This adds list of files to category
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="fdVM"></param>
        ///// <param name="listOfFiles"></param>
        ///// <returns></returns>
        //private static List<FileDoc> AddListOfFilesToCategory(string search, FileDocsPrintingVM fdVM, List<FileDoc> listOfFiles)
        //{
        //    //if search is not empty, then filter the
        //    //files
        //    //if (!search.IsNullOrEmpty())
        //    //    listOfFiles = listOfFiles
        //    //        .Where(x =>
        //    //            x.NameDecrypted
        //    //            .ToLower()
        //    //            .Contains(search.ToLower()))
        //    //        .ToList();

        //    //Add the files
        //    foreach (var f in listOfFiles)
        //    {
        //        FilesDetail fd = new FilesDetail();
        //        fd.FileName = f.Name;
        //        fd.FileNumber = f.FileNumberComplete();

        //        fdVM.Files.Add(fd);
        //    };

        //    var filesOrdered = fdVM.Files.OrderBy(x => x.FileName);
        //    fdVM.Files = filesOrdered.ToList();
        //    return listOfFiles;
        //}

        ////this is a helper which adds the categories
        //private static List<FileDocsPrintingVM> SelectDataForSearch(string search, List<FileDocsPrintingVM> lstFileDocsPrintingVM)
        //{
        //    if (!search.IsNullOrEmpty())
        //    {
        //        if (!lstFileDocsPrintingVM.IsNullOrEmpty())
        //        {
        //            foreach (var cat in lstFileDocsPrintingVM)
        //            {
        //                if (cat.Files.Count() > 0)
        //                {
        //                    cat.Selected = true;
        //                    continue;
        //                }

        //                if (cat.CategoryName.ToLower().Contains(search))
        //                {
        //                    cat.Selected = true;
        //                }
        //            }

        //            //Only select the selected categories
        //            lstFileDocsPrintingVM = lstFileDocsPrintingVM.Where(x => x.Selected).ToList();
        //        }


        //    }
        //    return lstFileDocsPrintingVM;
        //}

        #endregion


    }



}
