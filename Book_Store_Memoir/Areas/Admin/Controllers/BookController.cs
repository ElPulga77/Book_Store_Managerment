using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.DataAccess.Reponsitory;
using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    /*[Authorize]*/
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly BookReponsitory _bookReponsitory;
        private IWebHostEnvironment _environment;
        public INotyfService _notyfService { get; }
        public BookController(ApplicationDbContext db, BookReponsitory bookReponsitory, IWebHostEnvironment environment, INotyfService notyfService)
        {
            _db = db;
            _bookReponsitory = bookReponsitory;
            _environment = environment;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            var dsSanPham = _db.Books.Include(p => p.Category).
                Include(p => p.Publisher).Include(p => p.Language).
                Include(p => p.BookAuthors).
                ThenInclude(ba => ba.Author);
            return View(dsSanPham.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.DSTL = new SelectList(_db.Categories.ToList(), "Id", "Name");
            ViewBag.DSNN = new SelectList(_db.Languages.ToList(), "Id", "Language_Name");
            ViewBag.DSNXB = new SelectList(_db.Publisher.ToList(), "Id", "Name");
            ViewBag.DSTG = new SelectList(_db.Authors.ToList(), "Id", "NameAuhor");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book, IFormFile file, int[] Authors)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _environment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"image\sanpham");
                    var extention = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    book.Image = fileName + extention;
                }
                _bookReponsitory.AddBook(book);
                if (Authors != null && Authors.Any())
                {
                    foreach (var authorId in Authors)
                    {
                        var bookAuthor = new BookAuthor { BookId = book.Id, AuthorId = authorId };
                        _db.BookAuthors.Add(bookAuthor);
                        _db.SaveChanges();  
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var book = _db.Books.Include(p => p.Category).
                Include(p => p.Publisher).Include(p => p.Language).
                Include(p => p.BookAuthors).
                ThenInclude(ba => ba.Author).FirstOrDefault(p => p.Id == id);
            return View(book);
        }
        public IActionResult DeleteBook(int id)
        {
            int dem = _db.ReceiptDetails.Where(a => a.BookId == id).ToList().Count;
            ViewBag.flag = dem;
            Book x = _db.Books.Include(p => p.Category).
                Include(p => p.Publisher).Include(p => p.Language).
                Include(p => p.BookAuthors).
                ThenInclude(ba => ba.Author).FirstOrDefault(p => p.Id == id);
            return View(x);
        }
        public IActionResult Delete(int id)
        {
            var book = _db.Books.Find(id);
            if (book != null)
            {
                if (!string.IsNullOrEmpty(book.Image))
                {
                    string imagePath = Path.Combine(_environment.WebRootPath, @"image\sanpham", book.Image);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                _db.Books.Remove(book);
                _db.SaveChanges();
                _notyfService.Success("Sản phẩm đã được xóa khỏi hệ thống!!!");
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.DSTL = new SelectList(_db.Categories.ToList(), "Id", "Name");
            ViewBag.DSNN = new SelectList(_db.Languages.ToList(), "Id", "Language_Name");
            ViewBag.DSNXB = new SelectList(_db.Publisher.ToList(), "Id", "Name");
            ViewBag.DSTG = new SelectList(_db.Authors.ToList(), "Id", "NameAuhor");
            var book = _db.Books.Find(id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                /* string wwwRootPath = _environment.WebRootPath;*/
                if (file != null)
                {
                    string wwwRootPath = _environment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"image\sanpham");
                    var extention = Path.GetExtension(file.FileName);

                    // Xóa tệp tin hình ảnh cũ trong thư mục hình ảnh sản phẩm
                    if (!string.IsNullOrEmpty(book.Image))
                    {
                        var imagePath = Path.Combine(uploads, book.Image);
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
                    book.Image = fileName + extention;
                    Debug.WriteLine("File path: " + Path.Combine(uploads, book.Image));
                }
                _db.Books.Update(book);
                _db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}
