//using BreadCrumbsLibraryNS.Programs;
//using ErrorHandlerLibrary.ExceptionsNS;
//using UowLibrary;
//using UowLibrary.MyWorkClassesNS;
//using UowLibrary.PlayersNS;
//using UowLibrary.UploadFileNS;
//using WebLibrary.Programs;

//namespace ModelsClassLibrary.ModelsNS.SharedNS
//{
//    public class MyWorkClasses
//    {
//        readonly RightBiz _rightBiz;
//        readonly MyWorkClasses _myWorkClasses;

//        public MyWorkClasses(RightBiz rightBiz, MyWorkClasses myWorkClasses)
//        {
//            _rightBiz = rightBiz;
//            _myWorkClasses = myWorkClasses;
//        }

//        public UploadedFileBiz UploadedFileBiz
//        {
//            get
//            {
//                return RightBiz.UploadedFileBiz;
//            }
//        }

//        public UserBiz UserBiz
//        {
//            get
//            {
//                return RightBiz.UserBiz;
//            }
//        }

//        public RightBiz RightBiz
//        {
//            get
//            {
//                return _rightBiz;
//            }
//        }

//        public MyWorkClasses MyWorkClasses
//        {
//            get
//            {
//                return _myWorkClasses;
//            }
//        }

//        public IMemoryMain MemoryMain
//        {
//            get
//            {
//                return MyWorkClasses.MemoryMain;
//            }
//        }
//        public ErrorSet ErrorsGlobal
//        {
//            get
//            {
//                return MyWorkClasses.ErrorSet as ErrorSet;
//            }
//        }

//        public CountryBiz CountryBiz
//        {
//            get
//            {
//                //RightBiz.UserBiz.CountryBiz.UserNameBiz = UserName;
//                //RightBiz.UserBiz.CountryBiz.UserIdFromBiz = UserId;
//                return RightBiz.UserBiz.CountryBiz;
//            }
//        }
//        public BreadCrumbManager BreadCrumbManager
//        {
//            get
//            {
//                return MyWorkClasses.BreadCrumbManager;
//            }
//        }
//    }
//}
