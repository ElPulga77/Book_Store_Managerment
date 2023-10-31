using Book_Store_Memoir.Data;
using Book_Store_Memoir.DataAccess.Reponsitory;
using Book_Store_Memoir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_Memoir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly BookReponsitory _bookReponsitory;
        private IWebHostEnvironment _environment;
        public BookController(ApplicationDbContext db, BookReponsitory bookReponsitory, IWebHostEnvironment environment)
        {
            _db = db;
            _bookReponsitory = bookReponsitory;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var dsSanPham = _db.Books.Include(p => p.Category).
                Include(p=>p.Publisher).Include(p => p.Language).
                Include(p=>p.BookAuthors).
                ThenInclude(ba=>ba.Author);
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
        public IActionResult Create(Book book, IFormFile file)
        {
            if(ModelState.IsValid)
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
            }
            return RedirectToAction("Index");
        }
    }
}
