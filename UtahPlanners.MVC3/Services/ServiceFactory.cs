using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.MVC3.PropertyService;
using System.ServiceModel;


namespace UtahPlanners.MVC3.Services
{
    public interface IPropertyServiceProxy : IPropertyService, ICommunicationObject
    {
    }

    public class ServiceFactory : IServiceFactory
    {
        public virtual IPropertyServiceProxy CreateService()
        {
            return new PropertyServiceClient() as IPropertyServiceProxy;
        }
    }
}

namespace UtahPlanners.MVC3.PropertyService
{
    using UtahPlanners.MVC3.Services;

    public partial class PropertyServiceClient : IPropertyServiceProxy {} // This is required to be able to cast the object to the interface
}