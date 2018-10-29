using AliKuli.Extentions;
using AliKuli.Tools;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelClassLibrary.MigraDocNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using ModelsClassLibrary.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UowLibrary.MenuNS.MenuStateNS;
namespace ModelsClassLibrary.ViewModels
{
    /// <summary>
    /// The indexListVM holds just the list of Ids and Names.
    /// </summary>
    public class IndexListVM : IHaveMenuManager
    {

        string[] _listOfStopWords;
        string[] _searchWords;


        public IndexListVM(ControllerIndexParams p)
        {
            initialize(p.Id, p.SortBy, p.SearchFor, p.SelectedId, p.DudEntity, "", p.LogoAddress, p.UserId, p.UserName, p.IsAndForSearch, p.BreadCrumbManager, p.ActionNameEnum, p.LikeUnlikeCounter, p.IsMenu);

        }

        private void initialize(
            string id,
            SortOrderENUM sortOrderEnum,
            string searchFor,
            string selectedId,
            ICommonWithId dudEntity,
            string webCompanyName,
            string logoaddress,
            string userId,
            string userName,
            bool isAndForSearch,
            BreadCrumbManager breadCrumbManager,
            ActionNameENUM actionNameEnum,
            LikeUnlikeParameter likeUnlikesCounter,
            bool isMenu)
        {
            IsMenu = isMenu;
            SortOrderEnum = sortOrderEnum;
            Id = id;
            SearchFor = searchFor;
            IsAndForSearch = isAndForSearch;

            DudEntity = dudEntity;
            Data = new List<IndexItemVM>();
            Heading = new Headings();
            //this creates the incoming description. Otherwise it was only being created everytime we 
            //sorted... that is still happening.

            //Menu = new MenuModel();
            //Menu.ReturnUrl = returnUrl;
            MenuManager = new MenuManager(null, null, null, MenuENUM.IndexDefault, breadCrumbManager, likeUnlikesCounter, UserId);
            Show = new Show();
            SelectedId = selectedId;
            WebCompanyName = webCompanyName;

            //Heading.SortOrderDescription = getSortDescription();
            getMainHeadingAndSortNamesFromEntity();
            CreateSortOrderDescription(SortOrderEnum);

            string title = string.Format("{0} List.", Heading.RecordName);
            string subject = string.Format("{0}.", "Pdf Format");
            string author = string.Format("{0}.", "Ali Kuli");


            PdfHeaderInfo = new ModelClassLibrary.MigraDocNS.PdfHeaderInfo(title, subject, author);

            //this points to the logo
            Logo = new Logo(logoaddress);
            //User = user;
            //UserIsAdmin = userIsAdmin;

            UserId = userId;
            UserName = userName;

            //setup.
            _listOfStopWords = StringTools.GetStopWords();
            _searchWords = getSearchWords();

            //MenuManager = new MenuManager(id, null, null, null, MenuLevelENUM.unknown, returnUrl, false, "", "", "", sortOrderEnum,actionNameEnum);
            //MenuManager.ReturnUrl = returnUrl;
        }

        public void Load(ControllerIndexParams p)
        {
            string webCompany = "";
            initialize(p.Id, p.SortBy, p.SearchFor, p.SelectedId, p.DudEntity, webCompany, p.LogoAddress, p.UserId, p.UserName, p.IsAndForSearch, p.BreadCrumbManager, p.ActionNameEnum, p.LikeUnlikeCounter, p.IsMenu);

        }

        /// <summary>
        /// Note. This Id is dangerous. 
        /// This Id has various values.
        /// When MenuLevel is 1 to 3 - This is MenuPath1Id
        /// When MenuLevel is 4 -This is a productId 
        /// When MenuLevel is 5 -This is a productChildId 
        /// Always access this through the Menu
        /// </summary>
        protected string Id { get; set; }

        public bool IsMenu { get; set; }

