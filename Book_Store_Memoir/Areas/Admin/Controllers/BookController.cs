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
using System.Drawing;
using System.Drawing.Imaging;

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
            if (HttpContext.Session.GetString("AdminName") == null)
            {
                _notyfService.Error("Tài khoản của bạn không có quyền truy cập chức năng này!!!!");
                return RedirectToAction("Index", "AdminLogin");
            }
            else if (_db.Admins.First().Role == "ADMIN")
            {
                var dsSanPham = _db.Books.Include(p => p.Category).
               Include(p => p.Publisher).Include(p => p.Language).
               Include(p => p.BookAuthors).
               ThenInclude(ba => ba.Author);
                return View(dsSanPham.ToList());
            }
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.DSTL = new SelectList(_db.Categories.ToList(), "Id", "Name");
            ViewBag.DSNN = new SelectList(_db.Languages.ToList(), "Id", "Language_Name");
            ViewBag.DSNXB = new SelectList(_db.Publisher.ToList(), "Id", "Name");
            ViewBag.DSTG = new SelectList(_db.Authors.ToList(), "Id", "NameAuhor");
            return View();
        }
        private bool IsImage(IFormFile file)
        {
            try
            {
                using (var image = Image.FromStream(file.OpenReadStream()))
                {
                    return image.RawFormat.Equals(ImageFormat.Jpeg) ||
                           image.RawFormat.Equals(ImageFormat.Png) ||
                           image.RawFormat.Equals(ImageFormat.Gif) ||
                           image.RawFormat.Equals(ImageFormat.Bmp);
                }
            }
            catch
            {
                return false;
            }
        }
        [HttpPost]
        public IActionResult Create(Book? book, IFormFile file, int[] Authors, IFormFileCollection files)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _environment.WebRootPath;
                if (file != null)
                {
                    if (!IsImage(file) || file == null)
                    {
                        _notyfService.Error("Chỉ được chọn file hình ảnh!!!");
                        // Xử lý lỗi nếu muốn
                        return RedirectToAction("Create");
                    }
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"image\sanpham");
                    var extention = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    book.Image = fileName + extention;
                }

                List<string> imagePaths = new List<string>();
                foreach (var file1 in files)
                {
                    // Tạo một tên tệp tin mới
                    string fileNames = Guid.NewGuid().ToString() + Path.GetExtension(file1.FileName);
                    var uploads1 = Path.Combine(wwwRootPath, @"image\sanphams");

                    using (var fileStream1 = new FileStream(Path.Combine(uploads1, fileNames), FileMode.Create))
                    {
                        file1.CopyTo(fileStream1);
                    }

                    // Thêm đường dẫn của hình ảnh mới vào danh sách
                    imagePaths.Add(fileNames);
                }
                book.Images = string.Join(",", imagePaths);

                _notyfService.Success("Thêm sản phẩm thành công!!!!");
                    _bookReponsitory.AddBook(book);
                    if (Authors != null && Authors.Any())
                    {
                        foreach (var authorId in Authors)
                        {
                            var bookAuthor = new BookAuthor { BookId = book.Id, AuthorId = authorId };
                            _db.BookAuthors.Add(bookAuthor);
                            _db.SaveChanges();


                        }
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.DSTL = new SelectList(_db.Categories.ToList(), "Id", "Name");
                ViewBag.DSNN = new SelectList(_db.Languages.ToList(), "Id", "Language_Name");
                ViewBag.DSNXB = new SelectList(_db.Publisher.ToList(), "Id", "Name");
                ViewBag.DSTG = new SelectList(_db.Authors.ToList(), "Id", "NameAuhor");
                return View();
            }
            public IActionResult Details(int id)
            {
                var book = _db.Books.Include(p => p.Category).
                    Include(p => p.Publisher).Include(p => p.Language).
                    Include(p => p.BookAuthors).
                    ThenInclude(ba => ba.Author).FirstOrDefault(p => p.Id == id);
                return View(book);
            }
            public IActionResult DeleteBook(Book book, int id)
            {
                int dem = _db.OrderDetails.Where(a => a.BookId == id).ToList().Count;
                ViewBag.flag = dem;
                if (dem > 0)
                {
                    Book x = _db.Books.Include(p => p.Category).
                    Include(p => p.Publisher).Include(p => p.Language).
                    Include(p => p.BookAuthors).
                    ThenInclude(ba => ba.Author).FirstOrDefault(p => p.Id == id);
                    _notyfService.Error("Không thể xóa sản phẩm!!!!");
                    return View(x);
                }
                else
                {
                    Book x = _db.Books.Include(p => p.Category).
                    Include(p => p.Publisher).Include(p => p.Language).
                    Include(p => p.BookAuthors).
                    ThenInclude(ba => ba.Author).FirstOrDefault(p => p.Id == id);
                    return View(x);
                }
            }
            public IActionResult Delete(int id)
            {
                try
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
                        var dsTacgia = _db.BookAuthors.Where(ba => ba.BookId == book.Id);
                        _db.BookAuthors.RemoveRange(dsTacgia);
                        _db.Books.Remove(book);
                        _db.SaveChanges();
                        _notyfService.Success("Sản phẩm đã được xóa khỏi hệ thống!!!");
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _notyfService.Error("Không thể xóa sản phẩm này!!!");
                    return RedirectToAction("Index");
                }
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
            public IActionResult Edit(Book book, IFormFile? file, int[] Authors)
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
                    if (Authors != null && Authors.Any())
                    {
                        var dsTacgia = _db.BookAuthors.Where(ba => ba.BookId == book.Id);
                        _db.BookAuthors.RemoveRange(dsTacgia);
                        foreach (var authorId in Authors)
                        {
                            var bookAuthor = new BookAuthor { BookId = book.Id, AuthorId = authorId };
                            _db.BookAuthors.Add(bookAuthor);
                            _db.SaveChanges();
                            _notyfService.Success("Cập nhật thông tin sách thành công!!");
                        }
                    }

                }
                return RedirectToAction("Index");
            }
            public IActionResult AddQuantity(int id)
            {
                var book = _db.Books.Find(id);
                return View(book);
            }
            [HttpPost]
            public IActionResult AddQuantity(Book book, int sl)
            {
                // Lấy thông tin sách hiện tại từ cơ sở dữ liệu
                var existingBook = _db.Books.Find(book.Id);

                if (existingBook != null)
                {
                    // Cộng dồn số lượng hiện tại với quantity
                    existingBook.Quantity += sl;

                    // Cập nhật thông tin sách trong cơ sở dữ liệu
                    _db.Books.Update(existingBook);
                    _db.SaveChanges();

                    _notyfService.Success("Thêm số lượng thành công!!!");
                }
                else
                {
                    _notyfService.Error("Sách không tồn tại!!!");
                }

                return RedirectToAction("Index");
            }
        }
    }
