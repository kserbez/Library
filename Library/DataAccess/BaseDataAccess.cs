using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Library.DataAccess
{
    public abstract class BaseDataAccess
    {
        public BaseDataAccess() { }

        public BaseDataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected string connectionString = ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;
        protected SqlConnection sqlConnection = null;

        public void OpenConnection() // TODO: delete or find where it's needed
        {
            sqlConnection = new SqlConnection(this.connectionString);
            sqlConnection.Open();
        }

        public void CloseConnection() // TODO: delete or find where it's needed
        {
            sqlConnection?.Close();
        }
    }
}