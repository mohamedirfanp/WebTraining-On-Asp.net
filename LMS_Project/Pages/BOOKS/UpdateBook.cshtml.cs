using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;

namespace LMS_Project.Pages.BOOKS
{
	public class UpdateBookModel : PageModel
	{
		private SqlConnection _connection;
		public Book book = new Book();
		public string bookId;

		public UpdateBookModel()
		{
			_connection = new SqlConnection("Data Source=5CG9400GDW;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
			_connection.Open();
			
		}
		public void OnGet()
		{
			bookId = Request.Query["id"];

			string getBookQuery = $"SELECT * FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{bookId}'";
			
			using (SqlCommand cmd = new SqlCommand(getBookQuery, _connection))
			{
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					book.Id = bookId;
					book.Book_Title = (string)reader["BOOK_TITLE"];
					book.Author = (string)reader["AUTHOR"];
					book.Category = (string)reader["CATEGORY"];
					book.Publication = (string)reader["PUBLICATION"];
					book.Book_Edition = (int)reader["BOOK_EDITION"];
					book.Price = (int)reader["PRICE"];
					book.Publish_Date = Convert.ToDateTime(reader["PUBLISH_DATE"]);
				}
			}
		}

		public void OnPost()
		{
			Book book = new Book();
			book.Id = Request.Query["id"];
            book.Book_Title = Request.Form["title"];
            book.Category = Request.Form["category"];
            book.Author = Request.Form["author"];
            book.Publication = Request.Form["publication"];
            book.Publish_Date = DateTime.Parse(Request.Form["publish_date"].ToString());
            book.Book_Edition = int.Parse(Request.Form["edition"].ToString());
            book.Price = float.Parse(Request.Form["price"].ToString());


            try
            {
                

                string UpdateBookQuery = $"UPDATE LMS_BOOK_DETAILS SET BOOK_TITLE = '{book.Book_Title}'," +
					$"AUTHOR = '{book.Author}', CATEGORY = '{book.Category}', PUBLICATION = '{book.Publication}'," +
					$"BOOK_EDITION = {book.Book_Edition}, PRICE = {book.Price} WHERE BOOK_CODE = '{book.Id}'";


                using (SqlCommand cmd = new SqlCommand(UpdateBookQuery, _connection))
                {
                    int row_affect = cmd.ExecuteNonQuery();
                    Console.WriteLine(row_affect);
                }

				

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
			Response.Redirect("/BOOKS/IndexBook");
            //return RedirectPreserveMethod("~/BOOKS/IndexBook");
        }
    }
}
