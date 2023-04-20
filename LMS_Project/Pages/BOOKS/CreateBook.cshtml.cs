using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LMS_Project.Pages.BOOKS
{
    public class CreateBookModel : PageModel
    {
        private SqlConnection _connection;
        public Book book = new Book();
        public string errorMessage = "";
        public string successMessage = "";


        public void OnGet()
        {
        }

        public void OnPost()
        {
            _connection = new SqlConnection("Data Source=5CG9400GDW;Initial Catalog=LMS_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            _connection.Open();


            book.Id = Request.Form["id"];
            book.Book_Title = Request.Form["title"];
            book.Category = Request.Form["category"];
            book.Author = Request.Form["author"];
            book.Publication = Request.Form["publication"];
            book.Publish_Date = DateTime.Parse(Request.Form["publish_date"].ToString());
            book.Book_Edition = int.Parse(Request.Form["edition"].ToString());
            book.Price = float.Parse(Request.Form["price"].ToString());

            book.Rack_Num = "A1";
            book.Date_Arrival = DateTime.Parse("2023-01-01");
            book.Supplier_Id = "S01";



            try
            {
                errorMessage = "";
                successMessage = "";

            string addBookQuery = $"INSERT INTO LMS_BOOK_DETAILS VALUES ('{book.Id}','{book.Book_Title}','{book.Category}'," +
                $"'{book.Author}','{book.Publication}','{book.Publish_Date}',{book.Book_Edition},{book.Price}, '{book.Rack_Num}'," +
                $"'{book.Date_Arrival}','{book.Supplier_Id}');";

                Console.WriteLine(addBookQuery);

                using(SqlCommand cmd = new SqlCommand(addBookQuery, _connection))
                {
                    int row_affect = cmd.ExecuteNonQuery();
                    Console.WriteLine(row_affect);
                }
                successMessage = "Successfully Book Added!!!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errorMessage = ex.Message;
            }

            
        }

    }
}
