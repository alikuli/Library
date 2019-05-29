using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Linq;
using UowLibrary.AddressNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerCategoryNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
using UserModels;

namespace UowLibrary.PlayersNS.OwnerNS
{
    public partial class OwnerBiz : BusinessLayerPlayer<Owner>
    {

        readonly OwnerCategoryBiz _ownerCategoryBiz;

        public OwnerBiz(IRepositry<Owner> entityDal, BizParameters bizParameters, OwnerCategoryBiz ownerCategoryBiz, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(entityDal, bizParameters, addressBiz, cashTrxBiz)
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

        public Owner GetOwnerForUser(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowException("userId");
            Person person = UserBiz.GetPersonFor(userId);
            person.IsNullThrowException("Person not found");
            string personId = person.Id;
            //now get the Owner for this person
            Owner owner = FindAll().FirstOrDefault(X => X.PersonId == personId);
            return owner;

        }

        public override Owner GetPlayerFor(string userId)
        {
            Owner owner = base.GetPlayerFor(userId);

            if (owner.IsNull())
            {
                ApplicationUser user = GetUser(userId);
                user.IsNullThrowException("user");
                
                Person person = GetPersonForUserId(userId);

                //create a new customer for this
                owner = Factory() as Owner;
                owner.IsNullThrowException("Owner");
                owner.Name = user.UserName;
                owner.PersonId = person.Id;
                owner.DefaultBillAddressId = person.DefaultBillAddressId;
                //owner.DefaultShipAddressId = person.
                //add default addresses
                CreateAndSave(owner);
            }

            return owner;
        }

    }
}
