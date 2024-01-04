using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.DataAccess.Reponsitory;
using Book_Store_Memoir.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CategoryReponsitory _categoryRepository;
        public INotyfService _notyfService { get; }
        public CategoryController(ApplicationDbContext db, CategoryReponsitory categoryReponsitory, INotyfService notyfService)
        {
            _db = db;
            _categoryRepository = categoryReponsitory;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            var categories = _categoryRepository.ListCate();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                /*_categoryRepository.AddCate(category);*/
                _notyfService.Success("Thêm thành công!!");
                return RedirectToAction("Index");
            }
            return View();
        }
        /*    public IActionResult Delete()
            {
                return View();
            }*/
        public IActionResult Delete(int id)
        {
            int dem = _db.Books.Where(a => a.Category_Id == id).ToList().Count;
            ViewBag.flag = dem;
            if (dem > 0)
            {
                Category cat = _db.Categories.FirstOrDefault(x => x.Id == id);
                _notyfService.Error("Không thể xóa thể loại này!!!!");
                return View(cat);
            }
            else
            {
                Category cat = _db.Categories.FirstOrDefault(x => x.Id == id);
                _db.Remove(cat);
                _db.SaveChanges();
                _notyfService.Success("Danh mục đã bị xóa!!");
                return RedirectToAction("Index");
            }
        }
        public IActionResult Edit(int id)
        {
            var cat = _db.Categories.FirstOrDefault(x=>x.Id == id);
           return View(cat);        
        }
        [HttpPost]
        public IActionResult Edit(Category cat)
        {
            if(ModelState.IsValid)
            {
                _db.Categories.Update(cat);
                _db.SaveChanges();
                _notyfService.Success("Cập nhật sản phẩm thành công!!!!");
                return RedirectToAction("Index");
            }
            return View(cat);
        }
    }
}
