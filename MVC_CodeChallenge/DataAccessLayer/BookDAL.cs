using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MVC_CodeChallenge.Models;

namespace MVC_CodeChallenge.DataAccessLayer
{
    public class BookDAL
    {
        SqlConnection connection;
        SqlCommand cmd;    
        

        public bool AddBook(BookModel book)
        {
            string con = "Data Source=.;Initial Catalog=MVC_CodeChallenge;Integrated Security=True";
            connection = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand("select * from Author where authorId = @id", connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@id", book.author.authorId);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                Console.WriteLine("Author already exists!");
            }
            else
            {
                connection.Open();
                SqlCommand cmd1 = new SqlCommand("Insert into Author values(@authorId,@name,@address)", connection);
                cmd1.Parameters.AddWithValue("@authorId", book.author.authorId);
                cmd1.Parameters.AddWithValue("@name", book.author.name);
                cmd1.Parameters.AddWithValue("@address", book.author.address);
                cmd1.ExecuteNonQuery();
                connection.Close();
            }
            connection.Open();
            SqlCommand cmd2 = new SqlCommand("insert into book values(@bookId,@title,@genre,@price,@authorId)", connection);
            cmd2.Parameters.AddWithValue("@bookId", book.bookId);
            cmd2.Parameters.AddWithValue("@title", book.title);
            cmd2.Parameters.AddWithValue("@genre", book.genre);
            cmd2.Parameters.AddWithValue("@price", book.price);
            cmd2.Parameters.AddWithValue("@authorId", book.author.authorId);
            cmd2.ExecuteNonQuery();
            connection.Close();
            return true;
        }


      
        public List<BookModel> GetBooks()
        {
            string con = "Data Source=.;Initial Catalog=MVC_CodeChallenge;Integrated Security=True";
            connection = new SqlConnection(con);
            List<BookModel> list = new List<BookModel>();
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select  * from Book inner join Author on Book.authorId=Author.authorId", connection);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                BookModel book = new BookModel();
                book.bookId = (int)rdr[0];
                book.title = rdr[1].ToString();
                book.genre = rdr[2].ToString();
                book.price = (decimal)rdr[3];
                AuthorModel author = new AuthorModel();
                author.authorId = (int)rdr[5];
                author.name = rdr[6].ToString();
                author.address = rdr[7].ToString();
                book.author = author;
                list.Add(book);
            }
            return list;
        }
        public BookModel GetBook(int id)
        {
            string con = "Data Source=.;Initial Catalog=MVC_CodeChallenge;Integrated Security=True";
            connection = new SqlConnection(con);
            connection.Open();
            cmd = new SqlCommand("select * from Book inner join Author on Book.authorId=Author.authorId and Book.bookId=@id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr1 = cmd.ExecuteReader();
            BookModel book = new BookModel();
            while (dr1.Read())
            {

                book.bookId = (int)dr1[0];
                book.title = dr1[1].ToString();
                book.genre = dr1[2].ToString();
                book.price = (decimal)dr1[3];
                book.authorId = (int)dr1[4];
                AuthorModel author = new AuthorModel();
                author.authorId= (int)dr1[5];
                author.name = dr1[6].ToString();
                author.address = dr1[7].ToString();
                book.author = author;
            }
            connection.Close();
            return book;
        }

        public bool UpdateBooks(int id, BookModel book)
        {
            string con = "Data Source=.;Initial Catalog=MVC_CodeChallenge;Integrated Security=True";
            connection = new SqlConnection(con);
            connection.Open();
                cmd = new SqlCommand("update Book set title=@title,genre=@genre,price=@price where bookId=@bookId", connection);
                cmd.Parameters.AddWithValue("@bookId", book.bookId);
                cmd.Parameters.AddWithValue("@title", book.title);
                cmd.Parameters.AddWithValue("@genre", book.genre);
                cmd.Parameters.AddWithValue("@price", book.price);
                
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
    
        }

        public bool DeleteBooks(int id)
        {
            string con = "Data Source=.;Initial Catalog=MVC_CodeChallenge;Integrated Security=True";
            connection = new SqlConnection(con);
            connection.Open();
                cmd = new SqlCommand("Delete from Book where bookId=@id", connection);
                cmd.Parameters.AddWithValue("@id", id);
              
                cmd.ExecuteNonQuery();
                connection.Close();
                return true;
          
        }



    }
}