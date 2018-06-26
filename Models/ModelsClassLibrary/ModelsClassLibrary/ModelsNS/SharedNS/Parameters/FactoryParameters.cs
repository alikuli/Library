using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{

    /// <summary>
    /// This is used to create the VMs for the Products
    /// </summary>
    public class FactoryParameters
    {
        public FactoryParameters(MenuLevelENUM menuLevelEnum, string menuPathMainId)
        {
            MenuLevelEnum = menuLevelEnum;
            MenuPathMainId = MenuPathMainId;
        }

        public MenuLevelENUM MenuLevelEnum { get; set; }
        public string MenuPathMainId { get; set; }


    }
}
