using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using UtahPlanners.Domain.Entity;
using UtahPlanners.Domain.DTO;
using System;

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
        List<CsvPropertyDTO> FindAllCsvProperties();

        [OperationContract]
        KeyValuePair<int, int> FindShowcaseProperty();

        [OperationContract]
        Picture FindPicture(int id);

        [OperationContract]
        LookupValues GetAllLookupValues();

        [OperationContract]
        Dictionary<int, string> GetLookupTypes(LookupType lookupType);

        [OperationContract]
        Dictionary<int, Tuple<string, int>> GetLookupCodes(LookupCode lookupCode);

        [OperationContract]
        Weight GetWeights();

        [OperationContract]
        int SaveProperty(AdminPropertyDTO property);

        [OperationContract]
        bool DeleteProperty(int propertyId);

        [OperationContract]
        bool CreateLookupType(LookupType lookupType, string value);

        [OperationContract]
        bool ModifyLookupType(LookupType lookupType, int id, string value);

        [OperationContract]
        bool DeleteLookupType(LookupType lookupType, int id);

        [OperationContract]
        bool CreateLookupCode(LookupCode lookupCode, string value, int weight);

        [OperationContract]
        bool ModifyLookupCode(LookupCode lookupCode, int id, string value, int weight);

        [OperationContract]
        bool DeleteLookupCode(LookupCode lookupCode, int id);

        [OperationContract]
        bool UpdateWeights(Weight weights);

        [OperationContract]
        bool UploadPicture(Picture pic);
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
