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
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.DeleteCate(id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
