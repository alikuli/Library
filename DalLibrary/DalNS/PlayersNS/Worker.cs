//using ModelsClassLibrary.ModelsNS.CommonAndSharedNS;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;
//using EnumLibrary.EnumNS;

//namespace ModelsClassLibrary.ModelsNS.PlayersNS
//{
//    public class Worker : PlayerAbstract
//    {
//        #region Constructor

//        public override void Initialize()
//        {
            
//            BoardingStatus = BoardingENUM.Unknown;
//            EducationLevel = EducationLevelENUM.Unknown;

//        }
        
//        #endregion
//        #region Properties


//        #region Education

//        [Display(Name = "Education Lvl")]
//        public EducationLevelENUM EducationLevel { get; set; }

//        #endregion
//        #region TaxType

//        [MaxLength(100)]
//        [Display(Name = "Tax Code")]
//        public string TaxType { get; set; }


//        #endregion
//        #region BoardingStatus

//        [Display(Name = "Needs Boarding")]
//        public BoardingENUM BoardingStatus { get; set; }

//        #endregion
//        #region FrontIdPictureUploadUrl

//        [Display(Name = "Front ID Picture")]

//        [MaxLength(300)]
//        public string FrontIdPictureUploadUrl { get; set; }

//        #endregion
//        #region BackIdPictureUploadUrl

//        [Display(Name = "Back ID Picture")]
//        [MaxLength(300)]
//        public string BackIdPictureUploadUrl { get; set; }

//        #endregion
//        #region FrontFacePictureUploadUrl


//        [Display(Name = "Front Face Picture")]
//        [MaxLength(300)]
//        public string FrontFacePictureUploadUrl { get; set; }



//        #endregion        [Display(Name = "Side Face Picture")]
//        #region SideFacePictureUploadUrl

//        [MaxLength(300)]
//        public string SideFacePictureUploadUrl { get; set; }


//        #endregion

        
//        #endregion

//        public void LoadFrom(Worker w)
//        {
//            base.LoadFrom(w as PlayerAbstract);
//            EducationLevel = w.EducationLevel;
//            TaxType = w.TaxType;
//            BoardingStatus = w.BoardingStatus;
//            FrontFacePictureUploadUrl = w.FrontFacePictureUploadUrl;
//            BackIdPictureUploadUrl = w.BackIdPictureUploadUrl;
//            FrontIdPictureUploadUrl = w.FrontIdPictureUploadUrl;
//            SideFacePictureUploadUrl = w.SideFacePictureUploadUrl;
//        }
//    }
//}