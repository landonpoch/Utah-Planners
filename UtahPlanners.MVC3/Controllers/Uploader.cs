using System;
using System.Web;

namespace UtahPlanners.MVC3.Controllers
{
    public class Uploader : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                // Session not enabled
                // string sessionId = context.Session.SessionID;
                
                var mimeType = "image/jpeg";
                var mainPicture = context.Request["mainPicture"];
                var secondaryPicture = context.Request["secondaryPicture"];
                var frontPage = context.Request["frontPage"];
                HttpPostedFile file = context.Request.Files["Filedata"];
                

                context.Response.ContentType = "text/plain";
                context.Response.Write("Upload Successful");
            }
            catch (Exception e)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Upload Failed");
            }
        }

        #endregion
    }
}
