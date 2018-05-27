using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using UserModels;
using WebLibrary.Programs;
using System;
using ModelsClassLibrary.RightsNS;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Text;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Reflection;
using ModelsClassLibrary.MenuNS;

namespace UowLibrary.ProductNS
{
    public partial class ProductCatMainBiz 
    {


        public override void BusinessRulesFor(ProductCategoryMain entity)
        {
            MakeName(entity);

            base.BusinessRulesFor(entity);


        }

        private void MakeName(ProductCategoryMain entity)
        {
            string cat1Name = "";
            string cat2Name = "";
            string cat3Name = "";

            if (!entity.ProductCat1Id.IsNullOrWhiteSpace())
            {
                var c1 = _productCat1Biz.Find(entity.ProductCat1Id);

                if (c1.IsNull())
                {
                    ErrorsGlobal.Add("Product Category 1 was not found", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }
                cat1Name = c1.Name;

            }

            if (!entity.ProductCat2Id.IsNullOrWhiteSpace())
            {
                var c2 = _productCat2Biz.Find(entity.ProductCat2Id);

                if (c2.IsNull())
                {
                    entity.MakeName(cat1Name, cat2Name, cat3Name);
                    return;
                }
                cat2Name = c2.Name;

            }


            if (!entity.ProductCat3Id.IsNullOrWhiteSpace())
            {
                var c3 = _productCat3Biz.Find(entity.ProductCat3Id);

                if (!c3.IsNull())
                {
                    cat3Name = c3.Name;
                }

            }

            entity.MakeName(cat1Name, cat2Name, cat3Name);
        }




        private string MakeName(Right entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(entity.RightsFor.ToString());

            if (!entity.Id.IsNullOrWhiteSpace())
            {
                //locate the user for whom you are creating the rights for.
                var user = UserDal.FindForLightNoTracking(entity.UserId);
                if (user.IsNull())
                {
                    ErrorsGlobal.Add("User for whom Rights are being created was not found!", "Business Rules");
                    throw new Exception(ErrorsGlobal.ToString());
                }

                sb.Append(string.Format(" {0} [{1}]",
                    user.UserName, 
                    user.Id));


            }
            return sb.ToString();
        }        

    }
}
