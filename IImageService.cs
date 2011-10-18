using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfImageService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IImageService" in both code and config file together.
    [ServiceContract]
    public interface IImageService
    {

        [OperationContract]
        string GetData(int value);

        [WebGet(ResponseFormat=WebMessageFormat.Xml)]
        string GetString();

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        CompositeType GetAnImage();

        [OperationContract]
        [WebGet(UriTemplate = "GetImage")]
        //[WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        System.ServiceModel.Channels.Message GetImage();
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";
        byte[] image;

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

        [DataMember]
        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