        public Logo Logo { get; set; }
        public PdfHeaderInfo PdfHeaderInfo { get; set; }
        private ICommonWithId DudEntity { get; set; }
        public string DownloadFileName
        {
            get
            {

                StringBuilder sb = new StringBuilder();

                if (!UserName.IsNullOrWhiteSpace())
                {
                    sb.Append(UserName.ToUpper());
                }

                if (Heading.RecordName.IsNullOrWhiteSpace())
                {
                    throw new Exception("Heading Record Name is empty. Programming error.");
                }

                sb.Append(" " + Heading.RecordName);

                if (!SearchFor.IsNullOrWhiteSpace())
                {
                    sb.Append(" Filtered By " + SearchFor);
                }

                sb.Append(" " + Heading.SortOrderDescription);
                sb.Append(" " + DateTime.Now.Ticks.ToString());

                string downloadFileName = sb.ToString().Trim().ConvertSpacesToUnderScores();


                return downloadFileName;

            }
        }
        public Show Show { get; set; }

        //public bool ShowEditDeleteAndCreate
        //{
        //    set
        //    {
        //        bool isTrue = value;

        //        if (isTrue)
        //        {
        //            ShowEdit = true;
        //            ShowDelete = true;
        //            ShowCreate = true;
        //        }
        //    }
        //}

        //public void ShowMoveUpMoveDown(bool trueFalse)
        //{
        //    ShowMoveUp = trueFalse;
        //    ShowMoveDown = trueFalse;
        //    ShowMoveTop = trueFalse;
        //    ShowMoveBottom = trueFalse;
        //}

        //#region Show ...
        ////public bool ShowMoveTop { get; set; }
        ////public bool ShowMoveBottom { get; set; }
        ////public bool ShowMoveUp { get; set; }
        ////public bool ShowMoveDown { get; set; }
        /////// <summary>
        /////// If true, then Edit shows
        /////// </summary>
        ////public bool ShowEdit { get; set; }

        /////// <summary>
        /////// If true then Delete shows. Remember this is also controlled by the
        /////// AutoCreated in the CommonWithId. Both have to be true to allow delete.
        /////// </summary>
        ////public bool ShowDelete { get; set; }

        ///// <summary>
        ///// if true then create shows
        /////// </summary>
        ////public bool ShowCreate { get; set; }

        ////public bool HideImageInList { get; set; }

        //#endregion
        public string SearchFor { get; set; }

        private string[] listOfStopWords
        {
            get
            {
                return _listOfStopWords;
            }
        }
        public bool IsAndForSearch { get; set; }
        private string[] searchWords
        {
            get
            {

                return _searchWords;
            }
        }

        /// <summary>
        /// This makes an array of search words.
        /// </summary>
        /// <returns></returns>
        private string[] getSearchWords()
        {
            if (SearchFor.IsNullOrWhiteSpace())
                return null;



            string[] lstOfWordsEntered = SearchFor.Split(' ');


            if (lstOfWordsEntered.IsNullOrEmpty())
                return null;

            //Now exclude stop-Words.
            List<string> lstOfSearchWords = new List<string>();

            foreach (var word in lstOfWordsEntered)
            {
                if (listOfStopWords.Contains(word.ToLower()))
                    continue;
                lstOfSearchWords.Add(word);
            }

            return lstOfSearchWords.ToArray();

        }
        public string NoOfRecords
        {
            get
            {
                long noOfRecs = 0;

                if (!DataSortedAndFiltered.IsNullOrEmpty())
                    noOfRecs = DataSortedAndFiltered.Count();

                return string.Format("{0} {1}", noOfRecs.ToString("N00"), noOfRecs == 1 ? Heading.RecordName : Heading.RecordNamePlural);
            }
        }

        /// <summary>
        /// If this is true, then a tiled Index is printed in the view.
        /// </summary>
        public bool IsImageTiled { get; set; }

        #region Methods/Properties for Selected Id

