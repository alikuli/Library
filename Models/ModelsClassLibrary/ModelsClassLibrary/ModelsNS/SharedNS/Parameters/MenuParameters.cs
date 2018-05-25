
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class MenuParameters
    {
        public MenuParameters()
        {
            MenuLevel = MenuLevelENUM.unknown;
        }
        public MenuParameters(MenuLevelENUM menuLevelEnum, string productCat1Id, string productCat2Id, string productCat3Id)
        {
            MenuLevel = menuLevelEnum;
            ProductCat1Id = productCat1Id;
            ProductCat2Id = productCat2Id;
            ProductCat3Id = productCat3Id;

        }

        public MenuLevelENUM MenuLevel { get; set; }
        public string ProductCat1Id { get; set; }
        public string ProductCat2Id { get; set; }
        public string ProductCat3Id { get; set; }


    }
}
