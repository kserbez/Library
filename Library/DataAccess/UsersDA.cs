using Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Library.DataAccess
{
    public class UsersDA : BaseDataAccess
    {
        public List<UserModel> GetAllUsersAsList()
        {
            var sqlStr = "SELECT * FROM Users";
            DataTable usersDT = GetDataTableByQuery(sqlStr);

            var result = new List<UserModel>();
            for (int i = 0; i < usersDT.Rows.Count; i++)
                result.Add(GetUserModelBy(usersDT.Rows[i], usersDT.Columns));

            return result;
        }


        public void InsertUser(UserModel model)
        {
            string query = "INSERT INTO Users VALUES(@Email)";
            var parameters = new Dictionary<string, object> {
                { "@Email", model.Email },
            };
            ExecuteNonQuery(query, parameters);
        }

        public bool ExistsUserWithId(int id)
        {
            DataTable usersDT = GetUserDataTableBy(id);
            return (usersDT.Rows.Count == 1);
        }

        public UserModel GetUserModelBy(int id)
        {
            DataTable usersDT = GetUserDataTableBy(id);

            if (usersDT.Rows.Count == 1)
                return GetUserModelBy(usersDT.Rows[0], usersDT.Columns);
            else return null;
        }


        public DataTable GetUserDataTableBy(int id)
        {
            string query = "SELECT * FROM Users WHERE Id = @Id";
            var parameters = new Dictionary<string, object> {
                { "@Id", id },
            };
            DataTable result = GetDataTableByQuery(query, parameters);

            return result;
        }

        public UserModel GetUserModelBy(string email)
        {
            DataTable usersDT = GetUserDataTableBy(email);

            if (usersDT.Rows.Count == 1)
                return GetUserModelBy(usersDT.Rows[0], usersDT.Columns);
            else return null;
        }

        public DataTable GetUserDataTableBy(string email)
        {
            string query = "SELECT * FROM Users WHERE Email = @Email";
            var parameters = new Dictionary<string, object> {
                { "@Email", email },
            };
            DataTable result = GetDataTableByQuery(query, parameters);

            return result;
        }


        public UserModel GetUserModelBy(DataRow dataRow, DataColumnCollection columns)
        {
            return new UserModel
            {
                Id = Convert.ToInt32(dataRow[columns.IndexOf("Id")]),
                Email = Convert.ToString(dataRow[columns.IndexOf("Email")])
            };
        }
    }
}