        public string SelectedId { get; set; }

        private int GetIndexOfSelectedId()
        {
            if (DataSortedAndFiltered.IsNullOrEmpty())
                return -1;

            if (SelectedIdStr.IsNullOrWhiteSpace())
                return -1;

            var lstArray = DataSortedAndFiltered.ToArray();

            var indexItemVM = lstArray.FirstOrDefault(x => x.Id == SelectedIdStr);

            if (indexItemVM.IsNull())
                return -1;

            int indexOf_indexItemVM = Array.IndexOf(lstArray, indexItemVM);
            return indexOf_indexItemVM;
        }


        private string getIdOfItemAtIndexNo(int indexNo)
        {
            if (indexNo == -1)
                return string.Empty;

            if (DataSortedAndFiltered.IsNullOrEmpty())
                return string.Empty;

            var lstArray = DataSortedAndFiltered.ToArray();

            if (indexNo > lstArray.GetUpperBound(0))
                return string.Empty;

            string idstr = lstArray[indexNo].Id;

            return idstr;

        }

        public string FirstItemId
        {
            get
            {
                if (DataSortedAndFiltered.IsNullOrEmpty())
                    return string.Empty;

                var idFirstItem = getIdOfItemAtIndexNo(0);
                return idFirstItem;
            }
        }

        public string LastItemId
        {
            get
            {
                if (DataSortedAndFiltered.IsNullOrEmpty())
                    return string.Empty;
                int lengthOfData = DataSortedAndFiltered.Count();

                if (lengthOfData == 0)
                    return string.Empty;

                var idLastItem = getIdOfItemAtIndexNo(lengthOfData - 1);
                return idLastItem;
            }
        }

        public string SelectedID_PreviousItem
        {
            get
            {
                if (SelectedId.IsNullOrEmpty())
                    return string.Empty;

                var currIndex = GetIndexOfSelectedId();
                var idBeforeCurrIndex = getIdOfItemAtIndexNo(currIndex - 1);
                return idBeforeCurrIndex;
            }
        }

        public string SelectedId_NextItem
        {
            get
            {
                if (SelectedId.IsNullOrEmpty())
                    return string.Empty;

                var currIndex = GetIndexOfSelectedId();
                var idAfterCurrIndex = getIdOfItemAtIndexNo(currIndex + 1);
                return idAfterCurrIndex;
            }
        }


        public bool SelectedIdMatchesThis(string id)
        {
            bool match = SelectedId == id;
            return match;
        }

        public string SelectedIdStr { get { return SelectedId; } }


        #endregion

        public Headings Heading { get; set; }
        public string WebCompanyName { get; set; }

        public string GlobalComment { get; set; }

        #region Data Related

        /// <summary>
        /// This is data filtered by SearchFor and sorted...
        /// </summary>
        public IQueryable<IndexItemVM> DataSortedAndFiltered
        {
            get
            {
                return SortedAndFilteredData();
            }
        }
        public IndexItemVM[] DataSortedAndFilteredArray
        {
            get
            {
                return SortedAndFilteredData().ToArray();
            }
        }
        public List<IndexItemVM> DataSortedAndFilteredList
        {
            get
            {
                return SortedAndFilteredData().ToList();
            }
        }

        /// <summary>
        /// Add an IndexItemVM from here.
        /// </summary>
        /// <param name="indexItemVM"></param>
        public void Add(IndexItemVM indexItemVM)
        {
            if (!indexItemVM.IsNull())
                Data.Add(indexItemVM);
        }


        /// <summary>
        /// This is where all the data is stored. To add to it, use Add.
        /// </summary>
        private List<IndexItemVM> Data { get; set; }

