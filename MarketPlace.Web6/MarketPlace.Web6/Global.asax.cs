using ApplicationDbContextNS;
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MarketPlace.Web6
{
    public class MvcApplication : System.Web.HttpApplication
    {


        protected void Application_Start()
        {
            ModelBinders.Binders.DefaultBinder = new PerpetuumSoft.Knockout.KnockoutModelBinder();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CreateInitializationDirectories();



        }

        //https://stackoverflow.com/questions/12553639/ef-codefirst-get-all-poco-types-for-dbcontext
        //returns a list of all POCO class names
        protected System.Collections.ObjectModel.ReadOnlyCollection<EntityType> ListOfAllClassEntyTypes()
        {
            //var lstOfClasses = Enum.GetNames(typeof(ClassesWithRightsENUM));
            ApplicationDbContext ctx = new ApplicationDbContext();
            var objectContext = ((IObjectContextAdapter)ctx).ObjectContext;

            var mdw = objectContext.MetadataWorkspace;

            var lstClassesNames = mdw.GetItems<EntityType>(DataSpace.OSpace);
            return lstClassesNames;
        }

        /// <summary>
        /// This creates the initialization directories. This runs at startup.
        /// </summary>
        public void CreateInitializationDirectories()
        {
            //var lstClassesNames = ListOfAllClassEntyTypes();

            //if (lstClassesNames.IsNullOrEmpty())
            //{
            //    throw new Exception("Programming error. No POCO list recieved");
            //}
            string filename = AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;
            string path = HttpContext.Current.Server.MapPath(filename);
            AliKuli.ToolsNS.FileTools.CreateDirectory(path);
            //foreach (var entityType in lstClassesNames)
            //{


            //    //var cls = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(classname.Name);
            //    //var cls = Activator.CreateInstance()

            //    IHasUploads ihasuploads;
            //    ICommonWithId icommonWithId;
            //    var type = Type.GetType(entityType.FullName);
            //    if (type.IsNull())
            //    {
            //        //the type is in a different assembly.
            //        var type2 = GetInstance(entityType.FullName);
            //        ihasuploads = type2 as IHasUploads;
            //        icommonWithId = type2 as ICommonWithId;

            //    }
            //    else
            //    {

            //        ihasuploads = type as IHasUploads;
            //        icommonWithId = type as ICommonWithId;

            //    }

            //    if (!ihasuploads.IsNull())
            //    {
            //        string rawName = icommonWithId.ClassNameRaw.ToLower();
            //        switch (rawName)
            //        {
            //            case "menupath1": rawName = "menupaths";
            //                break;
            //            case "menupath2": continue;
            //            case "menupath3": continue; ;

            //        }
            //        //string filename = AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;
            //        //string path = HttpContext.Current.Server.MapPath(filename);
            //        ////this  has uploads then we need to make the directory
            //        //AliKuli.ToolsNS.FileTools.CreateDirectory(path);
            //    }
            //}
        }

        //If your Fully Qualified Name(ie, Vehicles.Car in this case) is in another assembly, the Type.GetType will be null. In such cases, you have loop through all assemblies and find the Type. For that you can use the below code
        //https://stackoverflow.com/questions/223952/create-an-instance-of-a-class-from-a-string
        public object GetInstance(string strFullyQualifiedName)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
                return Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type);
            }
            return null;
        }

        private void Application_Error(object sender, EventArgs e)
        {


            HttpContext httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
                /* When the request is ajax the system can automatically handle a mistake with a JSON response. 
                   Then overwrites the default response */
                if (requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    httpContext.Response.Clear();
                    string controllerName = requestContext.RouteData.GetRequiredString("controller");
                    IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
                    IController controller = factory.CreateController(requestContext, controllerName);
                    ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);
                    string errorMessage = requestContext.HttpContext.Error.Message ?? "No Error message";
                    JsonResult jsonResult = new JsonResult
                    {
                        Data = new { success = false, serverError = "500", message = errorMessage },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    jsonResult.ExecuteResult(controllerContext);
                    httpContext.Response.End();
                }
                else
                {

                    var ex = Server.GetLastError();
                    var httpException = ex as HttpException ?? ex.InnerException as HttpException;
                    if (httpException == null) return;

                    if (httpException.WebEventCode == WebEventCodes.RuntimeErrorPostTooLarge)
                    {


                        httpContext.Response.Redirect("~/Errors/Index?message=Your file was too large. Please select a smaller file or compress it! Max size allowed is less than 3MB");
                        //httpContext.Response.Redirect("~/Errors/FileTooBig");
                        //                        Response.Write("Too big a file, dude"); //for example
                    }

                }
            }


            //---------------------------------------------------------------------------


        }

    }
}
