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
            DataTable booksDT = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM Books", connection);
                //var sqlStr = @"SELECT b.Title, a.FullName
                //    FROM Books AS b LEFT JOIN BooksAuthors AS ba
                //    ON(b.Id = ba.BookId) LEFT JOIN Authors AS a
                //    ON(ba.AuthorID = a.Id)";
                //SqlDataAdapter sqlDA = new SqlDataAdapter(sqlStr, connection);
                sqlDA.Fill(booksDT);
            }

            var result = new List<BookModel>();
            for (int i = 0; i < booksDT.Rows.Count; i++)
                result.Add(GetBookModelBy(booksDT.Rows[i], booksDT.Columns));

            return result;
        }

        public void InsertBook(BookModel bookModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Books VALUES(@Title,@Quantity)";
                SqlCommand sqlCmd = new SqlCommand(query, connection);
                sqlCmd.Parameters.AddWithValue("@Title", bookModel.Title);
                sqlCmd.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                sqlCmd.ExecuteNonQuery();
            }
        }

        public void DeleteBook(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Books WHERE Id = @Id";
                SqlCommand sqlCmd = new SqlCommand(query, connection);
                sqlCmd.Parameters.AddWithValue("@Id", id);
                sqlCmd.ExecuteNonQuery();
            }
        }

        public void UpdateBook(BookModel bookModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Books SET Title = @Title , Quantity = @Quantity WHERE Id = @Id";
                SqlCommand sqlCmd = new SqlCommand(query, connection);
                sqlCmd.Parameters.AddWithValue("@Id", bookModel.Id);
                sqlCmd.Parameters.AddWithValue("@Title", bookModel.Title);
                sqlCmd.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                sqlCmd.ExecuteNonQuery();
            }
        }

        public bool ExistsBookWithId(int id)
        {
            DataTable booksDT = GetBookDataTableBy(id);
            return (booksDT.Rows.Count == 1);
        }

        public BookModel GetBookModelBy(int id)
        {
            DataTable booksDT = GetBookDataTableBy(id);

            if (booksDT.Rows.Count == 1)
                return GetBookModelBy(booksDT.Rows[0], booksDT.Columns);
            else return null;
        }


        public DataTable GetBookDataTableBy(int id)
        {
            DataTable result = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Books WHERE Id = @Id";
                SqlDataAdapter sqlDA = new SqlDataAdapter(query, connection);
                sqlDA.SelectCommand.Parameters.AddWithValue("@Id", id);
                sqlDA.Fill(result);
            }

            return result;
        }

        public BookModel GetBookModelBy(DataRow dataRow, DataColumnCollection columns)
        {
            return new BookModel
            {
                Id = Convert.ToInt32(dataRow[columns.IndexOf("Id")]),
                Title = Convert.ToString(dataRow[columns.IndexOf("Title")]),
                Quantity = Convert.ToInt32(dataRow[columns.IndexOf("Quantity")]),
            };
        }
    }
}