        private IQueryable<IndexItemVM> DataFiltered
        {
            get
            {
                if (Data.IsNullOrEmpty())
                    return null;

                if (searchWords.IsNullOrEmpty())
                    return Data.AsQueryable();

                var dataFiltered = Data.AsQueryable();

                if (IsAndForSearch)
                    dataFiltered = doAndSearch(dataFiltered);
                else
                    dataFiltered = doOrSearch(dataFiltered);

                return dataFiltered;
            }
        }

        private IQueryable<IndexItemVM> doOrSearch(IQueryable<IndexItemVM> dataFiltered)
        {
            //does up to 6 ors
            switch (searchWords.Count())
            {
                case 1: dataFiltered = dataFiltered.Where(x =>
                    x.Name.ToLower().Contains(searchWords[0].ToLower()));
                    break;
                case 2: dataFiltered = dataFiltered.Where(x =>
                    x.Name.ToLower().Contains(searchWords[0].ToLower()) ||
                    x.Name.ToLower().Contains(searchWords[1].ToLower()));
                    break;

                case 3: dataFiltered = dataFiltered.Where(x =>
                    x.Name.ToLower().Contains(searchWords[0].ToLower()) ||
                    x.Name.ToLower().Contains(searchWords[1].ToLower()) ||
                    x.Name.ToLower().Contains(searchWords[2].ToLower()));
                    break;

                case 4: dataFiltered = dataFiltered.Where(x =>
                    x.Name.ToLower().Contains(searchWords[0].ToLower()) ||
                    x.Name.ToLower().Contains(searchWords[1].ToLower()) ||
                    x.Name.ToLower().Contains(searchWords[2].ToLower()) ||
                    x.Name.ToLower().Contains(searchWords[3].ToLower()));
                    break;

                case 5: dataFiltered = dataFiltered.Where(x =>
                    x.Name.ToLower().Contains(searchWords[0].ToLower()) ||
                    x.Name.ToLower().Contains(searchWords[1].ToLower()) ||
                    x.Name.ToLower().Contains(searchWords[2].ToLower()) ||
                    x.Name.ToLower().Contains(searchWords[3].ToLower()) ||
                    x.Name.ToLower().Contains(searchWords[4].ToLower()));
                    break;

                default:
                    dataFiltered = dataFiltered.Where(x =>
                        x.Name.ToLower().Contains(searchWords[0].ToLower()) ||
                        x.Name.ToLower().Contains(searchWords[1].ToLower()) ||
                        x.Name.ToLower().Contains(searchWords[2].ToLower()) ||
                        x.Name.ToLower().Contains(searchWords[3].ToLower()) ||
                        x.Name.ToLower().Contains(searchWords[4].ToLower()) ||
                        x.Name.ToLower().Contains(searchWords[5].ToLower()));
                    break;
            }
            return dataFiltered;
        }


        private IQueryable<IndexItemVM> doAndSearch(IQueryable<IndexItemVM> dataFiltered)
        {
            //does up to 6 ors
            switch (searchWords.Count())
            {

                case 1: dataFiltered = dataFiltered.Where(x =>
                    x.Name.ToLower().Contains(searchWords[0].ToLower()));
                    break;
                case 2: dataFiltered = dataFiltered.Where(x =>
                    x.Name.ToLower().Contains(searchWords[0].ToLower()) &&
                    x.Name.ToLower().Contains(searchWords[1].ToLower()));
                    break;

                case 3: dataFiltered = dataFiltered.Where(x =>
                    x.Name.ToLower().Contains(searchWords[0].ToLower()) &&
                    x.Name.ToLower().Contains(searchWords[1].ToLower()) &&
                    x.Name.ToLower().Contains(searchWords[2].ToLower()));
                    break;

                case 4: dataFiltered = dataFiltered.Where(x =>
                    x.Name.ToLower().Contains(searchWords[0].ToLower()) &&
                    x.Name.ToLower().Contains(searchWords[1].ToLower()) &&
                    x.Name.ToLower().Contains(searchWords[2].ToLower()) &&
                    x.Name.ToLower().Contains(searchWords[3].ToLower()));
                    break;

                case 5: dataFiltered = dataFiltered.Where(x =>
                    x.Name.ToLower().Contains(searchWords[0].ToLower()) &&
                    x.Name.ToLower().Contains(searchWords[1].ToLower()) &&
                    x.Name.ToLower().Contains(searchWords[2].ToLower()) &&
                    x.Name.ToLower().Contains(searchWords[3].ToLower()) &&
                    x.Name.ToLower().Contains(searchWords[4].ToLower()));
                    break;

                default:
                    dataFiltered = dataFiltered.Where(x =>
                        x.Name.ToLower().Contains(searchWords[0].ToLower()) &&
                        x.Name.ToLower().Contains(searchWords[1].ToLower()) &&
                        x.Name.ToLower().Contains(searchWords[2].ToLower()) &&
                        x.Name.ToLower().Contains(searchWords[3].ToLower()) &&
                        x.Name.ToLower().Contains(searchWords[4].ToLower()) &&
                        x.Name.ToLower().Contains(searchWords[5].ToLower()));
                    break;
            }
            return dataFiltered;
        }
        #endregion
        #region Sorting...



