using AliKuli.Extentions;
using AliKuli.ToolsNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.UploadFileNS;

namespace MarketPlace.Web6.Controllers
{
    public class UploadedFilesController : EntityAbstractController<UploadedFile>
    {

        UploadedFileBiz _uploadedfilesBiz;

        public UploadedFilesController(UploadedFileBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _uploadedfilesBiz = biz;
        }

        [HttpPost]
        public ActionResult SavePicture(string imageFile, string id)
        {
            //http://www.dotnetfunda.com/articles/show/2665/saving-html-5-canvas-as-image-in-aspnet-mvc
            //filePath.IsNullOrWhiteSpaceThrowArgumentException("filePath");

            UploadedFile upf = Biz.Find(id);
            upf.IsNullThrowException("upf");

            string onlyFilePath = upf.RelativeWebsitePath;
            string oldFileNameWithPath = upf.GetRelativePathWithFileName();
            string oldFileName = upf.Name;
            string extention = upf.Extention;
            string newFileName = FileTools.CreateNewNameForFile(extention);
            string fileNameWithPath = Server.MapPath(Path.Combine(onlyFilePath, newFileName));


            //delete the old file

            try
            {
                FileTools.Delete(FileTools.GetAbsolutePath(oldFileNameWithPath));
            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("something went wrong during deleting file.", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }

            //update the name in the Uploaded files
            //save to database.
            try
            {
                upf.Name = newFileName;

                ControllerCreateEditParameter param = new ControllerCreateEditParameter();
                param.Entity = upf as ICommonWithId;
                param.GlobalObject = GlobalObject;

                Biz.UpdateAndSave(param);

                //Biz.UpdateAndSave(upf);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("something went wrong during saving.", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());

            }

            //create a new file
            try
            {
                using (FileStream fs = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(imageFile);
                        bw.Write(data);
                        bw.Close();
                    }
                    fs.Close();
                }

                //UploadedFile upf = Biz.Find(id);
                //Biz.Detach(upf);
                return Json(new
                {
                    Success = true
                }, JsonRequestBehavior.DenyGet);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("something went wrong during reading filestream.", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }

        }


        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ModelsClassLibrary.ModelsNS.SharedNS.ControllerIndexParams parm)
        {
            //ViewBag.ShowEditControls = "false";
            ViewBag.ShowDeleteButton = "true";
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            ViewBag.ShowEditControls = "false";
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }


    }
}