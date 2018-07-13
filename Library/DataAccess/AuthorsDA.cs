using Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Library.DataAccess
{
    public class AuthorsDA : BaseDataAccess
    {
        public List<AuthorModel> GetAllAuthorsAsList()
        {
            var sqlStr = "SELECT * FROM Authors";
            DataTable AuthorsDT = GetDataTableByQuery(sqlStr);

            var result = new List<AuthorModel>();
            for (int i = 0; i < AuthorsDT.Rows.Count; i++)
                result.Add(GetAuthorModelBy(AuthorsDT.Rows[i], AuthorsDT.Columns));

            return result;
        }


        public void InsertUser(AuthorModel model)
        {
            string query = "INSERT INTO Authors VALUES(@FullName)";
            var parameters = new Dictionary<string, object> {
                { "@FullName", model.FullName },
            };
            ExecuteNonQuery(query, parameters);
        }

        public bool ExistsAuthorWithId(int id)
        {
            DataTable authorsDT = GetAuthorDataTableBy(id);
            return (authorsDT.Rows.Count == 1);
        }

        public AuthorModel GetAuthorModelBy(int id)
        {
            DataTable authorsDT = GetAuthorDataTableBy(id);

            if (authorsDT.Rows.Count == 1)
                return GetAuthorModelBy(authorsDT.Rows[0], authorsDT.Columns);
            else return null;
        }


        public DataTable GetAuthorDataTableBy(int id)
        {
            string query = "SELECT * FROM Authors WHERE Id = @Id";
            var parameters = new Dictionary<string, object> {
                { "@Id", id },
            };
            DataTable result = GetDataTableByQuery(query, parameters);

            return result;
        }

        public AuthorModel GetAuthorModelBy(DataRow dataRow, DataColumnCollection columns)
        {
            return new AuthorModel
            {
                Id = Convert.ToInt32(dataRow[columns.IndexOf("Id")]),
                FullName = Convert.ToString(dataRow[columns.IndexOf("FullName")])
            };
        }
    }
}