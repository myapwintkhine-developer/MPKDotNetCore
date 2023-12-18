using Microsoft.AspNetCore.Mvc;

namespace MPKDotNetCore.MvcApp.Controllers
{
    public class UserController : Controller
    {
        [ActionName("Index")]
        public IActionResult UserIndex()
        {
            return View("UserIndex");
        }
    }
}