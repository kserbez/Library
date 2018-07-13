using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Library.DataAccess
{
    public abstract class BaseDataAccess
    {
        protected string connectionString = ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;


        public BaseDataAccess() { }

        public BaseDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected DataTable GetDataTableByQuery(string query, Dictionary<string,object> parameters = null)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, connection);
                if (parameters != null)
                {
                    foreach (var param in parameters)
                        sqlDA.SelectCommand.Parameters.AddWithValue(param.Key, param.Value);
                }

                sqlDA.Fill(result);
            }

            return result;
        }

        protected int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCmd = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        sqlCmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                return sqlCmd.ExecuteNonQuery();
            }
        }


    }
}