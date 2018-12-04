using ModelsClassLibrary.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.ProductNS.CheckBoxItemNS
{
    /// <summary>
    /// this class is so we can hide data in the view which I imagine will get very long.
    /// I am planning for it to be displayed as collapsable lists.
    /// </summary>
    public class CheckBoxListTree : CheckBoxItem
    {


        public CheckBoxListTree()
        {

            Mp2List = new List<CheckBoxListTree>();
            CheckedBoxesList = new List<CheckBoxItem>();
        }

        public string Name { get; set; }

        /// <summary>
        /// When checkboxList is MP1, then MPList is MP2 List
        /// When Item is in MP2 List then, the MP2List is Empty, and the CheckBoxItem List contains data
        /// </summary>
        public List<CheckBoxListTree> Mp2List { get; set; }
        public List<CheckBoxItem> CheckedBoxesList { get; set; }

        public bool IsActve { get; set; }
    }
}