        private IQueryable<IndexItemVM> Sorted_Ascending_Input1()
        {
            if (!DataFiltered.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput1 + " ASCENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return DataFiltered.OrderBy(x => x.Input1SortString);
            }
            return null;
        }

        private IQueryable<IndexItemVM> Sorted_Ascending_Input2()
        {
            if (!DataFiltered.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput2 + " ASCENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return DataFiltered.OrderBy(x => x.Input2SortString);
            }
            return null;
        }

        private IQueryable<IndexItemVM> Sorted_Ascending_Input3()
        {
            if (!DataFiltered.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput3 + " ASCENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return DataFiltered.OrderBy(x => x.Input3SortString);
            }
            return null;
        }




        private IQueryable<IndexItemVM> Sorted_Decending_Input1()
        {
            if (!DataFiltered.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput1 + " DECENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return DataFiltered.OrderByDescending(x => x.Input1SortString);
            }
            return null;
        }

        private IQueryable<IndexItemVM> Sorted_Decending_Input2()
        {
            if (!DataFiltered.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput2 + " DECENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return DataFiltered.OrderByDescending(x => x.Input2SortString);

            }

            return null;
        }

        private IQueryable<IndexItemVM> Sorted_Decending_Input3()
        {
            if (!DataFiltered.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput3 + " DECENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return DataFiltered.OrderByDescending(x => x.Input3SortString);

            }

            return null;
        }


        /// <summary>
        /// Controls the output in SortedData. Default value is Item1_Asc
        /// </summary>
        public SortOrderENUM SortOrderEnum { get; set; }

        /// <summary>
        /// This is sorted data. To control the sorting, set the SortOrderEnum property
        /// </summary>
        /// <returns></returns>
        public IQueryable<IndexItemVM> SortedAndFilteredData()
        {
            switch (SortOrderEnum)
            {
                case SortOrderENUM.Item1_Asc:
                    return Sorted_Ascending_Input1();

                case SortOrderENUM.Item2_Asc:
                    return Sorted_Ascending_Input2();

                case SortOrderENUM.Item3_Asc:
                    return Sorted_Ascending_Input3();

                case SortOrderENUM.Item1_Dsc:
                    return Sorted_Decending_Input1();

                case SortOrderENUM.Item2_Dsc:
                    return Sorted_Decending_Input2();

                case SortOrderENUM.Item3_Dsc:
                    return Sorted_Decending_Input3();
                default:
                    return Sorted_Ascending_Input1();
            }

        }

