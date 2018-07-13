using Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Library.DataAccess
{
    public class BooksDA : BaseDataAccess
    {
        public List<BookModel> GetAllBooksAsList()
        {
            var query = "SELECT * FROM Books";
            DataTable dt = GetDataTableByQuery(query);

            var result = new List<BookModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
                result.Add(GetBookModelBy(dt.Rows[i], dt.Columns));

            return result;
        }

        public List<BookModel> GetAllBooksWithAuthorsAsList()
        {
            var query = @"SELECT b.Id, b.Title, b.Quantity, a.FullName FROM Books AS b
                    LEFT JOIN BooksAuthors AS ba ON(b.Id = ba.BookId)
                    LEFT JOIN Authors AS a ON(ba.AuthorID = a.Id)
                    ORDER BY b.Id";
            DataTable booksDT = GetDataTableByQuery(query);

            var result = new List<BookModel>();
            for (int i = 0; i < booksDT.Rows.Count; i++)
                result.Add(GetBookModelBy(booksDT.Rows[i], booksDT.Columns));

            return GetUniqueWithAuthors(result);
        }


        public void InsertBook(BookModel model)
        {
            string query = "INSERT INTO Books VALUES(@Title,@Quantity)";
            var parameters = new Dictionary<string, object> {
                { "@Title", model.Title },
                { "@Quantity", model.Quantity },
            };
            ExecuteNonQuery(query, parameters);
        }

        public void DeleteBook(int id)
        {
            string query = "DELETE FROM Books WHERE Id = @Id";
            var parameters = new Dictionary<string, object> {
                { "@Id", id },
            };
            ExecuteNonQuery(query, parameters);
        }

        public void UpdateBook(BookModel model)
        {
            string query = "UPDATE Books SET Title = @Title , Quantity = @Quantity WHERE Id = @Id";
            var parameters = new Dictionary<string, object> {
                { "@Id", model.Id },
                { "@Title", model.Title },
                { "@Quantity", model.Quantity },
            };
            ExecuteNonQuery(query, parameters);
        }

        public bool ExistsBookWithId(int id)
        {
            DataTable dt = GetBookDataTableBy(id);
            return (dt.Rows.Count == 1);
        }

        public BookModel GetBookModelBy(int id)
        {
            DataTable dt = GetBookDataTableBy(id);

            if (dt.Rows.Count == 1)
                return GetBookModelBy(dt.Rows[0], dt.Columns);
            else return null;
        }


        public DataTable GetBookDataTableBy(int id)
        {
            string query = "SELECT * FROM Books WHERE Id = @Id";
            var parameters = new Dictionary<string, object> {
                { "@Id", id },
            };

            return GetDataTableByQuery(query, parameters);
        }

        public BookModel GetBookModelBy(DataRow dataRow, DataColumnCollection columns)
        {
            return new BookModel
            {
                Id = Convert.ToInt32(dataRow[columns.IndexOf("Id")]),
                Title = Convert.ToString(dataRow[columns.IndexOf("Title")]),
                Quantity = Convert.ToInt32(dataRow[columns.IndexOf("Quantity")]),
                Authors = null //new List<AuthorModel>()
            };
        }


        private List<BookModel> GetUniqueWithAuthors(List<BookModel> models)
        {
            var result = new List<BookModel>();

            result = models.Distinct().ToList();

            return result;
        }
    }
}