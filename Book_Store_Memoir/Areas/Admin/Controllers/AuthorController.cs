using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.DataAccess.Reponsitory;
using Book_Store_Memoir.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            if (HttpContext.Session.GetString("AdminName") == null)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            var author = _db.Authors.ToList();
            return View(author);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Author? author, IFormFile? file)
        {

            if (author.NameAuhor == null || author.Hometown == null || author.Birthday == null)
            {
                _notyfService.Warning("Vui lòng nhập các thông tin còn thiếu");
                return View();
            }
            else
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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var author = _db.Authors.Find(id);
            return View(author);
        }
        [HttpPost]
        public IActionResult Edit(Author? author, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                /* string wwwRootPath = _environment.WebRootPath;*/
                if (file != null)
                {
                    string wwwRootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"image\tacgia");
                    var extention = Path.GetExtension(file.FileName);

                    // Xóa tệp tin hình ảnh cũ trong thư mục hình ảnh sản phẩm
                    if (!string.IsNullOrEmpty(author.Image))
                    {
                        var imagePath = Path.Combine(uploads, author.Image);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    // Tải lên hình ảnh mới và lưu tên tệp tin mới vào sản phẩm
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    author.Image = fileName + extention;
                    Debug.WriteLine("File path: " + Path.Combine(uploads, author.Image));
                }
                _db.Authors.Update(author);
                _db.SaveChanges();
                _notyfService.Success("Cập nhật thông tin tác giả thành công!!!");
            }
            return RedirectToAction("Index");
        }
    }
}
