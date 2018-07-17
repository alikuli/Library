using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;

namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS
{
    public abstract class MenuStateAbstract
    {
        /// <summary>
        /// This is the Menu State.
        /// </summary>
        public MenuENUM MenuEnum { get; set; }

        /// <summary>
        /// This is the value of MenuEnum so that you go to the correct state when button is pressed.
        /// </summary>
        public MenuENUM MenuEnumValueForCreateButton { get; set; }

        /// <summary>
        /// This is the value Menu
        /// </summary>
        /// <summary>
        /// This is the value of MenuEnum so that you go to the correct state when button is pressed.
        /// </summary>
        public MenuENUM MenuEnumValueForEditButtom { get; set; }

        /// <summary>
        /// This is the current MenuPath that has been selected. This is fuzzy.
        /// </summary>
        public MenuPathMain MenuPathMain { get; set; }

        /// <summary>
        /// This is the product value when selected
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// This is the productChild
        /// </summary>
        public ProductChild ProductChild { get; set; }


        /// <summary>
        /// This is the name of the controller that is the parent of the current view.
        /// </summary>
        public string ControllerCurrentName { get; set; }



        public string CurrentUrl { get; set; }

        public string SelectedId { get; set; }
        public string SearchString { get; set; }
        public SortOrderENUM SortOrderEnum { get; set; }


        public string GetProductVM()
        {
            throw new NotImplementedException();
        }

        public abstract string MenuPath1Id { get; }
        public abstract string MenuPath2Id { get; }
        public abstract string MenuPath3Id { get; }

        public abstract string EditLink_Name { get; }
        public abstract string EditLink_Id { get; }
        public abstract string EditLink_MenuEnum { get; }


        public abstract string CreateLink_Name { get; }
        public abstract string CreateLink_Id { get; }
        public abstract string CreateLink_MenuEnum { get; }


        public abstract string BackLink_Name { get; }
        public abstract string BackLink_Id { get; }
        public abstract string BackLink_MenuEnum { get; }


        public abstract bool ShowCreateButton { get; }
        public abstract bool ShowEditButton { get; }



    }
}
