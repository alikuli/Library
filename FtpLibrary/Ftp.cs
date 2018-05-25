using System;
using System.IO;
using System.Net;
using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;

namespace AliKuli.UtilitiesNS.FtpNS

{
    public class Ftp
    {
        //private  string CompletePath = "C:/Doc/test.txt"; //path for file
        private ErrorSet _err;
        private string _user;

        private ErrorSet Errors {
            get
            {
                return _err ?? (_err = new ErrorSet("FtpLibrary", "Ftp", _user));
            }

        }

        /// <summary>
        /// You do not need stream if you are down loading the file 
        /// </summary>
        /// <param name="ftpUserName"></param>
        /// <param name="password"></param>
        /// <param name="fileName"></param>
        /// <param name="ftpServerIp"></param>
        /// <param name="userLoggedIn"></param>
        /// <param name="stream"></param>
        public Ftp(string ftpUserName, string password, string fileName, string ftpServerIp, string userLoggedIn, Stream stream = null)
        {
            _user = userLoggedIn;
            CheckUserPassworFtpServerIp(ftpUserName, password, stream, fileName, ftpServerIp);

            if(_err.HasErrors)
            {
                throw new Exception(_err.ToString());
            }

            FtpUserName = ftpUserName;
            Password = password; ;
            ServerIp = ftpServerIp;
            Stream = stream;


        }

        private void CheckUserPassworFtpServerIp(string ftpUserName, string password, Stream stream, string fileName, string ftpServerIp)
        {
            if (ftpUserName.IsNullOrEmpty())
                Errors.Add("Ftp Username empty.", "Ftp Constructor");


            if (password.IsNullOrEmpty())
                Errors.Add("Ftp Password empty.", "Ftp Constructor");


            if (ftpServerIp.IsNullOrEmpty())
                Errors.Add("Ftp ServerIp empty.", "Ftp Constructor");

            if (stream.IsNull())
                Errors.Add("No stream.", "Ftp Constructor");

            if (fileName.IsNullOrWhiteSpace())
                Errors.Add("No file name.", "Ftp Constructor");
        }


        private string FtpUserName { get; set; }
        private string Password { get; set; }

        private string ServerIp { get; set; }

        private Stream Stream { get; set; }

        private string FtpUri
        {
            get
            {
                return "ftp://" + ServerIp + "/" + FileName;
            }
        }

        public string FileName { get; set; }

        private FtpWebRequest SetObjFtp
        {
            get
            {
                try
                {

                    if (FileName.IsNullOrWhiteSpace())
                        Errors.Add("No file name", "SetObjFtp");

                    if (_err.HasErrors)
                        throw new Exception(Errors.ToString());


                    string uri = FtpUri;
                    FtpWebRequest objFTP;
                    objFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                    objFTP.Credentials = new NetworkCredential(FtpUserName, Password);
                    objFTP.UsePassive = true;
                    objFTP.KeepAlive = false;
                    objFTP.Proxy = null;
                    objFTP.UseBinary = false;
                    objFTP.Timeout = 90000;
                    return objFTP;
                }
                catch (Exception e)
                {
                    _err.Add("", "SetObjFtp", e);
                    throw new Exception(Errors.ToString());
                }
            }
        }

        /// <summary>
        /// This uploads the file using FTP
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="serverIp"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public bool UploadFile()
        {
            //UserName = userName;
            //Password = passWord;
            //ServerIp = serverIp;
            //the filename will become a part of the URL in Filename property
            //FileName = fileName;
            FtpWebRequest objFTP = null;


            try
            {
                objFTP = SetObjFtp;
                objFTP.Method = WebRequestMethods.Ftp.UploadFile;

                using (Stream fs = Stream)
                {
                    byte[] buff = new byte[fs.Length];
                    using (Stream strm = objFTP.GetRequestStream())
                    {
                        var contentLen = fs.Read(buff, 0, buff.Length);
                        while (contentLen != 0)
                        {
                            strm.Write(buff, 0, buff.Length);
                            contentLen = fs.Read(buff, 0, buff.Length);
                        }
                        objFTP = null;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                if (objFTP != null)
                {
                    objFTP.Abort();
                }
                Errors.Add("Upload Failed", "UploadFile", e);
                throw new Exception(Errors.ToString());
            }
        }
        /// <summary>
        /// This downloads the file using FTP
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="serverIp"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public bool DownloadFile()
        {

            //UserName = userName;
            //Password = passWord;
            //ServerIp = serverIp;
            //the filename will become a part of the URL in Filename property
            //FileName = fileName;

            FtpWebRequest objFTP = null;
            try
            {
                objFTP = SetObjFtp;
                objFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                
                using (FtpWebResponse response = (FtpWebResponse)objFTP.GetResponse())//get file from ftp
                {
                    using (Stream ftpStream = response.GetResponseStream())
                    {
                        int contentLen;
                        // save file in buffer
                        using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate))
                        {
                            byte[] buff = new byte[2048];
                            contentLen = ftpStream.Read(buff, 0, buff.Length);
                            while (contentLen != 0)
                            {
                                fs.Write(buff, 0, buff.Length);
                                contentLen = ftpStream.Read(buff, 0, buff.Length);
                            }
                            objFTP = null;
                        }
                    }
                }
                
                return true;
            }

            catch (Exception e)
            {
                if (objFTP != null)
                {
                    objFTP.Abort();
                }
                Errors.Add("Upload Failed", "DownLoadFile", e);
                throw new Exception(Errors.ToString());

            }
        }
    }


}