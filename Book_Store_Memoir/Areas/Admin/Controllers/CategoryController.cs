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
        public CategoryController(ApplicationDbContext db, CategoryReponsitory categoryReponsitory)
        {
            _db = db;
            _categoryRepository = categoryReponsitory;
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
                _categoryRepository.AddCate(category);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
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
