using System;
using System.Collections.Generic;
using System.Linq;
using AliKuli.Extentions;
using InterfacesLibrary.DocumentsNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UserModelsLibrary.ModelsNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS
{
    /// <summary>
    /// These are categories you can use for your files.
    /// </summary>
    public class FileCategory :  CommonWithId, IFileCategory
    {

        public FileCategory()
        {
            Files = new List<IFileDoc>();
        }
        #region Properties




        
        /// <summary>
        /// This is the owning User.
        /// </summary>
        #region User
        public virtual ApplicationUser User { get; set; }
        public Guid UserId { get; set; }

        #endregion


        public int OldId { get; set; }

        #endregion
        #region Overrides
        public override string ToString()
        {
            string files = "";
            var filesList = Files.ToList();


            if (!filesList.IsNullOrEmpty())
            {
                foreach (var item in filesList)
                {
                    files += item.Name + "; ";
                }
            }

            string f = files;
            string n = Name;

            string s = string.Format("Name: {0}, files '{1}'",
                n,
                f);
            return s;
        }

        #endregion

        #region Navigation

        public virtual ICollection<IFileDoc> Files { get; set; }

        #endregion

        #region SelfErrorCheck
        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            Check_User();

        }
        private void Check_User()
        {
            if (User.IsNull())
                throw new Exception("There is no user. Not allowed. FileCategory.Check_User");

            if (UserId.IsNullOrEmpty())
                throw new Exception("User Id is empty. Not allowed. FileCategory.Check_User");


        }

        #endregion

        #region String methods
        public override string MakeUniqueName()
        {
            string name = string.Format("{0}{1}", Name, UserId);
            return name;
        }

        public override string IdString()
        {

            string name = string.Format("{0}{1}", User.ToString(), Name);
            return base.IdString();
        }

        public override string FullName()
        {
            string name = string.Format("{0} ({1})", Name, ((User)User).UserName);
            return name;
        }

        #endregion
        public void LoadFrom(FileCategory f)
        {
            base.LoadFrom(f as ICommonWithId);
            User = f.User;
            UserId = f.UserId;
            OldId = f.OldId;
            Files = f.Files;//Navigation
        }



    }
}