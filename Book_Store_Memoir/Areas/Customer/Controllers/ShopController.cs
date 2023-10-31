using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Book_Store_Memoir.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShopController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ShopController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            var dsSanPham = _db.Books.Include(p => p.Category).Include(p => p.Publisher).Include(p => p.Language).Include(p => p.BookAuthors).ThenInclude(ba => ba.Author);
            return View(dsSanPham.ToList());
        }
        public IActionResult Details(int id)
        {
            Book product = _db.Books.FirstOrDefault(u => u.Id == id);
            var dsSanPham = _db.Books.Include(p => p.Category).Include(p => p.Publisher).Include(p => p.Language).Include(p => p.BookAuthors).ThenInclude(ba => ba.Author).FirstOrDefault(u => u.Id == id);

            return View(dsSanPham);
        }

        public IActionResult Cart(int id, int sl)
        {
            Orders gh = MySessions.Get<Orders>(HttpContext.Session, "GioHang");
            if (gh == null)
            {
                gh = new Orders();
                gh.CartItems = new List<ShoppingCartVM>();
            }

            ShoppingCartVM existingItem = gh.CartItems.FirstOrDefault(item => item.Book.Id == id);

            if (existingItem != null)
            {
                if (existingItem.Quantity != sl)
                {
                    existingItem.Quantity += sl;
                }
            }
            else
            {
                Book book = _db.Books.Find(id);
                if (book != null)
                {
                    ShoppingCartVM item = new ShoppingCartVM
                    {
                        Book = book,
                        Quantity = sl
                    };

                    gh.CartItems.Add(item);
                }
            }

            // Tính tổng tiền
            // Tính tổng tiền
            decimal totalAmount = 0;
            foreach (var item in gh.CartItems)
            {
                totalAmount += (decimal)(item.Book.Price * item.Quantity);
            }


            gh.CartItems.First().OrderTotal = (double)totalAmount;
            // Lưu tổng tiền vào giỏ hàng

            MySessions.Set(HttpContext.Session, "GioHang", gh);
            return View(gh);
        }


        public IActionResult Plus(int cartId)
        {
            
            Orders gh = MySessions.Get<Orders>(HttpContext.Session, "GioHang");

            if (gh != null)
            {
                
                ShoppingCartVM existingItem = gh.CartItems.FirstOrDefault(item => item.Book.Id == cartId);

                if (existingItem != null)
                {
                    existingItem.Quantity++; // Tăng số lượng sản phẩm lên 1 đơn vị
                    MySessions.Set(HttpContext.Session, "GioHang", gh);
                }
            }

            return RedirectToAction("Cart"); // Chuyển hướng lại giỏ hàng sau khi tăng số lượng
        }
        public IActionResult Minus(int cartId)
        {
            Orders gh = MySessions.Get<Orders>(HttpContext.Session, "GioHang");

            if (gh != null)
            {
                ShoppingCartVM existingItem = gh.CartItems.FirstOrDefault(item => item.Book.Id == cartId);

                if (existingItem != null)
                {
                    if (existingItem.Quantity > 1)
                    {
                        existingItem.Quantity--; // Giảm số lượng sản phẩm xuống 1 đơn vị, đảm bảo số lượng không nhỏ hơn 1
                        MySessions.Set(HttpContext.Session, "GioHang", gh);
                    }
                }
            }

            return RedirectToAction("Cart"); // Chuyển hướng lại giỏ hàng sau khi giảm số lượng
        }

        public IActionResult Delete(int cartId)
        {
            Orders gh = MySessions.Get<Orders>(HttpContext.Session, "GioHang");

            if (gh != null)
            {
                ShoppingCartVM existingItem = gh.CartItems.FirstOrDefault(item => item.Book.Id == cartId);

                if (existingItem != null)
                {
                    gh.CartItems.Remove(existingItem); // Xóa sản phẩm khỏi giỏ hàng
                    MySessions.Set(HttpContext.Session, "GioHang", gh);
                }
            }

            return RedirectToAction("Cart"); // Chuyển hướng lại trang giỏ hàng sau khi xóa sản phẩm
        }
        public IActionResult Checkout()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login1", "UserLogin");
            }
            else
            {
                string userName = HttpContext.Session.GetString("UserName");
                string Phone = HttpContext.Session.GetString("Phone");
                var user = HttpContext.Session.GetObject<Customers>("User");
                Orders gh = MySessions.Get<Orders>(HttpContext.Session, "GioHang");

                if (gh == null)
                {
                    gh = new Orders();
                    gh.CartItems = new List<ShoppingCartVM>();
                }

                decimal totalAmount = 0;
                foreach (var item in gh.CartItems)
                {
                    totalAmount += (decimal)(item.Book.Price * item.Quantity);
                }
                gh.CartItems.First().OrderTotal = (double)totalAmount;

                return View("Checkout", new Checkout
                {
                    Cusname = user.Name,
                    Phone = user.Phone,
                    Email = user.Email,
                    Address = user.Address,
                    PaymentMethod = user.Name,
                    CartItems = gh.CartItems,
                    TotalAmount = (decimal)gh.CartItems.First().OrderTotal
                });
            }
               
        }
        
        public IActionResult CreateOrder(Orders orders)
        {
            // Lấy thông tin người dùng từ phiên
            var user = HttpContext.Session.GetObject<Customers>("User");

            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (user == null)
            {
                // Người dùng chưa đăng nhập, chuyển hướng hoặc xử lý theo cách khác
                return RedirectToAction("Login", "Account");
            }

            // Lấy thông tin giỏ hàng từ phiên (nếu bạn lưu thông tin giỏ hàng trong phiên)
            var shoppingCart = MySessions.Get<Orders>(HttpContext.Session, "GioHang");
            
            if (shoppingCart == null)
            {
                // Giỏ hàng trống, xử lý theo cách khác hoặc hiển thị thông báo
                return RedirectToAction("EmptyCart");
            }

            // Tiến hành tạo đơn hàng và lưu vào cơ sở dữ liệu
            // ...
            foreach(ShoppingCartVM a in shoppingCart.CartItems)
            {
                ShoppingCartVM dh = new ShoppingCartVM();
                
            }    
                

            // Xóa thông tin giỏ hàng từ phiên sau khi đã tạo đơn hàng
            HttpContext.Session.Remove("GioHang");

            // Hiển thị thông báo hoặc trang cảm ơn
            return View("ThankYou");
        }


    }
}
