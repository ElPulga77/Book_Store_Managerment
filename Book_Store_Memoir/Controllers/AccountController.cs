using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Memoir.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
