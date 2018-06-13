using AliKuli.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AliKuli.ToolsNS
{
    public class FileTools
    {
        /// <summary>
        /// These reads a text file line by line and creates a List string of it
        /// </summary>
        /// <param name="fileNameWithPath"></param>
        /// <returns></returns>
        public static List<string> ReadTextFile(string fileNameWithPath)
        {
            List<string> sList = new List<string>();

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileNameWithPath))
                {
                    // Read the stream to a string, and write the string to the console.
                    while (sr != null)
                    {
                        String line = sr.ReadLine();

                        if (string.IsNullOrWhiteSpace(line))
                            break;
                        else
                            sList.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                string err = string.Format("The file named: '{0}'could not be read. {1}", fileNameWithPath, e.Message);
                Console.WriteLine(err);
                throw new Exception(err);
            }

            Console.WriteLine("*** Number of Records: {0:n0}", sList.Count);
            Console.ReadLine();

            return sList;
        }




        public static string ReadTextFileToString(string fileNameWithPath)
        {
            string str = "";

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileNameWithPath))
                {
                    // Read the stream to a string, and write the string to the console.
                    str = sr.ReadToEnd();
                    return str;
                }
            }
            catch (Exception e)
            {
                string err = string.Format("The file named: '{0}'could not be read. {1}", fileNameWithPath, e.Message);
                Console.WriteLine(err);
                throw new Exception(err);
            }

        }

        //https://www.techrepublic.com/blog/how-do-i/how-do-i-use-c-to-upload-and-download-files-from-an-ftp-server/
        /// <summary>
        ///   setup a WebClient object and set the Credentials property to our login information then 
        ///   call the DownloadData method of the WebClient object and supply the URI of the file we want to download. 
        ///  The DownloadData method returns an array of bytes which represent the downloaded file. This byte array 
        ///  is then written to a file, and the download is complete
        /// </summary>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpPassword"></param>
        /// <param name="downloadFileWithPath"></param>
        /// <param name="pathToWriteTo"></param>
        public static void DownloadFromFTP(string ftpUserName, string ftpPassword, string downloadFileWithPath, string pathToWriteTo)
        {
            WebClient request = new WebClient();
            System.Security.SecureString secureFtpPassword = new System.Security.SecureString();
            if (ftpPassword.Length > 0)
            {
                for (int i = 0; i < ftpPassword.Length; i++)
                {
                    secureFtpPassword.AppendChar(ftpPassword[i]);
                }
            }
            //setup our credentials
            request.Credentials = new NetworkCredential(ftpUserName, secureFtpPassword);

            string fileNameNoExtention = Path.GetFileNameWithoutExtension(downloadFileWithPath);
            string extention = Path.GetExtension(downloadFileWithPath);
            string fileNameWithExtention = Path.GetFileName(downloadFileWithPath);

            //Download the data into a Byte array
            byte[] fileData =
                request.DownloadData(downloadFileWithPath);


            //create a FileStream that we'll write the
            //byte array to.
            FileStream file = File.Create(downloadFileWithPath);

            //Write the full byte array to the file.
            file.Write(fileData, 0, fileData.Length);
            file.Close();



        }

        public static void UploadToFtp(string ftpUserName, string ftpPassword, string uploadFileWithPath, string pathToWriteTo)
        {
            //Get a FIleInfo object for the file that will be uploaded.
            FileInfo toUpload = new FileInfo(uploadFileWithPath);

            string completeNameOfFtpFile = string.Format(@"{0}/{1)", pathToWriteTo, toUpload.Name);
            //Get a new FtpWebRequest object
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(completeNameOfFtpFile);

            System.Security.SecureString secureFtpPassword = new System.Security.SecureString();

            request.Credentials = new NetworkCredential(ftpUserName, secureFtpPassword);

            //setup a stream for the request and a stream for
            //the file we'll be uploading.
            Stream ftpStream = request.GetRequestStream();
            FileStream file = File.OpenRead(uploadFileWithPath);

            //setup variables we will use to read the file
            int length = 1024;
            byte[] buffer = new byte[length];
            int bytesRead = 0;


            //now write the file to the request stream.
            do
            {
                bytesRead = file.Read(buffer, 0, length);
                ftpStream.Write(buffer, 0, bytesRead);
            }
            while (bytesRead != 0);

            //close the streams
            file.Flush();
            file.Close();
            ftpStream.Close();

        }




        /// <summary>
        /// This only copies the initFileName from the Initialization Directory to the relativePath.
        /// Returns the new name,
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        public static string CopyFileAndGiveNewName(string sourcePath, string targetPath, string fileName)
        {
            if (sourcePath.IsNullOrWhiteSpace())
            {
                string e = string.Format("Missing sourcePath argument; {0}", sourcePath);
                throw new ArgumentException(e);
            }
            if (targetPath.IsNullOrWhiteSpace())
            {
                string e = string.Format("Missing relativePath argument; {0}", targetPath);
                throw new ArgumentException(e);

            }
            if (!IsExistDirectory(sourcePath))
            {
                throw new Exception(string.Format("Source path:'{0}' does not exist.", sourcePath));
            }

            CreateDirectory(targetPath);

            string extention = Path.GetExtension(fileName);
            string name = CreateNewNameForFile(extention);
            string sourceFileWithPath = Path.Combine(sourcePath, fileName);
            string destFileName = Path.Combine(targetPath, name);

            File.Copy(sourceFileWithPath, destFileName);

            return name;



        }


        #region Directory

        public static DirectoryInfo CreateDirectory(string diretory)
        {
            if (!System.IO.Directory.Exists(diretory))
            {
                DirectoryInfo dinfo = System.IO.Directory.CreateDirectory(diretory);
                return dinfo;
            }

            return null;
        }

        public static bool IsExistDirectory(string diretory)
        {
            return System.IO.Directory.Exists(diretory);

        }

        #endregion
        public static bool IsExistFile(string filepath)
        {
            if (filepath.IsNullOrWhiteSpace())
            {
                string e = string.Format("Missing file path argument; {0}", filepath);
                throw new ArgumentException(e);
            }

            return File.Exists(filepath);

        }


        /// <summary>
        /// Creates a new name for file from tics.
        /// </summary>
        /// <param name="extention"></param>
        /// <returns></returns>
        public static string CreateNewNameForFile(string extention)
        {

            string name = DateTime.Now.Ticks.ToString();

            if (!extention.IsNullOrWhiteSpace())
                name += extention;

            return name;
        }

        /// <summary>
        /// Checks to see if file exists, if so, then deletes
        /// </summary>
        /// <param name="filepath"></param>
        public static void Delete(string filepath)
        {
            if (IsExistFile(filepath))
                File.Delete(filepath);
        }


        /// <summary>
        /// This parses a simple comma delimited list
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static String[] ParseCsvCommaDelimited(string fileName)
        {
            string str = ReadTextFileToString(fileName);
            if (str.IsNullOrWhiteSpace())
                return null;

            string[] words = str.Split(',');
            return words;

        }

        public static string GetPath(string filename)
        {
            filename.IsNullOrWhiteSpaceThrowException("Filename is empty");
            string filepath = System.Web.Hosting.HostingEnvironment.MapPath(filename);
            return filepath;
        }
    }
}
