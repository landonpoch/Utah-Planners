﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UtahPlanners.Domain;

namespace Paul.UtahPlanners.Application
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
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
        List<PropertiesIndex> GetIndecies(IndexFilter filter = null, IndexSort sort = null);

        [OperationContract]
        Property GetProperty(int id);

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
