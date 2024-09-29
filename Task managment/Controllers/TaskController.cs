using Microsoft.AspNetCore.Mvc;

namespace Task_managment.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