        public void CreateSortOrderDescription(SortOrderENUM sortOrderEnum)
        {
            switch (sortOrderEnum)
            {
                case SortOrderENUM.Item1_Asc:
                    Heading.SortOrderDescription = "Sorted by " + NameInput1 + " Asc";
                    break;

                case SortOrderENUM.Item2_Asc:
                    Heading.SortOrderDescription = "Sorted by " + NameInput2 + " Asc";
                    break;


                case SortOrderENUM.Item3_Asc:
                    Heading.SortOrderDescription = "Sorted by " + NameInput3 + " Asc";
                    break;

                case SortOrderENUM.Item1_Dsc:
                    Heading.SortOrderDescription = "Sorted by " + NameInput1 + " Dec";
                    break;

                case SortOrderENUM.Item2_Dsc:
                    Heading.SortOrderDescription = "Sorted by " + NameInput2 + " Dec";
                    break;

                case SortOrderENUM.Item3_Dsc:
                    Heading.SortOrderDescription = "Sorted by " + NameInput3 + " Dec";
                    break;
                default:
                    Heading.SortOrderDescription = "Sorted by " + NameInput1 + " Asc";
                    break;
            }

        }
        public string SortOrder1_Description_Asc
        {
            get
            {
                return string.Format("By {0} Asc", NameInput1);
            }
        }
        public string SortOrder1_Description_Dsc
        {
            get
            {
                return string.Format("By {0} Dsc", NameInput1);
            }
        }



        public string SortOrder2_Description_Asc
        {
            get
            {
                return string.Format("By {0} Asc", NameInput2);
            }
        }
        public string SortOrder2_Description_Dsc
        {
            get
            {
                return string.Format("By {0} Dsc", NameInput2);
            }
        }


        public string SortOrder3_Description_Asc
        {
            get
            {
                return string.Format("By {0} Asc", NameInput3);
            }
        }
        public string SortOrder3_Description_Dsc
        {
            get
            {
                return string.Format("By {0} Dsc", NameInput3);
            }
        }


        #endregion


        #region Sorting Names


        public string NameInput1 { get; set; }
        public string NameInput2 { get; set; }
        public string NameInput3 { get; set; }

        #endregion


        //private string getSortDescription()
        //{
        //    string theSortName = "Sorted by - ";
        //    switch (SortOrderEnum)
        //    {
        //        case EnumLibrary.EnumNS.SortOrderENUM.Item1_Asc:
        //            theSortName += string.Format("{0} (Asc)", NameInput1);
        //            break;
        //        case EnumLibrary.EnumNS.SortOrderENUM.Item2_Asc:
        //            theSortName += string.Format("{0} (Asc)", NameInput2);
        //            break;
        //        case EnumLibrary.EnumNS.SortOrderENUM.Item3_Asc:
        //            theSortName += string.Format("{0} (Asc)", NameInput3);
        //            break;
        //        case EnumLibrary.EnumNS.SortOrderENUM.Item1_Dsc:
        //            theSortName += string.Format("{0} (Dsc)", NameInput1);
        //            break;
        //        case EnumLibrary.EnumNS.SortOrderENUM.Item2_Dsc:
        //            theSortName += string.Format("{0} (Dsc)", NameInput2);
        //            break;
        //        case EnumLibrary.EnumNS.SortOrderENUM.Item3_Dsc:
        //            theSortName += string.Format("{0} (Dsc)", NameInput3);
        //            break;
        //        default:
        //            theSortName += string.Format("{0} (Dsc)", "Programing Error in getSortDescription in Business Layer Index.");
        //            break;
        //    }
        //    return theSortName;
        //}

        private void getMainHeadingAndSortNamesFromEntity()
        {
            NameInput1 = DudEntity.NameInput1;
            NameInput2 = DudEntity.NameInput2;
            NameInput3 = DudEntity.NameInput3;
        }


        public string UserName { get; set; }
        public string UserId { get; set; }

        //public ApplicationUser User { get; set; }

        //public bool UserIsAdmin { get; set; }


        //public MenuModel Menu { get; set; }
        public IMenuManager MenuManager { get; set; }
    }
}
