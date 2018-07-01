
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class MenuParameters
    {
        public MenuParameters()
        {
            MenuLevelEnum = MenuLevelENUM.unknown;
        }
        public MenuParameters(MenuLevelENUM menuLevelEnum, string menuPathMainId, string productId, string productChildId, string returnUrl)
        {
            MenuLevelEnum = menuLevelEnum;
            MenuPathMainId = menuPathMainId;
            ProductChildId = productChildId;
            ProductId = productId;
            ReturnUrl = returnUrl;
        }

        public MenuLevelENUM MenuLevelEnum { get; set; }
        public string MenuPathMainId { get; set; }
        public string ProductId { get; set; }
        public string ProductChildId { get; set; }
        public string ReturnUrl { get; set; }


        //public string MenuPath1Id { get; set; }
        //public string MenuPath2Id { get; set; }
        //public string MenuPath3Id { get; set; }

    }
}
