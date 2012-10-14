using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.MVC3.PropertyService;
using System.ServiceModel;
using UtahPlanners.MVC3.UserService;
using UtahPlanners.MVC3.Helper;


namespace UtahPlanners.MVC3.Services
{
    public interface IPropertyServiceProxy : IPropertyService, ICommunicationObject, IDisposable
    {
    }

    public interface IUserServiceProxy : IUserService, ICommunicationObject
    {
    }

    public class ServiceFactory : IServiceFactory
    {
        public IPropertyServiceProxy CreatePropertyServiceProxy()
        {
            return new PropertyServiceClient() as IPropertyServiceProxy;
        }

        public virtual WcfClient<IPropertyServiceProxy> CreatePropertyServiceWrapper()
        {
            return new WcfClient<IPropertyServiceProxy>(new PropertyServiceClient() as IPropertyServiceProxy);
        }

        public WcfClient<IUserServiceProxy> CreateUserServiceWrapper()
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

    public partial class PropertyServiceClient : IPropertyServiceProxy
    {
        void IDisposable.Dispose()
        {
            bool success = false;
            try
            {
                if (State != CommunicationState.Faulted)
                {
                    Close();
                    success = true;
                }
            }
            finally
            {
                if (!success)
                {
                    Abort();
                }
            }
        }
    } // This is required to be able to cast the object to the interface
}

namespace UtahPlanners.MVC3.UserService
{
    using UtahPlanners.MVC3.Services;

    public partial class UserServiceClient : IUserServiceProxy {}
}