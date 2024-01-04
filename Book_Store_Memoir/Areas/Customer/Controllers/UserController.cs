using AspNetCoreHero.ToastNotification.Abstractions;
using Book_Store_Memoir.Data;
using Book_Store_Memoir.Models;
using Book_Store_Memoir.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_Memoir.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public INotyfService _notyfService { get; }
        public UserController (ApplicationDbContext db, INotyfService notyfService)
        {
            _db = db;   
            _notyfService = notyfService;   
        }
        public IActionResult Index(int id)
        {
            
            string userName = HttpContext.Session.GetString("UserName");
            var user = HttpContext.Session.GetObject<Customers>("User");
            ViewBag.UserName = userName;
            if (user != null)
            {
                ViewBag.DuyNe = user.CustomerId;
            }
            else
            {
                // Xử lý khi user là null, có thể gán một giá trị mặc định hoặc làm gì đó tương ứng
                ViewBag.DuyNe = "";
            }
            var dh = _db.Orders.Include(p => p.OrderStatus).Where(p => p.CustomersCustomerId == id);
            var dh1 = _db.DeliveryReceipts.Include(p => p.Orders).ThenInclude(a=>a.OrderStatus).Where(x => x.Orders.CustomersCustomerId == id);
            var dh2 = _db.DeliveryReceipts.Include(p=>p.ReceiptDetails).Include(p=>p.Orders).ThenInclude(a => a.OrderStatus).Where(x => x.Orders.CustomersCustomerId == id);
            // Lấy thông tin người dùng từ phiên
/*            var user = HttpContext.Session.GetObject<Customers>("User");
*/            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login1", "UserLogin");
            }
            else

                return View("Index", new CustomerInfo
                {
                    
                    Cusname = user.Name,
                    Phone = user.Phone,
                    Email = user.Email,
                    Address = user.Address,
                    YourOrder = dh2.ToList(),                  
                });
        }
        public IActionResult YourOrder(int id)
        {
            var receipt = _db.DeliveryReceipts.Include(p => p.Orders)
                .ThenInclude(o => o.Customers)
                        .ThenInclude(o => o.Orders).ThenInclude(e => e.OrderStatus)
                .Include(p => p.Shipper)
                .FirstOrDefault(m => m.Id == id);
            var Chitietdonhang = _db.ReceiptDetails
                .Include(x => x.Book)
                .Where(x => x.DeliveryReceiptId == id)
                .OrderBy(x => x.Id);
            ViewBag.ChiTiet = Chitietdonhang.ToList();
            return View(receipt);
        }
    }
}
