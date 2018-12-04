using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerCategoryNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
using AliKuli.Extentions;

namespace UowLibrary.PlayersNS.OwnerNS
{
    public partial class OwnerBiz : BusinessLayerPlayer<Owner>
    {

        readonly OwnerCategoryBiz _ownerCategoryBiz;

        public OwnerBiz(IRepositry<Owner> entityDal, BizParameters bizParameters, OwnerCategoryBiz ownerCategoryBiz, AddressBiz addressBiz)
            : base(entityDal, bizParameters,  addressBiz)
        {

            _ownerCategoryBiz = ownerCategoryBiz;
        }



        public OwnerCategoryBiz OwnerCategoryBiz 
        { 
            get 
            {
                _ownerCategoryBiz.IsNullThrowException("_ownerCategoryBiz");
                _ownerCategoryBiz.UserId = UserId;
                _ownerCategoryBiz.UserName = UserName;
                return _ownerCategoryBiz; 
            } 
        }

    }
}
