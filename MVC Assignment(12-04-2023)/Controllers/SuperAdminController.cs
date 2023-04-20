using Microsoft.AspNetCore.Mvc;

namespace MVC_Assignment_12_04_2023_.Controllers
{
    public class SuperAdminController : Controller
    {

        public static string UserEmail;

        [HttpGet]
        public IActionResult SAdminIndex(string userEmail)
        {
            UserEmail = userEmail;


            return View();
        }
    }
}
