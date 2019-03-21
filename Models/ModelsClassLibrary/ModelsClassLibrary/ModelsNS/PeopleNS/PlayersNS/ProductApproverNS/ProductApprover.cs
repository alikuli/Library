using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    /// <summary>
    /// ProductApprover is owner who has privilages to:
    ///     Invoice
    ///     Receive Payments against Invoice
    ///     Etc
    ///     ProductApprover = VENDOR
    /// </summary>
    public class ProductApprover : PlayerAbstract, IPlayer
    {


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.ProductApprover;
        }



        [Display(Name = "Product Approver")]
        [MaxLength(128)]
        public virtual string ProductApproverCategoryId { get; set; }
        public virtual ProductApproverCategory ProductApproverCategory { get; set; }



        [NotMapped]
        public SelectList SelectListProductApproverCategory { get; set; }




        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);
            ProductApprover owner = ic as ProductApprover;
            owner.IsNullThrowException("Unable to unbox owner");
            ProductApproverCategoryId = owner.ProductApproverCategoryId;
        }
    }
}