
//using EnumLibrary.EnumNS;
//using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
//using ModelsClassLibrary.ModelsNS.ProductChildNS;
//using ModelsClassLibrary.ModelsNS.ProductNS;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ModelsClassLibrary.MenuNS
//{
//    /// <summary>
//    /// This class is a part of all models that serve in the Menu.
//    /// </summary>
//    [NotMapped]
//    public class Menu : IHaveMenuManager
//    {

//        public MenuLevelENUM MenuLevelEnum{get;set;}

//        public MenuPathMain MenuPathMain { get; set; }

//        public Product Product { get; set; }

//        public ProductChild ProductChild { get; set; }

//        public string ReturnUrl { get; set; }

//        public IMenuManager MenuManager { get; set; }
//    }
//}