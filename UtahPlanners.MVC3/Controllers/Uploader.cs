using System;
using System.Web;
using UtahPlanners.MVC3.Services;
using Ninject;
using System.Web.Mvc;
using System.IO;

namespace UtahPlanners.MVC3.Controllers
{
    public class Uploader : IHttpHandler
    {
        private IServiceFactory _factory;

        public Uploader()
        {
            // Kind of an anti-pattern but I don't really want to implement a custom Handler factory because it's overkill
            _factory = DependencyResolver.Current.GetService<IServiceFactory>();
        }
        
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
                var propertyId = Int32.Parse(context.Request["propertyId"]);
                var mimeType = "image/jpeg";
                var mainPicture = short.Parse(context.Request["mainPicture"] ?? "0");
                var secondaryPicture = short.Parse(context.Request["secondaryPicture"] ?? "0");
                var frontPage = short.Parse(context.Request["frontPage"] ?? "0");
                HttpPostedFile file = context.Request.Files["Filedata"];
                byte[] data;
                using (var memoryStream = new MemoryStream())
                {
                    file.InputStream.CopyTo(memoryStream);
                    data = memoryStream.ToArray();
                }

                using (var client = _factory.CreatePropertyServiceProxy())
                {
                    var result = client.UploadPicture(new PropertyService.Picture
                    {
                        binaryData = data,
                        frontPage = frontPage,
                        mainPicture = mainPicture,
                        mimeType = mimeType,
                        property_id = propertyId,
                        secondaryPicture = secondaryPicture
                    });
                }

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
