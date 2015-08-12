using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppRepairs_WCFService_TestClient.Data_Access
{
    public class ProductDB
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AppRepairs"].ConnectionString;
        }


        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            string query = "SELECT ProductCode, Name " +
                           "FROM Products " +
                           "ORDER BY ProductCode";

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) //while there is still another record
                        {
                            Product p = new Product();
                            p.ProductCode = reader["ProductCode"].ToString();
                            p.Name = reader["Name"].ToString();
                            products.Add(p); //add to the list
                        }
                    }  
                }
            }

            return products;
        }


        public static List<Product> GetProductsByPartialName(string partialName)
        {
            List<Product> results = new List<Product>(); //empty list

            string query = "SELECT ProductCode, Name " +
                           "FROM Products " +
                           "WHERE Name LIKE @PartialName " +
                           "ORDER BY ProductCode";

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PartialName", "%" + partialName + "%");

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) //while there is still another record
                        {
                            Product p = new Product();
                            p.ProductCode = reader["ProductCode"].ToString();
                            p.Name = reader["Name"].ToString();
                            results.Add(p); //add to the list
                        }
                    }
                }
            }

            return results;
        }
    } //END CLASS
}