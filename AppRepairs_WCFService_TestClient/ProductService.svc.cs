using AppRepairs_WCFService_TestClient.Data_Access;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AppRepairs_WCFService_TestClient
{
   public class Service1 : IProductService
    {
        public List<Product> GetAllProducts()
        {
            try
            {
                return ProductDB.GetAllProducts();
            }
            catch (SqlException ex)
            {
                throw new FaultException(
                    new FaultReason(ex.Message),
                    new FaultCode("Database Error"));
            }
            catch (Exception ex)
            {
                throw new FaultException(
                    new FaultReason(ex.Message),
                    new FaultCode("Error"));
            }
        }
        

        public List<Product> GetProductsByPartialName(string partialName)
        {
            try
            {
                return ProductDB.GetProductsByPartialName(partialName);
            }
            catch (SqlException ex)
            {
                throw new FaultException(
                    new FaultReason(ex.Message),
                    new FaultCode("Database Error"));
            }
            catch (Exception ex)
            {
                throw new FaultException(
                    new FaultReason(ex.Message),
                    new FaultCode("Error"));
            }     
        }
    } //END CLASS
}
