using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.GlobalCommentsNS
{
    public partial class GlobalComment : CommonWithId
    {
        public GlobalComment()
        {

        }

        public GlobalComment(string comment, string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId)
        {
            Initialize(comment, menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, userId);
        }

        public void Initialize(string comment, string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId)
        {
            Comment = comment;
            MenuPath1Id = menuPath1Id;
            MenuPath2Id = menuPath2Id;
            MenuPath3Id = menuPath3Id;
            ProductId = productId;
            ProductChildId = productChildId;
            UserId = userId;
            Name = DateTime.Now.Ticks.ToString();
        }

        public override bool IsAllowDuplicates
        {
            get
            {
                return true;
            }
        }

        [Display(Name = "User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        [Display(Name = "Menu 1")]
        public string MenuPath1Id { get; set; }
        public MenuPath1 MenuPath1 { get; set; }


        [Display(Name = "Menu 2")]
        public string MenuPath2Id { get; set; }
        public MenuPath2 MenuPath2 { get; set; }


        [Display(Name = "Menu 3")]
        public string MenuPath3Id { get; set; }
        public MenuPath3 MenuPath3 { get; set; }

        [Display(Name = "Product")]
        public string ProductId { get; set; }
        public Product Product { get; set; }


        [Display(Name = "Cust Product")]
        public string ProductChildId { get; set; }
        public ProductChild ProductChild { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.GlobalComments;
        }



        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);

            GlobalComment gc = icommonWithId as GlobalComment;
            gc.IsNullThrowException();

            UserId = gc.UserId;
            MenuPath1Id = gc.MenuPath1Id;
            MenuPath2Id = gc.MenuPath2Id;
            MenuPath3Id = gc.MenuPath3Id;
            ProductId = gc.ProductId;
            ProductChildId = gc.ProductChildId;

        }
    }
}
