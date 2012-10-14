using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Domain.DTO;

namespace Paul.UtahPlanners.Application
{
    [ServiceContract]
    public interface IPropertyService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        List<PropertiesIndex> FindAllIndecies();

        [OperationContract]
        List<PropertiesIndex> FindIndecies(IndexFilter filter, IndexSort sort);

        [OperationContract]
        List<AdminPropertyIndexDTO> FindAllAdminIndecies(PropertySort sort);
        
        [OperationContract]
        UserPropertyDTO FindUserProperty(int id);

        [OperationContract]
        AdminPropertyDTO FindAdminProperty(int id);

        [OperationContract]
        KeyValuePair<int, int> FindShowcaseProperty();

        [OperationContract]
        Picture FindPicture(int id);

        [OperationContract]
        LookupValues GetAllLookupValues();

        [OperationContract]
        Dictionary<int, string> GetLookupValues(LookupType lookupType);

        [OperationContract]
        int SaveProperty(Property property);

        [OperationContract]
        bool DeleteProperty(int propertyId);

        [OperationContract]
        bool CreateLookupType(LookupType lookupType, string value);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
