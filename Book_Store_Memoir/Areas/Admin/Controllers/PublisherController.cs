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
            if (HttpContext.Session.GetString("AdminName") == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
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
        public IActionResult Edit(int id)
        {
            var pub = _db.Publisher.Find(id);
            return View(pub);   
        }
        [HttpPost]
        public IActionResult Edit(Publisher pub)
        {
            if(ModelState.IsValid)
            {
                _db.Publisher.Update(pub);
                _db.SaveChanges();
                _notyfService.Success("Cập nhật thành công!!!!");
                return RedirectToAction("Index");
            }    
            return RedirectToAction("Index");   
        }
        public IActionResult Delete(int id)
        {
            int dem = _db.Books.Where(a => a.PublisherId == id).ToList().Count;
            ViewBag.flag = dem;
            if (dem > 0)
            {
                Publisher pub = _db.Publisher.FirstOrDefault(x => x.Id == id);
                _notyfService.Error("Không thể xóa nhà xuất bản này!!!!");
                return View(pub);
            }
            else
            {
                Publisher pub = _db.Publisher.FirstOrDefault(x => x.Id == id);
                _db.Remove(pub);
                _db.SaveChanges();
                _notyfService.Success("Nhà xuất bản đã bị xóa!!");
                
            }
            return RedirectToAction("Index");
        }
    }
}
