﻿using System.Collections.Generic;
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
        List<PropertiesIndex> GetAllIndecies();

        [OperationContract]
        List<PropertiesIndex> GetIndecies(IndexFilter filter, IndexSort sort);

        [OperationContract]
        List<Property> GetAllProperties(PropertySort sort);

        [OperationContract]
        UserPropertyDTO GetUserProperty(int id);

        [OperationContract]
        AdminPropertyDTO GetAdminProperty(int id);

        [OperationContract]
        int SaveProperty(Property property);

        [OperationContract]
        bool DeleteProperty(int propertyId);

        [OperationContract]
        KeyValuePair<int, int> GetShowcaseProperty();

        [OperationContract]
        Picture GetPicture(int id);

        [OperationContract]
        LookupValues GetLookupValues();
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
