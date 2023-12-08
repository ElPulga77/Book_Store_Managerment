using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.DataAccess.Reponsitory;
using Book_Store_Memoir.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;
        public INotyfService _notyfService { get; }
        private IWebHostEnvironment _environment;
        public AuthorController(ApplicationDbContext db, INotyfService notyfService, IWebHostEnvironment environment)
        {
            _db = db;
            _notyfService = notyfService;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var author = _db.Authors.ToList();
            return View(author);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Author author, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _environment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"image\tacgia");
                    var extention = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    author.Image = fileName + extention;
                }
                _db.Authors.Add(author); 
                _db.SaveChanges();
                _notyfService.Success("Thêm tác giả thành công!!!");
            }
            return RedirectToAction("Index");
        }
    }
}
