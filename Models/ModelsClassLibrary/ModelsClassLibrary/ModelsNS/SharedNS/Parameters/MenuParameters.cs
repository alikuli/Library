
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class MenuParameters
    {
        public MenuParameters()
        {
            MenuLevel = MenuLevelENUM.unknown;
        }
        public MenuParameters(MenuLevelENUM menuLevelEnum, string menuPathMainId, string productId, string productChildId)
        {
            MenuLevel = menuLevelEnum;
            MenuPathMainId = menuPathMainId;
            ProductChildId = productChildId;
            ProductId = productId;
        }

        public MenuLevelENUM MenuLevel { get; set; }
        public string MenuPathMainId { get; set; }
        public string ProductId { get; set; }
        public string ProductChildId { get; set; }


        //public string MenuPath1Id { get; set; }
        //public string MenuPath2Id { get; set; }
        //public string MenuPath3Id { get; set; }

    }
}
