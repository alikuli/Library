using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MarketPlace.Web6.ProgramsNS
{
    public class Tools
    {

        //https://www.codemag.com/article/1312081/Rendering-ASP.NET-MVC-Razor-Views-to-String
        public static string RenderViewToString(
                                            ControllerContext context,
                                            string viewPath,
                                            object model = null,
                                            bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                            context.Controller.ViewData,
                                            context.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }

        //https://www.codemag.com/article/1312081/Rendering-ASP.NET-MVC-Razor-Views-to-String
        public static T CreateController<T>(RouteData routeData = null)
            where T : Controller, new()
        {
            // create a disconnected controller instance
            T controller = new T();

            // get context wrapper from HttpContext if available
            HttpContextBase wrapper;
            if (System.Web.HttpContext.Current != null)
                wrapper = new HttpContextWrapper(System.Web.HttpContext.Current);
            else
                throw new InvalidOperationException(
                    "Can't create Controller Context if no " +
                    "active HttpContext instance is available.");

            if (routeData == null)
                routeData = new RouteData();

            // add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") &&
                !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller",
                                     controller.GetType()
                                               .Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
            return controller;
        }


        //public class ErrorModule : ApplicationErrorModule
        //{

        //    protected override void OnDisplayError(
        //                               WebErrorHandler errorHandler,
        //                               ErrorViewModel model)
        //    {
        //        var response = HttpContext.Current.Response;

        //        // Create an arbitrary controller instance
        //        var controller =
        //            ViewRenderer.CreateController<GenericController>();

        //        string html = ViewRenderer.RenderPartialView(
        //                                    "~/views/shared/Error.cshtml",
        //                                    model,
        //                                    controller.ControllerContext);

        //        HttpContext.Current.Server.ClearError();
        //        response.TrySkipIisCustomErrors = true;
        //        response.ClearContent();

        //        response.StatusCode = 500;
        //        response.Write(html);
        //    }
        //}
    }
}