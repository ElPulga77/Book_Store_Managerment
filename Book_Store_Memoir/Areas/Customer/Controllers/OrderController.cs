using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Memoir.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
