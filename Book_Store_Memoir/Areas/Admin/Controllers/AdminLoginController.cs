using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.DataAccess.Reponsitory;
using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        public INotyfService _notyfService { get; }
        public AdminLoginController (ApplicationDbContext db, INotyfService notyfService)
        {
            _db = db;   
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Admins admin, Shipper ship, string name, string password)
        {
            if (HttpContext.Session.GetString("AdminName") == null)
            {
                var adminlog = _db.Admins.Where(x => x.Name.Equals(admin.Name) && x.Password.Equals(admin.Password)).FirstOrDefault();
                var shipper = _db.Shipper.Where(x => x.Name.Equals(ship.Name) && x.Password.Equals(ship.Password)).FirstOrDefault();
                if (adminlog != null)
                {
                    HttpContext.Session.SetString("AdminName", admin.Name.ToString());
                    HttpContext.Session.SetObject("Admin", adminlog);
                    return RedirectToAction("Index", "Home");
                }
                else if(shipper!=null)
                {
                    HttpContext.Session.SetString("ShipperName", admin.Name.ToString());
                    HttpContext.Session.SetObject("Shipper", shipper);
                    return RedirectToAction("Index", "Home");
                }    
                else
                {
                    _notyfService.Error("Tên tài khoản hoặc mật khẩu không đúng. Vui lòng nhập lại!!!");
                    return RedirectToAction("Index", "AdminLogin");
                }
            }

            // Admin is already logged in, redirect to the desired page.
            return RedirectToAction("Index", "Home");
        }

    }
}
