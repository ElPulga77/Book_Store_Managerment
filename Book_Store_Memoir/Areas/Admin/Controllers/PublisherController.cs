using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _db;
        public INotyfService _notyfService { get; }
        public PublisherController(ApplicationDbContext db, INotyfService notyfService)
        {
            _db = db;   
            _notyfService=notyfService;
        }
        public IActionResult Index()
        {
            var publisher = _db.Publisher.ToList();
            return View(publisher);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Publisher pub)
        {
            if(ModelState.IsValid)
            {
                _db.Publisher.Add(pub);
                _db.SaveChanges();
                _notyfService.Success("Thêm thành công!!");
            }
            return RedirectToAction("Index");
        }
    }
}
