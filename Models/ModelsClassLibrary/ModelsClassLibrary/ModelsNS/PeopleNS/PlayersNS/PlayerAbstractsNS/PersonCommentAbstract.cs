using ModelsClassLibrary.ModelsNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UserModelsLibrary.ModelsNS;

namespace ModelsClassLibrary.ModelsNS.PeopleNS
{
    public class PersonCommentAbstract:CommonWithId
    {
        #region CommentForPerson
        
        [MaxLength(1000)]
        public string ForUserId { get; set; }
        public ApplicationUser ForUser { get; set; }

        #endregion

        public decimal Rating { get; set; }

        #region FromUser

        [MaxLength(1000)]
        public string FromUserId { get; set; }
        public virtual User FromUser { get; set; }

        #endregion

        #region ToUser

        [MaxLength(1000)]
        public string ToUserId { get; set; }
        public virtual User ToUser { get; set; }
        
        #endregion        

        public void LoadFrom(PersonCommentAbstract p)
        {
            base.LoadFrom(p as CommonWithId);

            Rating = p.Rating;
            
            ForUser = p.ForUser;
            ForUserId = p.ForUserId;
            
            FromUserId = p.FromUserId;
            FromUser = p.FromUser;

            ToUser = p.ToUser;
            ToUserId = p.ToUserId;
        }

    }
}