using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Web;

namespace Book_Store_Memoir.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShopController : Controller
    {

        private readonly ApplicationDbContext _db;
        public INotyfService _notyfService { get; }
        public ShopController(ApplicationDbContext db, INotyfService notyfService)
        {
            _db = db;
            _notyfService = notyfService;   
        }
        public IActionResult Index(int catID, string Search, int page=1 )
        {
            ViewBag.DSTL = new SelectList(_db.Categories.ToList(), "Id", "Name");
            int pageSize = 9;
            string userName = HttpContext.Session.GetString("UserName");
            ViewBag.UserName = userName;
            var dsSanPham = _db.Books.Include(p => p.Category).Include(p => p.Publisher).Include(p => p.Language).Include(p => p.BookAuthors).ThenInclude(ba => ba.Author).ToList();
            int totalBook = dsSanPham.Count();
            // Tính số trang cần hiển thị
            int totalPages = (int)Math.Ceiling((double)totalBook / pageSize);
            // Lấy danh sách sản phẩm cho trang hiện tại
            var productsForPage = dsSanPham.Skip((page - 1) * pageSize).Take(pageSize);
            // Truyền số trang, danh sách sản phẩm và tổng số trang vào view
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            if (!string.IsNullOrEmpty(Search) && catID != 0)
            {
                productsForPage = _db.Books.AsNoTracking()
                                   .Where(x => x.Category_Id == catID && x.Title.Contains(Search));
            }
            else if (!string.IsNullOrEmpty(Search))
            {
                productsForPage = _db.Books.AsNoTracking()
                                   .Where(x => x.Title.Contains(Search));
            }
            else if (catID != 0)
            {
                productsForPage = _db.Books.AsNoTracking().Where(x => x.Category_Id == catID);
            }
            return View(productsForPage);
            
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


            if (gh.CartItems.Any())
            {
                gh.CartItems.First().OrderTotal = (double)totalAmount;
            }
            else
            {
                // Giỏ hàng rỗng, bạn có thể xử lý tại đây, ví dụ: gán OrderTotal thành 0
                return View("CartEmpty");
            }

            MySessions.Set(HttpContext.Session, "GioHang", gh);
            _notyfService.Success("Đã thêm sản phẩm vào giỏ hàng!!");
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
        [HttpPost]
        public IActionResult CreateOrder(Orders orders, string Cusname, string Address, string Phone)
        {
            // Lấy thông tin người dùng từ phiên
            var user = HttpContext.Session.GetObject<Customers>("User");

            // Lấy thông tin giỏ hàng từ phiên (nếu bạn lưu thông tin giỏ hàng trong phiên)
            var shoppingCart = MySessions.Get<Orders>(HttpContext.Session, "GioHang");
            
            if (user!=null && shoppingCart!=null)
            {
                var order = new Orders
                {
                    CustomerId = user.CustomerId,
                    CustomersCustomerId = user.CustomerId,
                    BookId = shoppingCart.CartItems.First().Book.Id,
                    Quantity = shoppingCart.Quantity,
                    TotalAmount = shoppingCart.CartItems.First().OrderTotal,
                    OrderStatusId =1,
                    RecieverName = Cusname,
                    Address = Address,
                    PhoneNumber = Phone,
                    OrderDate = DateTime.Now,

                };
                _db.Orders.Add(order);
                _db.SaveChanges();
                foreach(var cartItem in shoppingCart.CartItems)
                {
                        var orderItem = new OrderDetails
                    {
                        OrdersId = order.Id,
                        BookId = cartItem.Book.Id,
                        Quantity = cartItem.Quantity,
                        TotalAmount = order.TotalAmount,

                        };
                    _db.OrderDetails.Add(orderItem);

                }
                _db.SaveChanges();
            }

           

            // Xóa thông tin giỏ hàng từ phiên sau khi đã tạo đơn hàng
            HttpContext.Session.Remove("GioHang");

            // Hiển thị thông báo hoặc trang cảm ơn
            return View("ThankYou");
        }


    }
}
