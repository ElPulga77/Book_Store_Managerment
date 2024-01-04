using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Book_Store.DataAccess.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string name)
        {
        
            if (!string.IsNullOrEmpty(name))
            {
                ViewBag.Name = name;
            }
            string userName = HttpContext.Session.GetString("UserName");
            var user = HttpContext.Session.GetObject<Customers>("User");
            ViewBag.UserName = userName;
            if (user != null)
            {
                ViewBag.DuyNe = user.CustomerId;
            }
            else
            {
                // Xử lý khi user là null, có thể gán một giá trị mặc định hoặc làm gì đó tương ứng
                ViewBag.DuyNe = "";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}