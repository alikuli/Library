using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.ModelsNS.SharedNS.Parameters
{
    public class GlobalCommentParameter
    {
        public GlobalCommentParameter(string comment, string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId)
        {
            Comment = comment;
            MenuPath1Id = menuPath1Id;
            MenuPath2Id = menuPath2Id;
            MenuPath3Id = menuPath3Id;
            ProductId = productId;
            ProductChildId = productChildId;
            UserId = userId;
        }
        public string Comment {get;set;}
        public string MenuPath1Id {get;set;}
        public string MenuPath2Id {get;set;}
        public string MenuPath3Id {get;set;}
        public string ProductId {get;set;}
        public string ProductChildId {get;set;}
        public string UserId { get; set; }

        public void LoadInto(GlobalComment gc)
        {
            gc.Comment = Comment;
            gc.MenuPath1Id = MenuPath1Id;
            gc.MenuPath2Id = MenuPath2Id;
            gc.MenuPath3Id = MenuPath3Id;
            gc.ProductId = ProductId;
            gc.ProductChildId = ProductChildId;
            gc.UserId = UserId;
        }
    }
}
