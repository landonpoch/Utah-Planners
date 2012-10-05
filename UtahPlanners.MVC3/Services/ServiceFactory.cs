using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.MVC3.PropertyService;
using System.ServiceModel;
using UtahPlanners.MVC3.UserService;
using UtahPlanners.MVC3.Presentation;


namespace UtahPlanners.MVC3.Services
{
    public interface IPropertyServiceProxy : IPropertyService, ICommunicationObject
    {
    }

    public interface IUserServiceProxy : IUserService, ICommunicationObject
    {
    }

    public class ServiceFactory : IServiceFactory
    {
        public virtual WcfClient<IPropertyServiceProxy> CreatePropertyService()
        {
            return new WcfClient<IPropertyServiceProxy>(new PropertyServiceClient() as IPropertyServiceProxy);
        }

        public WcfClient<IUserServiceProxy> CreateUserService()
        {
            return new WcfClient<IUserServiceProxy>(new UserServiceClient() as IUserServiceProxy);
        }

        public IFormsAuthenticationService CreateFormsAuthenticationService()
        {
            return new FormsAuthenticationService();
        }
    }

}

namespace UtahPlanners.MVC3.PropertyService
{
    using UtahPlanners.MVC3.Services;

    public partial class PropertyServiceClient : IPropertyServiceProxy {} // This is required to be able to cast the object to the interface
}

namespace UtahPlanners.MVC3.UserService
{
    using UtahPlanners.MVC3.Services;

    public partial class UserServiceClient : IUserServiceProxy {}
}