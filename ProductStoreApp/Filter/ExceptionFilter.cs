using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ProductStoreApp.Filter
{
    public class ExceptionFilter: FilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exceptionMessage = filterContext.Exception.Message;
                var stackTrace = filterContext.Exception.StackTrace;
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                string Message = Environment.NewLine +"Date :" + DateTime.Now.ToString() + ",\n Controller: " + controllerName + ", Action:" + actionName +
                                 "\n Error Message : " + exceptionMessage
                                + Environment.NewLine + "Stack Trace : " + stackTrace + Environment.NewLine;

                string strFileName = DateTime.Now.Date.ToShortDateString();
                File.AppendAllText(ConfigurationManager.AppSettings["LogPath"]+"AppErrorLog_"+ strFileName + ".txt", Message);             

                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }
        }
        }
}