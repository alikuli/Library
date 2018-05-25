using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.IO;
using System.Web;
namespace ModelsClassLibrary.ModelsNS.UploadedFileNS
{
    /// <summary>
    /// Note the new name of the file will be in Name in the website, without the extention. This stores the old and the new name along with the extention.
    /// This automatically saves the files to the disk, i.e. in the constructor.
    /// </summary>
    public partial class UploadedFile : CommonWithId
    {



        #region Image Adjustment

        private string maxSizeInBytes = AliKuli.ConstantsNS.MyConstants.SIZE_PIC_BYTES_TO_SAVE;
        private string maxHeightInPts = AliKuli.ConstantsNS.MyConstants.SIZE_PIC_HEIGHT_IN_PTS_MAX;
        private string maxWidthInPts = AliKuli.ConstantsNS.MyConstants.SIZE_PIC_WIDTH_IN_PTS_MAX;

        private double MaxSizeInBytes
        {
            get
            {
                double d = 0;
                bool success = double.TryParse(maxSizeInBytes, out d);
                if (!success)
                {
                    throw new Exception(string.Format("Unable to parse Max Size In Bytes. Value recieved is '{0}", maxSizeInBytes));
                }
                return d;
            }
        }
        private double MaxWidthInPts
        {
            get
            {
                double d = 0;
                bool success = double.TryParse(maxWidthInPts, out d);
                if (!success)
                {
                    throw new Exception(string.Format("Unable to parse Max Width In Points. Value recieved is '{0}", maxWidthInPts));
                }
                return d;
            }
        }
        private double MaxHeightInPts
        {
            get
            {
                double d = 0;
                bool success = double.TryParse(maxHeightInPts, out d);
                if (!success)
                {
                    throw new Exception(string.Format("Unable to parse Max Height In Points. Value recieved is '{0}", maxHeightInPts));
                }
                return d;
            }
        }
        public HttpPostedFileBase ConvertHttpImageToBig(HttpPostedFileBase fileIn)
        {

            return fileIn;
        }
        public HttpPostedFileBase ConvertHttpImageToMedium(HttpPostedFileBase fileIn)
        {
            return fileIn;
        }
        public HttpPostedFileBase ConvertHttpImageToSmall(HttpPostedFileBase fileIn)
        {
            return fileIn;
        }

        public Stream ConvertImageToBig(Stream stream)
        {

            return stream;
        }
        public Stream ConvertImageToMedium(Stream stream)
        {
            return stream;
        }
        public Stream ConvertImageToSmall(Stream stream)
        {
            return stream;
        }

        #endregion




    }
}
