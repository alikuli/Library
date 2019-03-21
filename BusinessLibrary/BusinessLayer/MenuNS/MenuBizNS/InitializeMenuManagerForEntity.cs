using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {


        public override void InitializeMenuManagerForEntity(ControllerIndexParams parm, InterfacesLibrary.SharedNS.ICommonWithId entity)
        {
            base.InitializeMenuManagerForEntity(parm, entity);
            if(!UserId.IsNullOrWhiteSpace())
            {
                Person person= UserBiz.GetPersonFor(UserId);
                person.IsNullThrowException("person");
                entity.MenuManager.UserPersonId = person.Id;
            }
        }
    }
}
