using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtahPlanners.MVC3.PropertyService;
using UtahPlanners.MVC3.MembershipService;
using System.ServiceModel;


namespace UtahPlanners.MVC3.Services
{
    public interface IPropertyServiceProxy : IPropertyService, ICommunicationObject
    {
    }

    public interface IMembershipServiceProxy : IMembershipService, ICommunicationObject
    {
    }

    public class ServiceFactory : IServiceFactory
    {
        public virtual IPropertyServiceProxy CreatePropertyService()
        {
            return new PropertyServiceClient() as IPropertyServiceProxy;
        }

        public IMembershipServiceProxy CreateMembershipService()
        {
            return new MembershipServiceClient() as IMembershipServiceProxy;
        }
    }

}

namespace UtahPlanners.MVC3.PropertyService
{
    using UtahPlanners.MVC3.Services;

    public partial class PropertyServiceClient : IPropertyServiceProxy {} // This is required to be able to cast the object to the interface
}

namespace UtahPlanners.MVC3.MembershipService
{
    using UtahPlanners.MVC3.Services;

    public partial class MembershipServiceClient : IMembershipServiceProxy {}
}