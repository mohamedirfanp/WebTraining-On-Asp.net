using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace LMS_Project.Pages.BOOKS
{
	public class IndexBookModel : PageModel
	{
		private SqlConnection _connection;
		public List<Book> books;

		public void OnGet()
		{
			try
			{
				_connection = new SqlConnection("Data Source=5CG9400GDW;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
				_connection.Open();

				string deleteMode = "";
				string selectBookQuery = "SELECT BOOK_CODE, BOOK_TITLE, AUTHOR, PUBLICATION, PRICE FROM LMS_BOOK_DETAILS";

                if (Request.Query.IsNullOrEmpty() == false)
				{
                    string bookId = Request.Query["id"];

                    string deleteQuery = $"DELETE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{bookId}'";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, _connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    Response.Redirect("/BOOKS/IndexBook");
                }
                using (SqlCommand cmd = new SqlCommand(selectBookQuery, _connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    books = new List<Book>();
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.Id = reader[0].ToString().Trim();
                        book.Book_Title = reader[1].ToString().Trim();
                        book.Author = reader[2].ToString().Trim();
                        book.Publication = reader[3].ToString().Trim();
                        book.Price = int.Parse(reader[4].ToString().Trim());
                        books.Add(book);
                    }
                }

            }
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);

            }


        }


	}

	public class Book
	{
		public string Id  { get; set; }
		public string Book_Title { get; set; }

		public string Category { get; set; }

		public string Author { get; set; }
		public string Publication { get; set; }

		public DateTime Publish_Date { get; set; }

		public int Book_Edition { get; set; }

		public float Price { get; set; }

		public string Rack_Num { get; set; }

		public DateTime Date_Arrival { get; set; }

		public string Supplier_Id { get; set; }


	}

}
