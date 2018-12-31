using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;

namespace UowLibrary.UserAndRightsAndUploadsBiz
{
    public class UserAndRightsAndUploadsBiz
    {
        UserBiz _userBiz;
        RightBiz _rightBiz;
        UploadedFileBiz _uploadedFileBiz;
        public UserAndRightsAndUploadsBiz(UserBiz userBiz, RightBiz rightBiz, UploadedFileBiz uploadedFileBiz)
        {
            _userBiz = userBiz;
            _rightBiz = rightBiz;
            _uploadedFileBiz = uploadedFileBiz;
        }

        public UserBiz UserBiz
        {
            get
            {
                return _userBiz;
            }
        }

        public RightBiz RightBiz
        {
            get
            {
                return _rightBiz;
            }
        }

        public UploadedFileBiz  UploadedFileBiz 
        {
            get { return _uploadedFileBiz; }
        }
    }
}
