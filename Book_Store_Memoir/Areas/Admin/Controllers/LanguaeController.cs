using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguageController : Controller
    {
        private readonly ApplicationDbContext _db;
        public INotyfService _notyfService { get; }
        public LanguageController(ApplicationDbContext db, INotyfService notyfService)
        {
            _db = db;   
            _notyfService=notyfService;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            var language = _db.Languages.ToList();
            return View(language);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Language language)
        {
            if(ModelState.IsValid)
            {
                _db.Languages.Add(language);
                _db.SaveChanges();
                _notyfService.Success("Thêm thành công!!");
            }
            return RedirectToAction("Index");
        }
    }
}
