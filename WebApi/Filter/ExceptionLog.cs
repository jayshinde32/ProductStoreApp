using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Http.ExceptionHandling;

namespace WebApi.Filter
{
    public class ExceptionLog: ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var exceptionMessage = context.Exception.Message;
            var stackTrace = context.Exception.StackTrace;
            var requestedURi = (string)context.Request.RequestUri.AbsoluteUri;
            var requestMethod = context.Request.Method.ToString();

            string Message = Environment.NewLine + "Date :" + DateTime.Now.ToString() + ",\n Requested Uri: " + requestedURi + ", Requested Method:" + requestMethod +
                             "\n Error Message : " + exceptionMessage
                            + Environment.NewLine + "Stack Trace : " + stackTrace+ Environment.NewLine ;
            string strFileName = DateTime.Now.ToShortDateString();
            File.AppendAllText(ConfigurationManager.AppSettings["LogPath"] + "ApiErrorLog_"+ strFileName + ".txt", Message);
        }
       
    }
}