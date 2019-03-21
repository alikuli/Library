using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;


namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {
        private List<string> lstOfIds = new List<string>();
        private Stack<Product> trailStack = new Stack<Product>();

        public override void BusinessRulesFor(ControllerCreateEditParameter parm)
        {
            base.BusinessRulesFor(parm);

            Product p = parm.Entity as Product;
            GetDataFromMenuPathCheckBoxes(p);

            IProduct iproduct = p as IProduct;
            addOwner(p);
            addRemoveApprover(p);
            //FixProductFeatures(iproduct);
        }

        private void addRemoveApprover(Product p)
        {
            if (UserId.IsNullOrWhiteSpace())
                return;
            //if user is not approver dont let him do anything
            //todo
            if (p.IsUnApproved)
            {
                //remove the old info.
                p.ApprovedBy = new DateAndByComplex();
            }
            else
            {
                if (p.ApprovedBy.By.IsNullOrWhiteSpace())
                {
                    if (p.ApprovedBy.By.IsNullOrWhiteSpace())
                    {
                        p.ApprovedBy.SetToTodaysDate(UserName, UserId);
                    }
                }
            }
        }

        private void addOwner(Product p)
        {
            if (IsCreate)
            {
                if (!UserId.IsNullOrWhiteSpace())
                {
                    //user is logged in
                    //make sure the user is an Owner

                    Owner owner = OwnerBiz.GetOwnerForUser(UserId);
                    if (!owner.IsNull())
                    {
                        //user is an owner
                        //Now this user will own this product.
                        p.OwnerId = owner.Id;
                    }

                }
            }
        }



    }
}
