﻿using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelClassLibrary.MigraDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelsClassLibrary.ViewModels
{
    /// <summary>
    /// The indexListVM holds just the list of Ids and Names.
    /// </summary>
    public class IndexListVM
    {

        //public IndexListVM(string userName, string nameInput1, string nameInput2, string nameInput3)
        //    : this()
        //{
        //    UserName = userName;

        //    //SmallHeading = smallHeading ?? "";
        //    //MainHeading = bighHeading ?? "";
        //    NameInput1 = nameInput1 ?? "";
        //    NameInput2 = nameInput2;
        //    NameInput3 = nameInput3;
        //    SelectedId = "";

        //}

        public IndexListVM(SortOrderENUM sortOrderEnum, string searchFor, string selectedId, ICommonWithId dudEntity, string webCompanyName, string logoaddress, string userName)
        {
            SortOrderEnum = sortOrderEnum;
            SearchFor = searchFor;
            DudEntity = dudEntity;
            Data = new List<IndexItemVM>();
            Heading = new Headings();
            //this creates the incoming description. Otherwise it was only being created everytime we 
            //sorted... that is still happening.
            Menu = new MenuModel();
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
            UserName = UserName;

        }

        public IndexListVM(ControllerIndexParams p)
            : this(p.SortBy, p.SearchFor, p.SelectedId, p.DudEntity, "", p.LogoAddress, p.UserName)
        {
            //note... if you change the parameters for this... remember to change the parameters for Load below.
        }

        public void Load(ControllerIndexParams parameters)
        {
            SortOrderEnum = parameters.SortBy;
            SearchFor = parameters.SearchFor;
            SelectedId = parameters.SelectedId;
            DudEntity = parameters.DudEntity;
            UserName = parameters.UserName;

            if (!parameters.LogoAddress.IsNullOrWhiteSpace())
                Logo = new Logo(parameters.LogoAddress);
            else
                Logo = new Logo();

        }
        //public IndexListVM(ICommonWithId dudEntity)
        //    : this(SortOrderENUM.Item1_Asc, "", "", dudEntity,"", null)
        //{
        //}

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

        public string NoOfRecords
        {
            get
            {
                long noOfRecs = 0;

                if (!DataSortedAndFiltered.IsNullOrEmpty())
                    noOfRecs = DataSortedAndFiltered.Count;

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
        public string UserName { get; set; }
        public string WebCompanyName { get; set; }

        #region Sorting...

        /// <summary>
        /// This is data filtered by SearchFor and sorted...
        /// </summary>
        public List<IndexItemVM> DataSortedAndFiltered
        {
            get
            {
                if (SearchFor.IsNullOrWhiteSpace())
                    return SortedData();

                List<IndexItemVM> filteredData = SortedData().ToList().Where(x => x.FullName.ToLower().Contains(SearchFor.ToLower())).ToList();
                return filteredData;

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

        private List<IndexItemVM> Sorted_Ascending_Input1()
        {
            if (!Data.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput1 + " ASCENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return Data.OrderBy(x => x.Input1SortString).ToList();
            }
            return null;
        }

        private List<IndexItemVM> Sorted_Ascending_Input2()
        {
            if (!Data.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput2 + " ASCENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return Data.OrderBy(x => x.Input2SortString).ToList();
            }
            return null;
        }

        private List<IndexItemVM> Sorted_Ascending_Input3()
        {
            if (!Data.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput3 + " ASCENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return Data.OrderBy(x => x.Input3SortString).ToList();
            }
            return null;
        }




        private List<IndexItemVM> Sorted_Decending_Input1()
        {
            if (!Data.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput1 + " DECENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return Data.OrderByDescending(x => x.Input1SortString).ToList();
            }
            return null;
        }

        private List<IndexItemVM> Sorted_Decending_Input2()
        {
            if (!Data.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput2 + " DECENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return Data.OrderByDescending(x => x.Input2SortString).ToList();

            }

            return null;
        }

        private List<IndexItemVM> Sorted_Decending_Input3()
        {
            if (!Data.IsNullOrEmpty())
            {
                //Heading.SortOrderDescription = "Sorted by " + NameInput3 + " DECENDING";
                CreateSortOrderDescription(SortOrderEnum);
                return Data.OrderByDescending(x => x.Input3SortString).ToList();

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
        public List<IndexItemVM> SortedData()
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

        public MenuModel Menu { get; set; }

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




    }
}
