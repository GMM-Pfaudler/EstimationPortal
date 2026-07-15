using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EstimationPortal.Utility
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string innerexception = "";
            string innerst = "";
            if (!filterContext.ExceptionHandled)
            {
                var exceptionMessage = filterContext.Exception.Message;
                var stackTrace = filterContext.Exception.StackTrace;
                if (filterContext.Exception.InnerException != null)
                {
                    innerexception = filterContext.Exception.InnerException.Message;
                    innerst = filterContext.Exception.InnerException.StackTrace;
                }
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();
                string Message = "Date :" + DateTime.Now.ToString() + ", Controller: " + controllerName + ", Action:" + actionName +
                                 "Error Message : " + exceptionMessage
                                + Environment.NewLine + "Stack Trace : " + stackTrace + "innerexception:" + innerexception + "innerst:" + innerst;

                //saving the data in a text file called Log.txt
                File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/Log.txt"), Message);
                //Logger<MvcApplication>.Instance.Error(Message);
                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(new HandleErrorInfo(filterContext.Exception, controllerName, actionName))
                };

            }
        }
    }
}