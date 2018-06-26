
//using AliKuli.Extentions;
//using EnumLibrary.EnumNS;
//using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
//using ModelsClassLibrary.ModelsNS.ProductChildNS;
//using ModelsClassLibrary.ModelsNS.ProductNS;
//using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
//using System;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ModelsClassLibrary.MenuNS
//{
//    /// <summary>
//    /// This class is a part of all models that serve in the Menu.
//    /// Note. All the Menupath1, MenuPath2, MenuPath3 and their Ids are derived from MenuPathMain.
//    /// The job of the Menu Path is to help in the Views.
//    /// 
//    /// Level 1.
//    ///     Menu Path helps by tracking that it is a menu.
//    ///     The MenusController is the Main Controller here
//    ///     
//    /// Level 2.
//    ///     Menu Path helps by tracking that it is a menu.
//    ///     It tracks a MenuPathMain in which the MenuPath1 is significant.
//    ///     The MenusController is the Main Controller here
//    /// Level 3.
//    ///     Menu Path helps by tracking that it is a menu.
//    ///     It tracks a MenuPathMain in which the MenuPath1 and MenuPath2 is significant.
//    ///     The MenusController is the Main Controller here
//    ///     
//    /// Level 4.
//    ///     Menu Path helps by tracking that it is a menu.
//    ///     It tracks a MenuPathMain in which the MenuPath1 and MenuPath2 and MenPath3 is significant.
//    ///     The MenusController is the Main Controller here
//    ///     
//    /// Level 5.
//    ///     Menu Path helps by tracking that it is a menu.
//    ///     It tracks a MenuPathMain in which the MenuPath1 and MenuPath2 and MenPath3 is significant.
//    ///     The MenusController is the Main Controller here
//    ///     The ProductController VM is for Edit.
//    ///     The ProductController VM is for Create
//    ///     It also tracks the product.
//    ///     For Edit, it tracks the returnUrl
//    ///     
//    ///     
//    /// </summary>
//    [NotMapped]
//    public class MenuManager_Lv1 : IMenuManager
//    {

//        public MenuManager_Lv1(string id)
//        {
//            Id = id;
//            MenuPathMain = new MenuPathMain();
//        }

//        public MenuManager_Lv1(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu)
//            : this(id)
//        {
//            Load(id, menuPathMain, product, productChild, menuLevelEnum, returnUrl, isMenu);
//        }


//        public void Load(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu)
//        {
//            MenuPathMain = menuPathMain;
//            Product = product;
//            ProductChild = productChild;
//            IsMenu = isMenu;
//        }

//        #region MenuPathMain Related
//        /// <summary>
//        /// This is the current MenuPathMain.
//        /// </summary>
//        public MenuPathMain MenuPathMain { get; set; }
//        /// <summary>
//        /// This Id has various values.
//        /// When MenuLevel is 1 to 3 - This is MenuPath1Id
//        /// When MenuLevel is 4 -This is a productId 
//        /// When MenuLevel is 5 -This is a productChildId 
//        /// 
//        /// </summary>
//        protected string Id { get; set; }

//        public MenuPath1 MenuPath1
//        {
//            get
//            {

//                return null;
//            }
//        }

//        public string MenuPathMainId
//        {
//            get
//            {
//                return "";
//            }
//        }

//        public string ProductId
//        {
//            get
//            {
//                return "";
//            }

//        }

//        public string ProductChildId
//        {
//            get
//            {
//                return "";
//            }

//        }

//        public string MenuPath1Id
//        {
//            get
//            {
//                return "";
//            }


//        }

//        public string MenuPath2Id
//        {
//            get
//            {
//                return "";
//            }

//        }


//        public string MenuPath3Id
//        {
//            get
//            {
//                return "";
//            }

//        }


//        public bool IsMenu 
//        { 
//            get 
//            { 
//                return true;
//        } 
//            set; 
//        }


//        public string MenuPath1Name
//        {
//            get
//            {
//                return "";
//            }
//        }

//        public string MenuPath2Name
//        {
//            get
//            {
//                return "";
//            }

//        }

//        public string MenuPath3Name
//        {
//            get
//            {
//                return "";
//            }
//        }


//        public string ControlerNameForProductVms
//        {
//            get
//            {
//                return "";
//            }

//        }


//        #endregion


//        #region Product Related

//        //public string ProductId { get; set; }

//        public Product Product { get; set; }



//        /// <summary>
//        /// This is the product level
//        /// </summary>
//        public string MenuPath4Name
//        {
//            get
//            {
//                return "";
//            }
//        }

//        #endregion


//        #region  ProductChild Related
//        public ProductChild ProductChild { get; set; }
//        /// <summary>
//        /// This is the product child level.
//        /// </summary>
//        public string MenuPath5Name
//        {
//            get
//            {
//                return "";
//            }
//        }

//        #endregion




//    }
//}