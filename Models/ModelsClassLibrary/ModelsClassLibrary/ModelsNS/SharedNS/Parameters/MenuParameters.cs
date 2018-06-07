
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class MenuParameters
    {
        public MenuParameters()
        {
            MenuLevel = MenuLevelENUM.unknown;
        }
        public MenuParameters(MenuLevelENUM menuLevelEnum, string productCat1Id, string productCat2Id, string productCat3Id, string productId)
        {
            MenuLevel = menuLevelEnum;
            MenuPath1Id = productCat1Id;
            MenuPath2Id = productCat2Id;
            MenuPath3Id = productCat3Id;
            ProductId = productId;
        }

        public MenuLevelENUM MenuLevel { get; set; }
        public string MenuPath1Id { get; set; }
        public string MenuPath2Id { get; set; }
        public string MenuPath3Id { get; set; }
        public string ProductId { get; set; }


    }
}
