using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MVC_App.Models;
using Microsoft.Extensions.Configuration;

namespace MVC_App.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlConnection _connection;
        private List<StudentModel> _studentList  = new List<StudentModel>();
        public void Connection()
        {
            string conn = _configuration.GetConnectionString("studentDB");
            _connection = new SqlConnection(conn);
            _connection.Open();
        }

        public IActionResult Index()
        {
            Connection();
            //Console.WriteLine(_configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT"));  -> To get the environment

            string selectQuery = "SELECT * FROM StudentDetails";
            using (SqlCommand cmd = new SqlCommand(selectQuery, _connection))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StudentModel studentModel = new StudentModel();
                    studentModel.Id = (int)reader[0];
                    studentModel.Name = (string)reader[1];
                    studentModel.Email = (string)reader[2];

                    _studentList.Add(studentModel);
                }
            }
            ViewBag.Students = _studentList;

            return View(ViewBag);
        }

    }
}
