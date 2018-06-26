using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ViewModels
{
    [NotMapped]
    public class IndexItemVM : IIndexListItems
    {
        private const int MAX_LENGTH_ALLOWED = 100;
        //public IndexItemVM(string id, string name, string input1SortStr, bool isEditLocked, string description)
        //{
        //    Id = id;
        //    Name = name;
        //    Input1SortString = input1SortStr;
        //    IsEditLocked = isEditLocked;
        //    Description = description;
        //    PrintLineNumber = "";
        //    //Menu = new Menu();
        //}

        //public IndexItemVM(string id, string name, string input1SortStr, string input2SortStr, bool isEditLocked, string description)
        //    : this(id, name, input1SortStr, isEditLocked, description)
        //{
        //    Input2SortString = input2SortStr;
        //}

        public IndexItemVM(string id, string name, string input1SortStr, string input2SortStr, string input3SortStr, bool isEditLocked, string description)
        {
            Input3SortString = input3SortStr;
            Input2SortString = input2SortStr;
            Id = id;
            Name = name;
            Input1SortString = input1SortStr;
            IsEditLocked = isEditLocked;
            Description = description;
            PrintLineNumber = "";
            MenuManager = new MenuManager(id);
        }
        public string Id { get; set; }

        public string ImageAddressStr { get; set; }

        public string Description { get; set; }

        #region Sort strings


        public string Input1SortString { get; set; }
        public string Input2SortString { get; set; }
        public string Input3SortString { get; set; }

        #endregion

        /// <summary>
        /// This is used in the descriptions
        /// </summary>
        public string Name { get; set; }
        public string ShortName
        {
            get
            {
                if (Name.IsNullOrWhiteSpace())
                    return "";

                if (Name.Trim().Length > MAX_LENGTH_ALLOWED)
                {
                    return Name.Trim().Substring(0, MAX_LENGTH_ALLOWED) + "...";
                }
                return Name.Trim();
            }
        }
        public string FullName
        {
            get
            {
                if (Name.IsNullOrWhiteSpace())
                    return "";

                return Name.Trim();
            }
        }

        public override string ToString()
        {
            return FullName;
        }

        public MenuManager MenuManager { get; set; }

        #region Bools
        public bool IsImageThere
        {
            get
            {
                return !ImageAddressStr.IsNullOrWhiteSpace();
            }
        }

        public bool IsEditLocked { get; set; }

        public bool AllowDelete { get { return !IsEditLocked; } }
        public bool AllowEdit { get { return !IsEditLocked; } }

        public string PrintLineNumber { get; set; }





        #endregion


    }
}
