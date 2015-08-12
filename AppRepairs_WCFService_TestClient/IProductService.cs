using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AppRepairs_WCFService_TestClient
{
    [ServiceContract]
    public interface IProductService
    {

        [OperationContract]
        List<Product> GetAllProducts();

        [OperationContract]
        List<Product> GetProductsByPartialName(string partialName);
    }


    [DataContract]
    public class Product
    {
        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
