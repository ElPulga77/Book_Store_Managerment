using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminName") == null && HttpContext.Session.GetString("ShipperName")==null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            var admin = HttpContext.Session.GetObject<Admins> ("Admin");
            var shipper = HttpContext.Session.GetObject<Shipper>("Shipper");
            if (admin != null)
            {
                ViewBag.Admim = admin.Id;
            }
            else if(shipper !=null)
            {
                ViewBag.Shipper = shipper.Id;   
            }    
            else
            {
                // Xử lý khi user là null, có thể gán một giá trị mặc định hoặc làm gì đó tương ứng
                ViewBag.Admim = "";
            }
            return View();
        }
    }
}
