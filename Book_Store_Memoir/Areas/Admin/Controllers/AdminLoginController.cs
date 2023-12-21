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
        public AdminLoginController (ApplicationDbContext db)
        {
            _db = db;   
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Admins admin, string name, string password)
        {   
            if (HttpContext.Session.GetString("AdminName") == null)
            {
                var user = _db.Admins.Where(x => x.Name.Equals(admin.Name) && x.Password.Equals(admin.Password)).FirstOrDefault();
                HttpContext.Session.SetString("AdminName", admin.Name.ToString());   
                
                HttpContext.Session.SetObject("Admin", user);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
