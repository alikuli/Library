
namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.CreateMenuStatesNS
{
    public class CreateEditMenuState_Lv1 : CreateEditMenuStateAbstract
    {
        public CreateEditMenuState_Lv1(
            string currentViewName, 
            string menuPath1Id, 
            string menuPath2Id, 
            string menuPath3Id, 
            string productId, 
            string productChildId,
            string menuPathMainId,
            string productVmNameEditLink)
            : base(currentViewName, menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, menuPathMainId, productVmNameEditLink)
        {
            _currrentViewName = currentViewName;
        }



    }
}
